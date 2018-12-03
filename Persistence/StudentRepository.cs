using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using studentsApi.Core;
using studentsApi.Core.Models;
using studentsApi.Extensions;

namespace studentsApi.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyDbContext context;

        public StudentRepository(MyDbContext context)
        {
            this.context = context;
        }

        public void Add(Student student)
        {
            context.Students.Add(student);
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            
            return await context.Students.SingleOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task<QueryResult<Student>> GetStudentsAsync(StudentQuery queryObj)
        {
            var result = new QueryResult<Student>();
            var query = context.Students.AsQueryable();

            var columnsMap = new Dictionary<string, Expression<Func<Student, object>>>()
            {
                ["FirstName"] = s => s.FirstName,
                ["LastName"] = s => s.LastName,
                ["School"] = s => s.School
            };

            query = query.ApplyOrdering(queryObj, columnsMap);
            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public void Remove(Student student)
        {
            context.Students.Remove(student);
        }
    }
}