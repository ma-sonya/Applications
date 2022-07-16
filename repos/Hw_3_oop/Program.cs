using System;
using System.Collections.Generic;

namespace Hw_3_oop
{
    class Group
    {
        public string GroupName;

        public Group(string groupName)
        {
            GroupName = groupName;
        }

        public List<Student> GroupInfo = new List<Student>();

        public void AddStudent(Student st)
        {
            GroupInfo.Add(st);
        }

        public void GetInfo()
        {
            Console.WriteLine(GroupName);
            foreach (Student st in GroupInfo)
                Console.WriteLine(st.Name);
        }

        public void GetFullInfo()
        {
            Console.WriteLine(GroupName);
            foreach (Student st in GroupInfo)
            {
                Console.WriteLine(st.Name + ": " + st.State);
            }
        }


    }

    abstract class Student
    {
        public string Name { get; set; }
        public string State;

        public Student(string name)
        {
            Name = name;
            State = "";
        }

        public abstract void Study();
        public void Read()
        {
            State += "Read ";
        }
        public void Write()
        {
            State += "Write ";
        }
        public void Relax()
        {
            State += "Relax ";
        }
    }

    class BadStudent : Student
    {
        public BadStudent(string Name) : base(Name)
        {
            State = "Bad. ";
        }

        public override void Study()
        {
            for (int i = 0; i < 4; ++i)
                Relax();
            Read();
        }

    }

    class GoodStudent : Student
    {
        public GoodStudent(string Name) : base(Name)
        {
            State = "Good. ";
        }

        public override void Study()
        {
            Read();
            Write();
            Read();
            Write();
            Relax();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            GoodStudent Stud_1 = new GoodStudent("Ben");
            BadStudent Stud_2 = new BadStudent("Wlad");

            Console.WriteLine("Name of group is..");
            string name = Console.ReadLine();

            Console.WriteLine();

            Group FirstGroup = new Group(name);
            FirstGroup.AddStudent(Stud_1);
            FirstGroup.AddStudent(Stud_2);
            FirstGroup.GetInfo();

            Console.WriteLine();

            FirstGroup.GetFullInfo();
        }
    }
}
