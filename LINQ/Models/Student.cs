using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LINQ.Models
{
   public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        public string FullName { get; set; }
 


        public int KlassId { get; set; }
        public Klass klass { get; set; }


        public virtual ICollection<StudentKurs> StudentKurser { get; set; }

    }
}
