using System.Collections.Generic;
using System.Xml;

namespace XML
{
    class SAX : IParse
    {
        public List<Search> AnalizeFile(Search mySearch, string path)
        {
            List<Search> info = new List<Search>();

            XmlReader BestReader = XmlReader.Create(path);
            info.Clear();

            List<Search> result = new List<Search>();
            Search ser;
            string grou = null;
            string fac = null;

            while (BestReader.Read())
            {
                switch (BestReader.Name)
                {
                    case "faculty":
                        while (BestReader.MoveToNextAttribute())
                        {
                            if (BestReader.Name == "FCNAME")
                            {
                                fac = BestReader.Value;
                            }
                        }
                        break;
                    case "group":
                        while (BestReader.MoveToNextAttribute())
                        {
                            if (BestReader.Name == "GROUPNAME")
                            {
                                grou = BestReader.Value;
                            }
                        }
                        break;
                    case "section":
                        if (BestReader.HasAttributes)
                        {
                            ser = new Search
                            {
                                faculty = fac,
                                group = grou
                            };

                            while (BestReader.MoveToNextAttribute())
                            {
                                switch (BestReader.Name)
                                {
                                    case "COURSE":
                                        ser.yearsofstudying = BestReader.Value;
                                        break;
                                    case "NAME":
                                        ser.name = BestReader.Value;
                                        break;
                                    case "CATHEDRA":
                                        ser.cathedra = BestReader.Value;
                                        break;
                                    case "ROOM":
                                        ser.audience = BestReader.Value;
                                        break;
                                    case "FLOOR":
                                        ser.floor = BestReader.Value;
                                        break;
                                    case "NUMBER":
                                        ser.students = SplitString(BestReader.Value);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            result.Add(ser);
                        }
                        break;
                    default:
                        break;
                }
            }
            info = Filter(result, mySearch);
            return info;
        }

        private List<Search> Filter(List<Search> allRes, Search tmp)
        {
            List<Search> newResult = new List<Search>();

            if (allRes != null)
            {
                foreach (Search i in allRes)
                {
                    try
                    {
                        if ((i.faculty == tmp.faculty || tmp.faculty == null) &&
                            (i.name == tmp.name || tmp.name == null) &&
                            (i.group == tmp.group || tmp.group == null) &&
                            (i.yearsofstudying == tmp.yearsofstudying || tmp.yearsofstudying == null) &&
                            (i.cathedra == tmp.cathedra || tmp.cathedra == null) &&
                            (i.audience == tmp.audience || tmp.audience == null) &&
                            (i.floor == tmp.floor || tmp.floor == null)
                            )
                        {
                            newResult.Add(i);
                        }
                    }
                    catch { }
                }
            }
            return newResult;
        }

        private List<string> SplitString(string phrase)
        {
            string[] newStr = phrase.Split(' ');
            List<string> lst = new List<string>();
            foreach (var item in newStr)
            {
                lst.Add(item);
            }
            return lst;
        }
    }
}
