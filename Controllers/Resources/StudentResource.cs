using System;

namespace studentsApi.Controllers.Resources
{
    public class StudentResource
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public DateTime StartDate { get; set; }
    }
}