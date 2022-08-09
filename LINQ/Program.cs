using System;
using System.Collections.Generic;
using System.Linq;
using LINQ.Models;

namespace LINQ
{
    class Program
    {
        
        static void Main(string[] args)
        {

            using (var db = new TableContext())
            {
                  Init(db);

                Console.WriteLine("1. Hämta lärare programmering1." + "\n" + 
                                  "2. Skriv ut alla elever med deras lärare." + "\n" +
                                  "3. Hämta alla elever som läser programmering 1 och deras lärare." + "\n" +
                                  "4. Uppdatera ämne från programmering2 till OOP." + "\n" +
                                  "5. Uppdatera en elevs lärare i “programmering1” från Anas till Reidar.");

                string selectInput = Console.ReadLine();
                switch (selectInput)
                {
                    #region
                    case "1":
                        GetKurs();

                        GetCourseTeach(db);

                        break;
                    #endregion
                    #region
                    case "2":

                        PrintTeachNStudents(db);
                        break;
                    #endregion
                    #region
                    case "3":
                        GetProg1(db);

                        break;
                    #endregion
                    #region
                    case "4":
                        ChangeCourseName(db);

                        break;
                    #endregion
                    #region
                    case "5":

                        ChangeTeacherOnCourse(db);
                        break;
                    #endregion
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }


           
                Console.ReadKey();

            }
        }

        private static void ChangeTeacherOnCourse(TableContext db)
        {
            var courseToChange = (from k in db.Kurser
                                  join t in db.Teachers
                                  on k.TeacherID equals t.TeacherID
                                  where k.Id == 3
                                  select k).ToList();

            var selectedTeacher = (from t in db.Teachers
                                   join ti in db.Kurser
                                   on t.TeacherID equals ti.TeacherID
                                   where ti.Id == 3
                                   select new
                                   {
                                       Teacher = t.FullName
                                   });

            Console.Clear();
            var oldTeacher = "";
            foreach (var item in selectedTeacher)
            {
                oldTeacher = item.Teacher;
                Console.WriteLine($"Lärare som har kursen nu: {item.Teacher}");
                Console.WriteLine();
            }

            int newTeacher = 8;
            var aNewTeacher = (from at in db.Teachers
                               where at.TeacherID == 8
                               select new
                               {
                                   Teacher = at.FullName
                               });

            var theNewTeacher = "";
            foreach (var item in aNewTeacher)
            {
                theNewTeacher = item.Teacher.ToString();
            }

            foreach (Kurs c in courseToChange)
            {
                if (c.Id == 3)
                {
                    c.TeacherID = newTeacher;
                    // db.SaveChanges();
                    Console.WriteLine($"Ny kursansvarig är {theNewTeacher}");
                }
            }
            Console.WriteLine(new string('-', 30));
            Console.ReadLine();
            Console.Clear();
        }

        private static void ChangeCourseName(TableContext db)
        {
            var prog2 = (from c in db.Kurser
                         where c.Id == 4
                         select c).ToList();



            string newCourseName = "OOP";

            foreach (Kurs kurs in prog2)
            {
                if (kurs.Id == 4)
                {
                    kurs.Namn = newCourseName;
                    //   db.SaveChanges();
                    Console.WriteLine($"Nytt kursnamn är {kurs.Namn}");
                }
            }
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Tryck ENTER för att återgå till menyn");
            Console.ReadLine();
            Console.Clear();
        }

        private static void GetProg1(TableContext db)
        {
            var prog1 = (from sc in db.StudentKurser
                         join s in db.Studenter
                         on sc.StudentID equals s.StudentID
                         join c in db.Kurser
                         on sc.KursId equals c.Id
                         join t in db.Teachers
                         on c.TeacherID equals t.TeacherID
                         join schC in db.Klasser
                         on s.KlassId equals schC.Id
                         where sc.KursId == 3
                         select new
                         {
                             Student = s.FullName,
                             Teacher = t.FullName,
                             Kurs = c.Namn,

                         }).ToList();

            Console.Clear();

            var header = String.Format("{0,-20}{1,-20}{2,-10}\n",
                     "Elev", "Lärare", "Kurs");
            Console.WriteLine(header);
            foreach (var item in prog1)
            {
                var output = String.Format("{0,-20}{1,-20}{2,-10}",
                         item.Student, item.Teacher, item.Kurs);
                Console.WriteLine(output);
            }
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Tryck ENTER för att återgå till menyn");
            Console.ReadLine();
            Console.Clear();
        }

        private static void PrintTeachNStudents(TableContext db)
        {
            var SaT = (from sc in db.StudentKurser
                       join c in db.Kurser
                       on sc.KursId equals c.Id
                       join t in db.Teachers
                       on c.TeacherID equals t.TeacherID
                       join s in db.Studenter
                       on sc.StudentID equals s.StudentID
                       join sch in db.Klasser
                       on s.KlassId equals sch.Id
                       orderby s.KlassId
                       orderby s.StudentID
                       select new
                       {
                           Klassen = sch.KlassNamn,
                           Studenten = s.FullName,
                           Lärarn = t.FullName,
                           Kursen = c.Namn
                       }).ToList();

            var header = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}\n",
                            "Klass", "Elev", "Lärare", "Kurs");
            Console.WriteLine(header);
            foreach (var item in SaT)
            {
                var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                   item.Klassen, item.Studenten, item.Lärarn, item.Kursen);
                Console.WriteLine(output);
            }


            Console.WriteLine("Tryck ENTER för att återgå till menyn");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintStudents(IEnumerable<dynamic> studentsAndTeachers)
        {
            Console.Clear();
            var header = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}\n",
                                         "Klass", "Elev", "Lärare", "Kurs");
            var student = "";
            Console.WriteLine(header);
            foreach (var item in studentsAndTeachers)
            {
                if (student == null)
                {
                    student = item.Student;
                    var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                            item.StudentKurs, item.Student, item.Teacher, item.Kurs);
                    Console.WriteLine(output);
                }
                else if (student == item.Student)
                {
                    var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                            item.Klass, " ", item.Teacher, item.Kurs);
                    Console.WriteLine(output);

                }
                else if (student != item.Student)
                {
                    student = item.Student;
                    var output = String.Format("{0,-8}{1,-20}{2,-20}{3,-10}",
                                            item.Klass, item.Student, item.Teacher, item.Kurs);
                    Console.WriteLine(new string('-', 60));
                    Console.WriteLine(output);

                }

            }
            Console.WriteLine(new string('-', 60));
        }
        private static void GetCourseTeach(TableContext db)
        {
            int kursVal = Int32.Parse(Console.ReadLine());

            var JoinCourse = (from c in db.Kurser
                              join t in db.Teachers
                              on c.TeacherID equals t.TeacherID
                              where c.Id == kursVal
                              select new
                              {
                                  Course = c.Namn,
                                  Teacher = t.FullName
                              }).ToList();

            foreach (var t in JoinCourse)
            {
                Console.WriteLine($"{t.Teacher} undervisar i {t.Course}");
            }
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Tryck ENTER för att återgå till menyn");
            Console.ReadLine();
            Console.Clear();
        }

        private static void Init(TableContext db)
        {
            //var classNr1 = new Klass { KlassNamn = "A" };
            //var classNr2 = new Klass { KlassNamn = "B" };


            var stud1 = new Student { FullName = "Anton Johansson", KlassId = 3};
            var stud2 = new Student { FullName = "Johan Larsson", KlassId = 3};
            var stud3 = new Student { FullName = "Ida Johansson", KlassId = 4};
            var stud4 = new Student { FullName = "Wilhelm Groth", KlassId = 4};


            //var teach = new Teacher { FullName = "Anas" };
            //var teach2 = new Teacher { FullName = "Reidar" };
            //var teach3 = new Teacher { FullName = "Tobias" };


            //var kursProg1 = new Kurs { Namn = "Programming1", TeacherID = 1 };
            //var kursProg2 = new Kurs { Namn = "Programmering2", TeacherID = 2 };


            //var stuKurs = new StudentKurs { KursId = 1, StudentID = 1 };
            //var stuKurs2 = new StudentKurs { KursId = 1, StudentID = 2 };
            //var stuKurs3 = new StudentKurs { KursId = 2, StudentID = 3 };
            //var stuKurs4 = new StudentKurs { KursId = 2, StudentID = 4 };

            //db.Klasser.Add(classNr1);
            //db.Klasser.Add(classNr2);

            db.Studenter.Add(stud1);
            db.Studenter.Add(stud2);
            db.Studenter.Add(stud3);
            db.Studenter.Add(stud4);

            //db.Teachers.Add(teach);
            //db.Teachers.Add(teach2);
            //db.Teachers.Add(teach3);

            //db.Kurser.Add(kursProg1);
            //db.Kurser.Add(kursProg2);

            //db.StudentKurser.Add(stuKurs);
            //db.StudentKurser.Add(stuKurs2);
            //db.StudentKurser.Add(stuKurs3);
            //db.StudentKurser.Add(stuKurs4);
            db.SaveChanges();
        }

        private static void GetKurs()
        {
            using (TableContext context = new TableContext())
            {
                List<Kurs> kurser = (from c in context.Kurser
                                        select c).ToList();

                var header = String.Format("{0,-4}{1,-20}\n",
                                     "ID", "Namn");
                Console.WriteLine(header);
                foreach (Kurs c in kurser)
                {
                    var output = String.Format("{0,-4}{1,-20}",
                                        c.Id, c.Namn);
                    Console.WriteLine(output);
                }
                Console.WriteLine();
                Console.WriteLine("Välj kursid: ");
            }
        }
    }
}
