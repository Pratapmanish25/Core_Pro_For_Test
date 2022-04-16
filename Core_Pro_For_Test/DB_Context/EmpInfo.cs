using System;
using System.Collections.Generic;

#nullable disable

namespace Core_Pro_For_Test.DB_Context
{
    public partial class EmpInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public int? Contact { get; set; }
        public string City { get; set; }
        public int? Salary { get; set; }
    }
}
