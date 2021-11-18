using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCLass
{
    class Manage
    {
        /// <summary>
        /// список студентов
        /// </summary>
        public List<Student> list = new List<Student>();

        //список имен для рандомных студентов
        private static string[] names = {
            "Liam",
            "Olivia",
            "Noah",
            "Emma",
            "Oliver",
            "Ava",
            "William",
            "Sophia",
            "Elijah",
            "James",
            "Amelia",
            "Lucas",
            "Mia",
            "Mason",
            "Harper",
            "Ethan",
            "Evelyn"
        };

        public static string[] Names { get => names; set => names = value; }

        /// <summary>
        /// обработка ввода с клавиатуры любого количества пунктов меню или заданного интервала
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int getInt(int min, int max)
        {
            int choice = 999;
            while (choice < min || choice > max)
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());//0 - выход из программы
                    if (choice == 0) Environment.Exit(0);
                    if (choice < min || choice > max)
                    {
                        if (choice == 9)
                        {
                            //главное меню
                            Console.Clear();
                            printMainMenu();
                            return 9;
                        }
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Введите число в диапазоне от {min} до {max}: ");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Это не число...");
                }
            }
            return choice;
        }

        /// <summary>
        /// очищает список при повторном создании
        /// </summary>
        public void clearList()
        {
            this.list.Clear();
        }
      

        public void addStudent(List<Student> list)
        { 
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            //студенту больше 18 лет
            DateTime localDate = DateTime.Now;
            Console.WriteLine("Введите год рождения с 1950 по " + (localDate.Year - 18) + ":");
            int year = getInt(1950, localDate.Year - 18);

            Console.WriteLine("Введите номер группы от 1 до 4:");
            int group = getInt(1, 4);

            list.Add(new Student(name, year, group));
            Console.WriteLine("Студент добавлен");
        }

        /// <summary>
        /// Создает рандомных студентов
        /// </summary>
        public void createStudentList()
        {
            Console.WriteLine("Введите количество студентов(от 2 до 25): ");

            // номера групп
            int[] groupN = { 1, 2, 3, 4 };

            Random randomName = new Random();
            Random randomGroup = new Random();
            Random randomYear = new Random();

            //количество студентов в списке
            int q = getInt(2, 25);

            for (int i = 0; i < q; i++)
            {
                list.Add(new Student(names[randomName.Next(0, names.Length - 1)], randomYear.Next(1995, 2004), groupN[randomGroup.Next(0, 4)]));
            }
            printStudents(list);
        }


        public void deleteStudent(List<Student> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список студентов пуст");
                Console.WriteLine("--------------------- ");
            }
            else
            {
                Console.Clear();
                printStudents(list);
                Console.WriteLine(" Введите номер студента для удаления: ");

                int numberOfStudent = getInt(1, Student.Count);
                try
                {
                    //удаление студента по id, так как id может не совпасть с индексом в списке после сортировки или повторной герерации списка
                    int n = list.IndexOf(list.Find(x => x.Id == numberOfStudent));
                    list.RemoveAt(n);
                    Console.WriteLine("Студент удален");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Студента с таким номером не существует");
                }
            }
        }

        public void printStudents(List<Student> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список студентов пуст ");
                Console.WriteLine("--------------------- ");
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------ ");
                Console.WriteLine("\t№" + "\tName" + "\t    Year of birth" + "\tGroup ");
                foreach (Student s in list)
                {
                    Console.WriteLine(s.ToString());
                }
                Console.WriteLine("------------------------------------------------------ ");
            }
        }

        public void writeStudentsInFile(List<Student> list)
        {
            try
            {
                StreamWriter sw = new StreamWriter("students.txt");
                foreach (Student s in list)
                {
                    sw.WriteLine(s);
                }
                sw.Close();
            } catch(FileNotFoundException e)
            {
                Console.WriteLine("Файл не найден");
            }
        }

        public void readStudentsInFile()
        {
            try
            {
                StreamReader sr = new StreamReader("students.txt");
                List<Student> list = new List<Student>();
                string str;
                string[] buf;
                string name;
                int yob;
                int group;
                int id;
                char[] r = new char[] {'\t', ' '};
                while ((str = sr.ReadLine()) != null)
                {
                    str.Replace("\t\t", " ");
                    str.Replace("\n", " ");
                    buf = str.Split(r);
                    id = Convert.ToInt32(buf[1].Trim());
                    name = buf[2].Trim();
                    yob = Convert.ToInt32(buf[4].Trim());
                    group = Convert.ToInt32(buf[8]);
                    list.Add(new Student(name, yob, group, id));
                }
                sr.Close();
                printStudents(list);
                printMainMenu();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Файл не найден");
            }
            catch (Exception e)
            {
                Console.WriteLine("Файл не найден");
            }
        }


        /// <summary>
        /// сортировка по выбору
        /// </summary>
        /// <param name="list"></param>
        public void sortList(List<Student> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список студентов пуст ");
                Console.WriteLine("--------------------- ");
            }
            else
            {
                Console.WriteLine();
                printStudents(list);               
                Console.WriteLine("Выберите способ сортировки:");
                Console.WriteLine("1) по Id");
                Console.WriteLine("2) имени");
                Console.WriteLine("3) по году рождения");
                Console.WriteLine("4) по номеру группы");
                Console.WriteLine("9) главное меню");
                int i = getInt(1, 4);
                switch (i)
                {
                    case 1:
                        list.Sort((x, y) => x.Id.CompareTo(y.Id));
                        sortList(list);
                        break;
                    case 2:
                        list.Sort((x, y) => x.Name.CompareTo(y.Name));
                        sortList(list);
                        break;
                    case 3:
                        list.Sort((x, y) => x.Yob.CompareTo(y.Yob));
                        sortList(list);
                        break;
                    case 4:
                        list.Sort((x, y) => x.Group.CompareTo(y.Group));
                        sortList(list);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// дополнительное меню 6)
        /// </summary>
        /// <param name="list"></param>
        public void printMoreMenu (List<Student> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список студентов пуст ");
                Console.WriteLine("----------------------");
            } else {
                
                Console.Clear();
                printStudents(list);
                Console.WriteLine("");
                Console.WriteLine("1) Разница в возрасте между двумя студентами");
                Console.WriteLine("2) Найти студентов по номеру группы");
                Console.WriteLine("9) главное меню");
                int i = getInt(1, 2);
                switch (i)
                {
                    case 1:
                        diffBetweenStudent(list);
                        break;
                    case 2:
                        findStudentByGroup(list);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// поиск студентов по номеру группы
        /// </summary>
        /// <param name="list"></param>
        public void findStudentByGroup(List<Student> list)
        {            
            Console.WriteLine("Введите номер группы ");
            int i = getInt(1, 4);
            Student s = new Student(i);
            printStudents(list.FindAll(x => s.Equals(x)));
            printMainMenu();
        }
        

        /// <summary>
        /// разница в возрасте между студентами
        /// </summary>
        /// <param name="list"></param>
        public void diffBetweenStudent(List<Student> list)
        {
            Console.WriteLine("Выберите № первого студента: ");

            //индекс первого студента, поиск по id
            int i = -2; 
            do
            {
                int first = getInt(1, list.Last().Id);
                i = list.IndexOf(list.Find(x => x.Id == first));
                if (i < 0)
                {
                    Console.WriteLine("Студента с таким номером не существует");                 
                }
            } while (i < 0);


            Console.WriteLine("Выберите № второго студента: ");
            //индекс второго студента, поиск по id
            int j = -2;
            do
            {
                int second = getInt(1, list.Last().Id);
                j = list.IndexOf(list.Find(x => x.Id == second));
                if (j < 0)
                {
                    Console.WriteLine("Студента с таким номером не существует");
                }
            } while (j < 0);

            int result = list[i] - list[j];
            //int result = Math.Abs( list[i].CompareTo(list[j]));
            string s = " лет";
            if (result == 1) s = " год";
            if (result > 1 && result < 5) s = " года";
            Console.WriteLine("Разница между выбранными студентами составляет " + result + s);
            Console.ReadLine();
            printMoreMenu(list);
            
        }

              

        public void printMainMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1) Сгенерировать список студентов и вывести на экран");
            Console.WriteLine("2) Добавить студента");
            Console.WriteLine("3) Удалить студента");
            Console.WriteLine("4) Вывести на экран список студентов");
            Console.WriteLine("5) Сортировать список студентов");
            Console.WriteLine("6) Дополнительно->");
            Console.WriteLine("7) Запись студентов в файл");
            Console.WriteLine("8) Чтение студентов из файла");
            Console.WriteLine("0 - выход; 9 - главное меню, очистить консоль");
        }
    }
}
