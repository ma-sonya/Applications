using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XML
{
    class XmlWriters
    {
        private readonly string pathPartXml = @"PartXml.xml"; // Part Xml FILE

        public void WriteToXml(List<Search> res)
        {            
            File.WriteAllText(pathPartXml, String.Empty);
            using (XmlWriter writer = XmlWriter.Create(pathPartXml))
            {
                writer.WriteStartDocument();
                writer.WriteRaw("\n" + @"<?xml-stylesheet type='text/xsl' href='D:\Пари\ООП\Lab#2\XML_Lab2\XSL.xsl'?>");
                writer.WriteRaw("\n");
                writer.WriteStartElement("DataBaseTable");

                foreach (Search n in res)
                {
                    string studentsList = "";
                    writer.WriteRaw("\n\t");
                    writer.WriteStartElement("faculty");
                    writer.WriteAttributeString("FCNAME", n.faculty);
                    writer.WriteRaw("\n\t\t");
                    writer.WriteStartElement("group");
                    writer.WriteAttributeString("DEPNAME", n.group);
                    writer.WriteRaw("\n\t\t\t");
                    writer.WriteStartElement("section");
                    writer.WriteAttributeString("SECNAME", n.yearsofstudying);
                    writer.WriteAttributeString("NAME", n.name);
                    writer.WriteAttributeString("CATHEDRA", n.cathedra);
                    writer.WriteAttributeString("AUDIENCE", n.audience);
                    writer.WriteAttributeString("CURRICULUM", n.floor);
                    foreach (var item in n.students)
                    {
                        studentsList += item + " ";
                    }
                    writer.WriteAttributeString("STUDENTS", studentsList);
                    writer.WriteEndElement();
                    writer.WriteRaw("\n\t\t");
                    writer.WriteEndElement();
                    writer.WriteRaw("\n\t");
                    writer.WriteEndElement();
                }
                writer.WriteRaw("\n");
                writer.WriteEndDocument();
                writer.Close();
            }
        }
    }
}
