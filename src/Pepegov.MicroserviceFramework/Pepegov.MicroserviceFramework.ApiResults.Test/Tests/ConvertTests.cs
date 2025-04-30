using NUnit.Framework;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.ApiResults.Test.Tests;

public class ConvertTests
{
    [Test]
    public void ApiResult_Convert_WithoutObject_ShouldCopyProperties()
    {
        // Arrange
        var original = new ApiResult(200)
        {
            Metadata = new List<Metadata> { new Metadata { Type = MetadataType.Info, Data = "Test" } },
            Exceptions = new List<ExceptionData> { new ExceptionData { Message = "Error", TypeData = new TypeData { Name = "Test" } } }
        };

        // Act
        var result = original.Convert<TestViewModel>();

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(original.StatusCode));
        Assert.That(result.Metadata, Is.EqualTo(original.Metadata));
        Assert.That(result.Exceptions, Is.EqualTo(original.Exceptions));
        Assert.That(result.Message, Is.Null);
    }

    [Test]
    public void ApiResult_Convert_WithObject_ShouldCopyPropertiesAndSetMessage()
    {
        // Arrange
        var original = new ApiResult(200)
        {
            Metadata = new List<Metadata> { new Metadata { Type = MetadataType.Info, Data = "Test" } },
            Exceptions = new List<ExceptionData> { new ExceptionData { Message = "Error", TypeData = new TypeData { Name = "Test" } } }
        };
        var testViewModel = new TestViewModel { Name = "Test" };

        // Act
        var result = original.Convert(testViewModel);

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(original.StatusCode));
        Assert.That(result.Metadata, Is.EqualTo(original.Metadata));
        Assert.That(result.Exceptions, Is.EqualTo(original.Exceptions));
        Assert.That(result.Message, Is.EqualTo(testViewModel));
    }

    [Test]
    public void ApiResultWrapper_Convert_WithoutObject_ShouldCopyProperties()
    {
        // Arrange
        var original = new ApiResult<TestViewModel>(new TestViewModel { Name = "Original" }, 200)
        {
            Metadata = new List<Metadata> { new Metadata { Type = MetadataType.Info, Data = "Test" } },
            Exceptions = new List<ExceptionData> { new ExceptionData { Message = "Error", TypeData = new TypeData { Name = "Test" } } }
        };

        // Act
        var result = original.Convert<string>();

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(original.StatusCode));
        Assert.That(result.Metadata, Is.EqualTo(original.Metadata));
        Assert.That(result.Exceptions, Is.EqualTo(original.Exceptions));
        Assert.That(result.Message, Is.Null);
    }

    [Test]
    public void ApiResultWrapper_Convert_WithObject_ShouldCopyPropertiesAndSetMessage()
    {
        // Arrange
        var original = new ApiResult<TestViewModel>(new TestViewModel { Name = "Original" }, 200)
        {
            Metadata = new List<Metadata> { new Metadata { Type = MetadataType.Info, Data = "Test" } },
            Exceptions = new List<ExceptionData> { new ExceptionData { Message = "Error", TypeData = new TypeData { Name = "Test" } } }
        };
        var newMessage = "New Message";

        // Act
        var result = original.Convert(newMessage);

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(original.StatusCode));
        Assert.That(result.Metadata, Is.EqualTo(original.Metadata));
        Assert.That(result.Exceptions, Is.EqualTo(original.Exceptions));
        Assert.That(result.Message, Is.EqualTo(newMessage));
    }
}