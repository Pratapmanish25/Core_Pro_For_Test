using System;
using System.Collections.Generic;

#nullable disable

namespace Core_Pro_For_Test.DB_Context
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
