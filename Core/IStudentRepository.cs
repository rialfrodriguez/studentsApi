using System.Threading.Tasks;
using studentsApi.Core.Models;
using studentsApi.Extensions;
using System.Collections.Generic;

namespace studentsApi.Core
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentAsync(int id);
        void Add(Student student);
        void Remove(Student student);
        Task<QueryResult<Student>> GetStudentsAsync(StudentQuery filter);
    }
}