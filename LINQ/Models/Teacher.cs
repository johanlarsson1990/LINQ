using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LINQ.Models
{
   public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string FullName { get; set; }


        public ICollection<Kurs> Kurser { get; set; }

    }
}

