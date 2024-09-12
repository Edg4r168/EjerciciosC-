using System;

namespace CookieAuthenticationEDSL20240911.Models;

public class Matricula
{
    public int Id { get; set; }
    public string? Estudiante { get; set; }
    public string? Curso { get; set; }
    public string? FechaMatricula { get; set; }
    public string? Periodo { get; set; }

    public int GenerateId(List<Matricula> matriculas)
    {
        if (matriculas.Count < 1) return 1;

        int id = matriculas.Max(m => m.Id) + 1;

        return id;
    }
}
