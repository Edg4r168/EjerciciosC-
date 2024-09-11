using ControllerApiMaterias.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerApiMaterias.Controllers;

[Route("[controller]")]
[ApiController]
public class MateriaController : ControllerBase
{
    private static List<Materia> materias = new List<Materia>();

    [HttpGet]
    public IEnumerable<Materia> getAll() => materias;

    [HttpGet("{id:int}")]
    public ActionResult<Materia> getbyId(int id)
    {
        var materia = materias.FirstOrDefault(materia => materia.Id == id);

        if (materia is null) return NotFound("La materia no existe");

        return Ok(materia);
    }

    [HttpPost]
    public ActionResult<Materia> create([FromBody] Materia materia)
    {
        bool isValid = !string.IsNullOrEmpty(materia.Nombre) && !string.IsNullOrEmpty(materia.Descripcion);

        if (!isValid) return BadRequest("Todos los campos son requeridos");

        materia.Id = materia.GenerateId(materias);
        materias.Add(materia);

        return Ok(materia);
    }

    [HttpPut]
    public ActionResult<Materia> update([FromBody] Materia materia)
    {
        var materiaToUpdate = materias.FirstOrDefault(m => m.Id == materia.Id);

        if (materiaToUpdate is null) return NotFound("La materia no existe");

        materiaToUpdate.Nombre = !string.IsNullOrEmpty(materia.Nombre) ? materia.Nombre : materiaToUpdate.Nombre;
        materiaToUpdate.Descripcion = !string.IsNullOrEmpty(materia.Descripcion) ? materia.Descripcion : materiaToUpdate.Descripcion;

        return Ok("Materia actualizada correctamente");
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Materia> remove(int id)
    {
        var materia = materias.FirstOrDefault(materia => materia.Id == id);

        if (materia is null) return NotFound("La materia no existe");

        materias.Remove(materia);

        return Ok("Materia borrada correctamente");
    }
}
