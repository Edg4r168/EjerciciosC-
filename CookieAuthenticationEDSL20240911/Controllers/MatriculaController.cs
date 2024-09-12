using CookieAuthenticationEDSL20240911.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthenticationEDSL20240911.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MatriculaController : ControllerBase
    {
        private static List<Matricula> matriculas = [];

        [HttpGet]
        public IEnumerable<Matricula> GetAll() => matriculas;

        [HttpGet("{id:int}")]
        public ActionResult<Matricula> GetbyId(int id)
        {
            var matricula = matriculas.FirstOrDefault(matricula => matricula.Id == id);

            if (matricula is null) return NotFound("La matricula no existe");

            return Ok(matricula);
        }

        [HttpPost]
        public ActionResult<Matricula> Create([FromBody] Matricula matricula)
        {
            bool isValid = !string.IsNullOrEmpty(matricula.Estudiante) && !string.IsNullOrEmpty(matricula.Curso)
                            && !string.IsNullOrEmpty(matricula.FechaMatricula) && !string.IsNullOrEmpty(matricula.Periodo);

            if (!isValid) return BadRequest("Todos los campos son requeridos");

            matricula.Id = matricula.GenerateId(matriculas);
            matriculas.Add(matricula);

            return Ok(matricula);
        }

        [HttpPut]
        public ActionResult<Matricula> Update([FromBody] Matricula matricula)
        {
            var matriculaToUpdate = matriculas.FirstOrDefault(m => m.Id == matricula.Id);

            if (matriculaToUpdate is null) return NotFound("La matricula no existe");

            matriculaToUpdate.Estudiante = !string.IsNullOrEmpty(matricula.Estudiante) ? matricula.Estudiante : matriculaToUpdate.Estudiante;
            matriculaToUpdate.Curso = !string.IsNullOrEmpty(matricula.Curso) ? matricula.Curso : matriculaToUpdate.Curso;
            matriculaToUpdate.FechaMatricula = !string.IsNullOrEmpty(matricula.FechaMatricula) ? matricula.FechaMatricula : matriculaToUpdate.FechaMatricula;
            matriculaToUpdate.Periodo = !string.IsNullOrEmpty(matricula.Periodo) ? matricula.Periodo : matriculaToUpdate.Periodo;

            return Ok("Matricula actualizada correctamente");
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Matricula> Remove(int id)
        {
            var matricula = matriculas.FirstOrDefault(matricula => matricula.Id == id);

            if (matricula is null) return NotFound("La matricula no existe");

            matriculas.Remove(matricula);

            return Ok("Matricula borrada correctamente");
        }
    }
}
