using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Bdo.V2G.Utilities;

public static class XmlValidator
{
    public static void ValidateXml(string xmlContent, string xsdPath)
    {
        var settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            if (args.Severity == XmlSeverityType.Error)
                throw new System.Exception(args.Message);
        };

        using var stringReader = new StringReader(xmlContent);
        using var xmlReader = XmlReader.Create(stringReader, settings);
        while (xmlReader.Read()) { }
    }
}