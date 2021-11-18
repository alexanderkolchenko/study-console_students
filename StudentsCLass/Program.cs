using System;
using System.Collections.Generic;

namespace StudentsCLass
{
    class Program
    {
        static void Main(string[] args)
        {           
            Manage m = new Manage();
            m.printMainMenu();
            int choice = m.getInt(1, 8);
            List<Student> list = m.list;

            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        m.clearList();
                        m.createStudentList();
                        m.printMainMenu();
                        break;
                    case 2:
                        m.addStudent(list);
                        m.printMainMenu();
                        break;
                    case 3:
                        m.deleteStudent(list);
                        m.printMainMenu();
                        break;
                    case 4:
                        m.printStudents(list);
                        m.printMainMenu();
                        break;
                    case 5:
                        m.sortList(list);
                        break;
                    case 6:
                        m.printMoreMenu(list);
                        break;
                    case 7:
                        m.writeStudentsInFile(list);
                        break;
                    case 8:
                        m.readStudentsInFile();
                        break;
                    case 9:                        
                        break;
                    default:
                        Console.WriteLine("Введите корректный номер меню или 0 для выхода...");
                        break;
                }
                choice = m.getInt(1, 8);
            }
        }
    }
}
