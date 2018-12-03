using System;
using System.Linq;
using studentsApi.Persistence;

namespace studentsApi.Core.Models
{
    public class DummyData
    {
        public static void Initialize(MyDbContext db)
        {
            if (!db.Students.Any())
            {
                db.Students.Add(new Student
                {
                    FirstName = "Ricardo",
                    LastName = "Rodriguez",
                    School = "Ingenieria",
                    StartDate = Convert.ToDateTime("2018/12/01")
                });
                db.Students.Add(new Student
                {
                    FirstName = "Alfredo",
                    LastName = "Ojeda",
                    School = "Licenciatura",
                    StartDate = Convert.ToDateTime("2018/12/01")
                });
                db.SaveChanges();
            }

        }
    }
}