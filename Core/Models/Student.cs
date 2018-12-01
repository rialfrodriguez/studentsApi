using System;

namespace studentsApi.Core.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public DateTime StartDate { get; set; }
    }
}