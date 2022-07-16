using System.Collections.Generic;
using System.Xml;

namespace XML
{
    class DOM : IParse
    {
        readonly XmlDocument doc = new XmlDocument();

        public List<Search> AnalizeFile(Search mySearch, string path)
        {
            doc.Load(path);
            List<List<Search>> info = new List<List<Search>>();

            if (mySearch.faculty == null &&
                mySearch.name == null &&
                mySearch.group == null &&
                mySearch.yearsofstudying == null &&
                mySearch.cathedra == null &&
                mySearch.audience == null &&
                mySearch.floor == null
                )
            {
                return AllSearch(doc);
            }

            if (mySearch.faculty != null) info.Add(SearchByAttribute("faculty", "FCNAME", mySearch.faculty, doc, 0));
            if (mySearch.group != null) info.Add(SearchByAttribute("group", "GROUPNAME", mySearch.group, doc, 1));
            if (mySearch.yearsofstudying != null) info.Add(SearchByAttribute("section", "COURSE", mySearch.yearsofstudying, doc, 2));
            if (mySearch.name != null) info.Add(SearchByAttribute("section", "NAME", mySearch.name, doc, 2));
            if (mySearch.cathedra != null) info.Add(SearchByAttribute("section", "CATHEDRA", mySearch.cathedra, doc, 2));
            if (mySearch.audience != null) info.Add(SearchByAttribute("section", "ROOM", mySearch.audience, doc, 2));
            if (mySearch.floor != null) info.Add(SearchByAttribute("section", "FLOOR", mySearch.floor, doc, 2));

            return Cross(info);
        }

        public static List<Search> SearchByAttribute(string nodeName, string attribute, string myTemplate, XmlDocument doc, int n)
        {
            List<Search> find = new List<Search>();

            if (myTemplate != null || myTemplate != string.Empty)
            {
                switch (n)
                {
                    case 0:
                        {
                            XmlNodeList lst = doc.SelectNodes("//" + nodeName + "[@" + attribute + "=\"" + myTemplate + "\"]");
                            try
                            {
                                foreach (XmlNode e in lst)
                                {
                                    XmlNodeList list1 = e.ChildNodes;
                                    foreach (XmlNode el in list1)
                                    {
                                        XmlNodeList list2 = el.ChildNodes;
                                        foreach (XmlNode ell in list2)
                                        {
                                            find.Add(Info(ell));
                                        }
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    case 1:
                        {
                            XmlNodeList lst = doc.SelectNodes("//" + nodeName + "[@" + attribute + "=\"" + myTemplate + "\"]");
                            try
                            {
                                foreach (XmlNode e in lst)
                                {
                                    XmlNodeList list1 = e.ChildNodes;
                                    foreach (XmlNode el in list1)
                                    {
                                        find.Add(Info(el));
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    case 2:
                        {
                            XmlNodeList lst = doc.SelectNodes("//" + nodeName + "[@" + attribute + "=\"" + myTemplate + "\"]");
                            try
                            {
                                foreach (XmlNode e in lst)
                                {
                                    find.Add(Info(e));
                                }
                            }
                            catch { }
                            break;
                        }
                    default:
                        break;
                }
                return find;
            }
            return AllSearch(doc);
        }

        public static Search Info(XmlNode node)
        {
            Search search = new Search
            {
                faculty = node.ParentNode.ParentNode.Attributes.GetNamedItem("FCNAME").Value,
                group = node.ParentNode.Attributes.GetNamedItem("GROUPNAME").Value,
                yearsofstudying = node.Attributes.GetNamedItem("COURSE").Value,
                name = node.Attributes.GetNamedItem("NAME").Value,
                cathedra = node.Attributes.GetNamedItem("CATHEDRA").Value,
                audience = node.Attributes.GetNamedItem("ROOM").Value,
                floor = node.Attributes.GetNamedItem("FLOOR").Value,
                students = SplitString(node.Attributes.GetNamedItem("NUMBER").Value)
            };
            return search;
        }

        public static List<Search> Cross(List<List<Search>> list)
        {
            List<Search> result = new List<Search>();

            try
            {
                if (list != null)
                {
                    Search[] newSearch = list[0].ToArray();
                    if (newSearch != null)
                    {
                        foreach (Search elem in newSearch)
                        {
                            bool IsIn = true;
                            foreach (List<Search> tmpList in list)
                            {
                                if (tmpList.Count <= 0) return new List<Search>();

                                foreach (Search element in tmpList)
                                {
                                    IsIn = false;
                                    if (elem.Compare(element))
                                    {
                                        IsIn = true;
                                        break;
                                    }
                                }
                                if (!IsIn) break;
                            }
                            if (IsIn)
                            {
                                result.Add(elem);
                            }
                        }
                    }
                }
            }
            catch { }
            return result;
        }

        public static List<Search> AllSearch(XmlDocument doc)
        {
            List<Search> findAll = new List<Search>();
            XmlNodeList elem = doc.SelectNodes("//section");
            try
            {
                foreach (XmlNode el in elem)
                {
                    findAll.Add(Info(el));
                }
            }
            catch { }
            return findAll;
        }

        private static List<string> SplitString(string phrase)
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
