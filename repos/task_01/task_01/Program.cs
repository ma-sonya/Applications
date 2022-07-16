using System;
using System.IO;
using System.Xml;

namespace task_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //user chooses a folder and a template
            Console.WriteLine("Enter folder`s name: ");
            String fold = Console.ReadLine();
            Console.WriteLine("Enter folder`s template: ");
            String? temp = Console.ReadLine();


            //create xml file
            XmlDocument doc = new XmlDocument();

            //сreating main root
            XmlElement mainFolder = doc.CreateElement("Root");
            doc.AppendChild(mainFolder);
            mainFolder.SetAttribute("dir", fold);
            mainFolder.SetAttribute("template", temp);


            //creating element of founded files
            XmlElement foundedList = doc.CreateElement("FoundedFiles");
            mainFolder.AppendChild(foundedList);

            //start looking for files we need
            int n = 0;
            int finalCount = writeFolder(doc, foundedList, fold, temp, n);

            foundedList.SetAttribute("count", finalCount.ToString());

            //save xml file
            doc.Save("Test Folder" + "//result.xml");
        }
            
        private static int writeFolder(XmlDocument doc, XmlElement root, String path, String? tem, int n)
        {
            //recursion start
            String[] directories = Directory.GetDirectories(path);
            foreach (String dir in directories)
            {
                n = writeFolder(doc, root, dir, tem, n);
            }

            //checking all files for a template
            String[] files = Directory.GetFiles(path);
            foreach (String file in files)
            {
                if (file.Contains(tem))
                {
                    n++;
                    //taking a name of the current file
                    String currFile = file.Substring(file.LastIndexOf(@"\") + 1);
                    XmlElement fileElem = doc.CreateElement("File");
                    fileElem.SetAttribute("nr", n.ToString("d3"));
                    root.AppendChild(fileElem);

                    XmlElement name = doc.CreateElement("Name");
                    name.InnerText = currFile;
                    fileElem.AppendChild(name);

                    XmlElement filePath = doc.CreateElement("Path");
                    filePath.InnerText = file;
                    fileElem.AppendChild(filePath);

                    XmlElement LastModifyDate = doc.CreateElement("LastModifyDate");
                    LastModifyDate.InnerText = Directory.GetLastWriteTime(file).ToString();
                    fileElem.AppendChild(LastModifyDate);
                }

            }
            return n;
        }
    }
}