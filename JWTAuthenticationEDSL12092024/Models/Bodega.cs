using System;

namespace JWTAuthenticationEDSL12092024.Models;

public class Bodega
{

    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }

    public int GenerateId(List<Bodega> bodegas)
    {
        if (bodegas.Count < 1) return 1;

        int id = bodegas.Max(materia => materia.Id) + 1;

        return id;
    }
}
