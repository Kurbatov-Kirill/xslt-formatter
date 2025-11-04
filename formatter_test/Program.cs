using System;
using System.Xml;
using System.Xml.Xsl;

public class XsltTransformation
{
    public static void Main(string[] args)
    {
        string xmlFilePath = "Data1.xml";
        string xsltFilePath = "formatter.xslt";
        string outputFilePath = "Employees.xml";

        XslCompiledTransform transform = new XslCompiledTransform();
        transform.Load(xsltFilePath);

        using (XmlWriter writer = XmlWriter.Create(outputFilePath))
        {
            transform.Transform(xmlFilePath, writer);
        }

        Console.WriteLine($"Преобразование завершено. Результат сохранен в {outputFilePath}");
    }
}
