using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCLass
{
    class Student : IComparer<Student>, IComparable<Student>
    {
        static int count = 0;
        private string name;
        private int yob;
        private int group;
        private int id;

        public string Name  { get => name; }
        public static int Count { get => count; }

        public int Yob { get => yob; }

        public int Group { get => group; }
        public int Id { get => id; }

        public static int operator - (Student f, Student s)
        {
            return Math.Abs( f.Yob - s.Yob);
        }

        public static explicit operator int(Student s)
        {
            return s.Id;
        }

        public Student(string name, int yob, int group, int id) : this(name, yob, group)
        {
            this.id = id;
        }

        public Student(string name, int yob, int group)
        {
            if (name.Equals(""))
            {
                this.name = "Unknown";
            }
            else
            {
                this.name = name;
            }
            if(yob > (DateTime.Now.Year-17)||yob<(DateTime.Now.Year - 71)) {
                throw new ArgumentException ("Недопустимый возраст");
            }
            this.yob = yob;
            this.group = group;
            this.id = ++count;
        }

        public Student()
        {
            this.name = "Unknown";
            this.yob = 0;
            this.group = 0;
            this.id = ++count;
        }
        public Student(string name) : base()
        {
            this.name = name;
        }

        public Student(int group) : base()
        {
            this.group = group;
        }

        public override string ToString()
        {
            return "\t" + Id + "\t" + Name + "\t\t" + Yob + "\t\t  " + Group;
        }

        public int Compare(Student x, Student y)
        {
            return String.Compare(x.Name, y.Name);
        }

        public int CompareTo(Student other)
        {
            //return String.Compare(this.Name, other.Name);
            return this.Yob - other.Yob;
        }

      
        public override bool Equals(object obj)
        {
            return obj is Student student &&                   
                   Group == student.Group;                 
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, yob, group, id, Name, Yob, Group, Id);
        }
    }
}
