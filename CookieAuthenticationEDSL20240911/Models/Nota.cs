using System;

namespace CookieAuthenticationEDSL20240911.Models;

public class Nota
{
    public int Id { get; set; }
    public string? Estudiante { get; set; }
    public string? Materia { get; set; }
    public decimal Calificacion { get; set; }

    public int GenerateId(List<Nota> notas)
    {
        if (notas.Count < 1) return 1;

        int id = notas.Max(m => m.Id) + 1;

        return id;
    }
}
