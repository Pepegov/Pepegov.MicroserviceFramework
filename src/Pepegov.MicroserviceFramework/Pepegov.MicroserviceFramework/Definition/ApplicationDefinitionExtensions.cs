﻿using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.Definition;

public static class ApplicationDefinitionExtensions
{
    public static void AddApplicationDefinitionsModules(this IServiceCollection services, string contentRootPath, string modulesFolderPath, IDefinitionServiceContext context)
    { //TODO: test this method
        var modulesFolder = Path.Combine(contentRootPath, modulesFolderPath);

        if (!Directory.Exists(modulesFolder))
        {
            throw new DirectoryNotFoundException(modulesFolder);
        }

        var instances = new List<IApplicationDefinition>();
        
        var modulesDirectory = new DirectoryInfo(modulesFolderPath);
        var modules = modulesDirectory.GetFiles("*.dll");
        if (!modules.Any())
        {
            return;
        }

        foreach (var fileInfo in modules)
        {
            var module = Assembly.LoadFile(fileInfo.FullName);
            var typesAll = module.GetExportedTypes();
            var typesDefinition = typesAll
                .Where(type =>
                    type is { IsAbstract: false, IsInterface: false } &&
                    typeof(ApplicationDefinition).IsAssignableFrom(type));

            var moduleInstances = typesDefinition.Select(Activator.CreateInstance)
                .Cast<IApplicationDefinition>()
                .Where(x => x.Enabled && x.Exported)
                .ToList();

            instances.AddRange(moduleInstances);
        }
        
        if (instances.Any())
        {
            var applicationDefinitionInformation = services
                                                       .Where(x => x.ServiceType == typeof(ApplicationDefinitionInformationRecords))
                                                       .Select(x => x.ImplementationInstance)
                                                       .Cast<ApplicationDefinitionInformationRecords>()
                                                       .FirstOrDefault()
                                                   ?? new ApplicationDefinitionInformationRecords();

            foreach (var definition in instances)
            {
                applicationDefinitionInformation.AddApplicationDefinitionInformation(
                    new ApplicationDefinitionInformation(definition, definition.GetType().Assembly, definition.Enabled, definition.Exported));
            }
            
            //add to di
            services.AddSingleton(applicationDefinitionInformation);
        }
    }
    
    public static Task AddApplicationDefinitions(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        => AddApplicationDefinitions(services, new DefinitionServiceContext(services, configuration), assemblies);

    public static async Task AddApplicationDefinitions(this IServiceCollection services, IDefinitionServiceContext context, params Assembly[] assemblies)
    {
        var definitionInstances = new List<IApplicationDefinition>();
        var applicationDefinitionInformation = services
                                                   .Where(x => x.ServiceType == typeof(ApplicationDefinitionInformationRecords))
                                                   .Select(x => x.ImplementationInstance)
                                                   .Cast<ApplicationDefinitionInformationRecords>()
                                                   .FirstOrDefault()
                                               ?? new ApplicationDefinitionInformationRecords();
        
        //find & save application definitions by assembles
        foreach (var assembly in assemblies)
        {
            var types = assembly.ExportedTypes.Where(t =>
                !t.IsAbstract && typeof(IApplicationDefinition).IsAssignableFrom(t));
            if (types is null)
            {
                continue;
            }
            
            var instances = types.Select(Activator.CreateInstance).Cast<IApplicationDefinition>();
            definitionInstances.AddRange(instances);
            
            foreach (var definition in instances)
            {
                applicationDefinitionInformation.AddApplicationDefinitionInformation(
                    new ApplicationDefinitionInformation(definition, assembly, definition.Enabled, definition.Exported));
            }
        }

        
        //Configure services in all definitions
        var orderByDescending = definitionInstances 
            .Where(x => x.Enabled)
            .OrderByDescending(x => x.Priority);
        foreach (var definition in orderByDescending)
        {
            await definition.ConfigureServicesAsync(context);
        }

        //Log information about application definitions
        var loggerFactoryDescriptor = services.FirstOrDefault(x => x.ServiceType == typeof(ILoggerFactory));
        if (loggerFactoryDescriptor?.ImplementationInstance is not null)
        {
            var logger = ((ILoggerFactory)loggerFactoryDescriptor.ImplementationInstance).CreateLogger<IApplicationDefinition>();
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("[AppDefinitions]: From {@items}", string.Join(", ", 
                    applicationDefinitionInformation.Items.Select(x => x.Assembly.FullName)));

                foreach (var item in applicationDefinitionInformation.Items)
                {
                    logger.LogDebug("[AppDefinitions]: {@AppDefinitionName} ({@AssemblyName}) (Enabled: {@Enabled})", item.ApplicationDefinition.GetType().Name, item.Assembly, item.ApplicationDefinition.Enabled ? "Yes" : "No");
                }
            }
        }
        
        //add to di
        services.AddSingleton(applicationDefinitionInformation);
    }

    public static Task UseApplicationDefinitions(this IServiceProvider serviceProvider)
        => UseApplicationDefinitions(serviceProvider, new DefinitionApplicationContext(serviceProvider));
    
    public static async Task UseApplicationDefinitions(this IServiceProvider serviceProvider, IDefinitionApplicationContext context)
    {
        var logger = serviceProvider.GetService<ILogger<ApplicationDefinition>>();
        var definitionRecords = serviceProvider.GetRequiredService<ApplicationDefinitionInformationRecords>();

        if (logger is not null && logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("From {Modules} assemblies totally AppDefinitions found: {Count} ", 
                string.Join(", ", definitionRecords.Items.Select(x => x.Assembly.FullName)), definitionRecords.Items.Count);
        }

        //Configure application in all definitions
        var orderByDescending = definitionRecords.Items 
            .Where(x => x.ApplicationDefinition.Enabled)
            .OrderByDescending(x => x.ApplicationDefinition.Priority);
        
        foreach (var definition in orderByDescending)
        {
            if (context is not null)
            {
                await definition.ApplicationDefinition.ConfigureApplicationAsync(context);   
            }
            else
            {
                await definition.ApplicationDefinition.ConfigureApplicationAsync(new DefinitionApplicationContext(serviceProvider));
            }
        }
        
        if (logger is not null && logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Total AppDefinitions applied: {Count}", orderByDescending.Count());
        }
    }

    /*public static void ConfigureDefinitionServiceByType(this WebApplicationBuilder builder, params Type[] definitionTypes)
    {
        var services = builder.Services
            .Where(x => x.ServiceType == typeof(List<IApplicationDefinition>) && x.ImplementationInstance is not null)
            .Select(x => (List<IApplicationDefinition>)x.ImplementationInstance!).FirstOrDefault();

        var collection = services.Where(x => definitionTypes.Any(o => o == x.GetType()));
        foreach (var definition in collection)
        {
            definition.ConfigureServicesAsync(builder.Services, builder.Configuration);
        }
    }
    
    public static void UseDefinitionByType(this WebApplication application, params Assembly[] definitionTypes)
    {
        using (var scope = application.Services.CreateScope())
        {
            var collection = scope.ServiceProvider.GetService<IReadOnlyCollection<IApplicationDefinition>>()
                .Where(x => definitionTypes.Any(o => o == x. GetType())).ToList();
            collection.ForEach(x => x.ConfigureApplicationAsync(application));
        }
    }*/
}