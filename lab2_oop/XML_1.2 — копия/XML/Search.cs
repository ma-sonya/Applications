using System.Collections.Generic;

namespace XML
{
    public class Search
    {
        #region init

        public string name = null;
        public string faculty = null;
        public string group = null;
        public string yearsofstudying = null;
        public string cathedra = null;
        public string audience = null;
        public string floor = null;
        public List<string> students = null;

        #endregion init 
        public Search() { }
        public Search(string[] data)
        {
            name = data[0];
            group = data[1];
            yearsofstudying = data[2];
            cathedra = data[3];
            audience = data[4];
            floor = data[5];
            faculty = data[6];
            students = new List<string>();
        }
        #region Comparison
        public bool Compare(Search obj)
        {
            if ((this.name == obj.name)
                && (this.faculty == obj.faculty)
                && (this.group == obj.group)
                && (this.yearsofstudying == obj.yearsofstudying)
                && (this.cathedra == obj.cathedra)
                && (this.audience == obj.audience)
                && (this.floor == obj.floor)
                && (this.students == obj.students))
            {
                return true;
            }
            else
                return false;
        }
        #endregion Comparison
    }
}
