using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.Models
{
   public class StudentKurs
    {
        public int ID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
