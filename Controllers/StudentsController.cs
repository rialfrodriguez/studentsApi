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

        /// <summary>
        /// Retornar la lista de estudiantes
        /// </summary>
        /// <returns>Datos del estudiante</returns>
        [HttpGet]
        public async Task<QueryResultResource<StudentResource>> GetStudents(StudentQueryResource filterResource)
        {
            var filter = mapper.Map<StudentQueryResource, StudentQuery>(filterResource);
            var queryResult = await repository.GetStudentsAsync(filter);
            
           return mapper.Map<QueryResult<Student>, QueryResultResource<StudentResource>>(queryResult);
        }

        /// <summary>
        /// Retornar el estudiante mediante el parametro ID
        /// </summary>
        /// <param name="id">ID del estudiante</param>
        /// <returns>Datos del estudiante</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await repository.GetStudentAsync(id);

            if (student == null)
                return NotFound();
            var studentResource = mapper.Map<Student, StudentResource>(student);
            return Ok(studentResource);
        }

        /// <summary>
        /// Insertar el objeto estudiante
        /// </summary>
        /// <returns>Datos del estudiante</returns>
        [HttpPost()]
        public async Task<IActionResult> CreateStudent([FromBody]StudentResource studentResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = mapper.Map<StudentResource, Student>(studentResource);
            repository.Add(student);
            await unitOfWork.CompleteAsync();

            // student = await repository.GetStudentAsync(student.StudentId);
            // var result = mapper.Map<Student, StudentResource>(student);

            // retornamos 201
            return CreatedAtAction(nameof(GetStudent), new {id = student.StudentId}, student);
        }

        /// <summary>
        /// Actualizar los datos del estudiante
        /// </summary>
        /// <param name="id">ID del estudiante</param>
        /// <returns>Datos del estudiante actualizado</returns>
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

        /// <summary>
        /// Eliminar un estudiante
        /// </summary>
        /// <param name="id">ID del estudiante</param>
        /// <returns>Id del estudiante eliminado</returns>
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