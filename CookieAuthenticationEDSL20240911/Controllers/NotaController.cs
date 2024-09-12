using CookieAuthenticationEDSL20240911.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthenticationEDSL20240911.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private static readonly List<Nota> notas = [];

        [HttpGet]
        public List<Nota> ObtenerNotas() => notas;

        [HttpPost]
        [Authorize]
        public ActionResult<Nota> RegistrarNotas(Nota nota)
        {
            nota.Id = nota.GenerateId(notas);
            notas.Add(nota);

            return Ok("Nota registrada correctamente");
        }
    }
}
