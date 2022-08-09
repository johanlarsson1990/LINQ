using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LINQ.Models
{
   public class Kurs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Namn { get; set; }


        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public virtual ICollection<StudentKurs> Studentkurser { get; set; }
    }
}
