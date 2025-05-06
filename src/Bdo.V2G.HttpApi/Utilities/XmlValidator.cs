using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Bdo.V2G.Utilities;

public static class XmlValidator
{
    public static void ValidateXml(
        string xmlContent,
        string mainXsdFilename
    )
    {
        // Schemas klasörünü belirle (bin/Debug/net6.0/Schemas gibi)
        string xsdDirectory = Path.Combine(AppContext.BaseDirectory, "Schemas");
        string mainXsdPath = Path.Combine(xsdDirectory, mainXsdFilename);

        if (!File.Exists(mainXsdPath))
        {
            throw new FileNotFoundException($"Main XSD file not found: {mainXsdPath}");
        }

        // XmlSchemaSet oluştur ve XmlUrlResolver tanımla (import/include için kritik)
        var schemas = new XmlSchemaSet
        {
            XmlResolver = new XmlUrlResolver()
        };

        // Ana XSD'yi base URI ile yükle
        var xsdReaderSettings = new XmlReaderSettings
        {
            XmlResolver = new XmlUrlResolver()
        };

        using (var reader = XmlReader.Create(mainXsdPath, xsdReaderSettings, new XmlParserContext(null, null, null, XmlSpace.None)))
        {
            schemas.Add(null, reader);
        }

        // XML okuma ve validasyon ayarları
        var settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            XmlResolver = new XmlUrlResolver(),
            DtdProcessing = DtdProcessing.Ignore
        };

        settings.ValidationEventHandler += (sender, args) =>
        {
            throw new XmlSchemaValidationException(
                $"XML validation error at line {args.Exception?.LineNumber}, position {args.Exception?.LinePosition}: {args.Message}"
            );
        };

        // XML içeriğini okuyarak doğrula
        using var xmlReader = XmlReader.Create(new StringReader(xmlContent), settings);
        while (xmlReader.Read()) { /* validation while reading */ }
    }
}