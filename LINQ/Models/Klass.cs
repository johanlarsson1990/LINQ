using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LINQ.Models
{
  public  class Klass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string KlassNamn { get; set; }

        public virtual ICollection<Student> Student { get; set; }

        public List<Student> GetStudents()
        {
            List<Student> listToReturn = new List<Student>();

            foreach (var item in Student)
            {
                listToReturn.Add(item);
            }
            return listToReturn;
        }
    }
}
