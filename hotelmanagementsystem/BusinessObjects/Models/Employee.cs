using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public decimal? Salary { get; set; }
        public int Role { get; set; }

    }
}
