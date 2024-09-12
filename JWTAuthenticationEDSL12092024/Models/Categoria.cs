using System;

namespace JWTAuthenticationEDSL12092024.Models;

public class Categoria
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }

    public int GenerateId(List<Categoria> categorias)
    {
        if (categorias.Count < 1) return 1;

        int id = categorias.Max(materia => materia.Id) + 1;

        return id;
    }
}
