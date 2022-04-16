using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Pro_For_Test.Models
{
    public class Employee
    {
        public int Id { get; set; }
       
        [Required ]

        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public int? Contact { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int? Salary { get; set; }
        
    }
}
