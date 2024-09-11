namespace ControllerApiMaterias.Models;

public class Materia
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }

    public int GenerateId(List<Materia> materias)
    {
        if (materias.Count < 1) return 1;

        int id = materias.Max(materia => materia.Id) + 1;

        return id;
    }
}
