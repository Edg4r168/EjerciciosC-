var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var proveedores = new List<Proveedor>();

app.MapGet("/proveedores", () =>
{
    return proveedores;
});

app.MapGet("/proveedores/{id}", (int id) =>
{
    var proveedor = proveedores.FirstOrDefault(proveedor => proveedor.Id == id);

    if (proveedor is null) return Results.NotFound("El proveedor no existe");

    return Results.Ok(proveedor);
});

app.MapPost("proveedores", (Proveedor proveedor) =>
{
    bool esValido = !string.IsNullOrEmpty(proveedor.Nombre) && !string.IsNullOrEmpty(proveedor.Direccion)
                    && !string.IsNullOrEmpty(proveedor.Telefono) && !string.IsNullOrEmpty(proveedor.Email);

    if (!esValido) return Results.BadRequest("Todos los campos son requeridos");

    proveedor.Id = Proveedor.GenerarId(proveedores);
    proveedor.FechaRegistro = DateTime.UtcNow;

    proveedores.Add(proveedor);

    return Results.Ok("Proveedor creado correctamente");
});

app.MapPut("proveedores", (Proveedor proveedor) =>
{
    var proveedorAEditar = proveedores.FirstOrDefault(p => p.Id == proveedor.Id);

    if (proveedorAEditar is null) return Results.NotFound("El proveedor no existe");

    proveedorAEditar.Nombre = !string.IsNullOrEmpty(proveedor.Nombre) ? proveedor.Nombre : proveedorAEditar.Nombre;
    proveedorAEditar.Direccion = !string.IsNullOrEmpty(proveedor.Direccion) ? proveedor.Direccion : proveedorAEditar.Direccion;
    proveedorAEditar.Telefono = !string.IsNullOrEmpty(proveedor.Telefono) ? proveedor.Telefono : proveedorAEditar.Telefono;
    proveedorAEditar.Email = !string.IsNullOrEmpty(proveedor.Email) ? proveedor.Email : proveedorAEditar.Email;

    return Results.Ok("Proveedor actualizado correctamente");
});

app.MapDelete("proveedores/{id}", (int id) =>
{
    var proveedorABorrar = proveedores.FirstOrDefault(proveedor => proveedor.Id == id);

    if (proveedorABorrar is null) return Results.NotFound("El proveedor no existe");

    proveedores.Remove(proveedorABorrar);

    return Results.Ok("Proveedor borrado correctamente");
});

app.Run();

public class Proveedor
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime FechaRegistro { get; set; }

    public static int GenerarId(List<Proveedor> proveedores)
    {
        if (proveedores.Count < 1) return 1;

        var id = proveedores.Max(p => p.Id) + 1;

        return id;
    }
}