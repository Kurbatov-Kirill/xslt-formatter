using System;
using System.Xml;
using System.Xml.Xsl;
using System.Globalization;

public class XsltTransformation
{
    public static void Main(string[] args)
    {
        double employeeAmount = 0;
        string employeeAmountStr = "0";
        double overallAmount = 0;
        string overallAmountStr = "0";
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
                employeeAmount = 0;
                employeeAmountStr = "0";

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    XmlNode? amount = childnode.Attributes.GetNamedItem("amount");
                    string tempStr = amount.InnerText;
                    tempStr = tempStr.Replace(",", ".");
                    Console.WriteLine(employeeAmount + " + " + double.Parse(tempStr, CultureInfo.InvariantCulture) + " = " + (employeeAmount + double.Parse(tempStr, CultureInfo.InvariantCulture)));
                    employeeAmount += double.Parse(tempStr, CultureInfo.InvariantCulture);
                }
                Console.WriteLine();

                overallAmount += employeeAmount;
                Console.WriteLine("Overall: " + overallAmount + " + " + employeeAmount + " = " + (overallAmount + employeeAmount));
                employeeAmountStr = employeeAmount.ToString();
                XmlAttribute sum = xDoc.CreateAttribute("sum");
                XmlText amountVal = xDoc.CreateTextNode(employeeAmountStr);
                sum.AppendChild(amountVal);
                XmlNode? attr = xnode.Attributes.Append(sum);
            }
        }

        xDoc.Save("Employees.xml");
        xDoc.Load("Data1.xml");

        XmlElement? xRootS = xDoc.DocumentElement;

        if (xRootS != null)
        {
            overallAmountStr = overallAmount.ToString();
            XmlAttribute overall = xDoc.CreateAttribute("overall");
            XmlText overalAmountVal = xDoc.CreateTextNode(overallAmountStr);
            overall.AppendChild(overalAmountVal);
            XmlNode? attr = xRootS.Attributes.Append(overall);
        }

        xDoc.Save("Data1.xml");

        Console.WriteLine($"Преобразование завершено. Результат сохранен в {outputFilePath}");
    }
}
