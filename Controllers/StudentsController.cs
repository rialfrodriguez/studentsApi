using AutoMapper;
using studentsApi.Core;
using Microsoft.AspNetCore.Mvc;
using studentsApi.Core.Models;
using System.Threading.Tasks;
using studentsApi.Controllers.Resources;

namespace studentsApi.Controllers
{
    [Route("/api/students")]
    public class StudentsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IStudentRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public StudentsController(IMapper mapper, IStudentRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<QueryResultResource<StudentResource>> GetStudents(StudentQueryResource filterResource)
        {
            var filter = mapper.Map<StudentQueryResource, StudentQuery>(filterResource);
            var queryResult = await repository.GetStudentsAsync(filter);
            
           return mapper.Map<QueryResult<Student>, QueryResultResource<StudentResource>>(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await repository.GetStudentAsync(id);

            if (student == null)
                return NotFound();
            var studentResource = mapper.Map<Student, StudentResource>(student);
            return Ok(studentResource);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateStudent([FromBody]StudentResource studentResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = mapper.Map<StudentResource, Student>(studentResource);
            repository.Add(student);
            await unitOfWork.CompleteAsync();

            student = await repository.GetStudentAsync(student.StudentId);

            var result = mapper.Map<Student, StudentResource>(student);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody]StudentResource studentResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await repository.GetStudentAsync(id);

            if (student == null)
                return NotFound();

            mapper.Map<StudentResource, Student>(studentResource, student);
            await unitOfWork.CompleteAsync();

            student = await repository.GetStudentAsync(student.StudentId);

            var result = mapper.Map<Student, StudentResource>(student);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await repository.GetStudentAsync(id);
            if (student == null)
                return NotFound();

            repository.Remove(student);

            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

    }
}