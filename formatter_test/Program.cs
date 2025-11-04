using System;
using System.Xml;
using System.Xml.Xsl;
using System.Globalization;

public class XsltTransformation
{
    public static void Main(string[] args)
    {
        double overallAmount = 0;
        string overallAmountStr = "";
        string xmlFilePath = "Data1.xml";
        string xsltFilePath = "formatter.xslt";
        string outputFilePath = "Employees.xml";

        XslCompiledTransform transform = new XslCompiledTransform();
        transform.Load(xsltFilePath);

        using (XmlWriter writer = XmlWriter.Create(outputFilePath))
        {
            transform.Transform(xmlFilePath, writer);
        }

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Employees.xml");
        XmlElement? xRoot = xDoc.DocumentElement;
        if (xRoot != null)
        {
            foreach (XmlElement xnode in xRoot)
            {
                overallAmount = 0;
                overallAmountStr = "0";

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    XmlNode? amount = childnode.Attributes.GetNamedItem("amount");
                    string tempStr = amount.InnerText;
                    tempStr = tempStr.Replace(",", ".");
                    Console.WriteLine(overallAmount + " + " + double.Parse(tempStr, CultureInfo.InvariantCulture) + " = " + (overallAmount + double.Parse(tempStr, CultureInfo.InvariantCulture)));
                    overallAmount += double.Parse(tempStr, CultureInfo.InvariantCulture);
                }
                Console.WriteLine();

                overallAmountStr = overallAmount.ToString();
                XmlAttribute sum = xDoc.CreateAttribute("sum");
                XmlText amountVal = xDoc.CreateTextNode(overallAmountStr);
                sum.AppendChild(amountVal);
                XmlNode? attr = xnode.Attributes.Append(sum);
            }
        }
        
        xDoc.Save("Employees.xml");

        Console.WriteLine($"Преобразование завершено. Результат сохранен в {outputFilePath}");
    }
}
