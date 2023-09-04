using System.Text;

namespace Pepegov.MicroserviceFramework.AspNetCore.Infrastructure;

public sealed class StringWriterWithEncoding : StringWriter
{
    public override Encoding Encoding { get; }

    public StringWriterWithEncoding (Encoding encoding)
    {
        Encoding = encoding;
    }

    public StringWriterWithEncoding(Encoding encoding, IFormatProvider? formatProvider) : base(formatProvider)  
    {
        Encoding = encoding;
    }    
    
    public StringWriterWithEncoding (Encoding encoding, StringBuilder stringBuilder) : base(stringBuilder)
    {
        Encoding = encoding;
    }    
    
    public StringWriterWithEncoding (Encoding encoding, StringBuilder stringBuilder, IFormatProvider? formatProvider) : base(stringBuilder, formatProvider)
    {
        Encoding = encoding;
    }    
}