using System;
using JWTAuthenticationEDSL12092024.Models;

namespace JWTAuthenticationEDSL12092024.Endpoints;

public static class BodegaEndpoints
{
    private static List<Bodega> bodegas = [];

    public static void AddBodegaEndpoints(this WebApplication app)
    {
        app.MapGet("/bodegas", () =>
        {
            return bodegas;
        }).RequireAuthorization();

        app.MapGet("/bodegas/{id}", (int id) =>
        {
            var bodega = bodegas.FirstOrDefault(bodega => bodega.Id == id);

            if (bodega is null) return Results.NotFound("La bodega no existe");

            return Results.Ok(bodega);
        }).RequireAuthorization();

        app.MapPost("bodegas", (Bodega bodega) =>
        {
            bool esValido = !string.IsNullOrEmpty(bodega.Nombre) && !string.IsNullOrEmpty(bodega.Descripcion);

            if (!esValido) return Results.BadRequest("Todos los campos son requeridos");

            bodega.Id = bodega.GenerateId(bodegas);

            bodegas.Add(bodega);

            return Results.Ok("Bodega creado correctamente");
        }).RequireAuthorization();

        app.MapPut("bodegas", (Bodega bodega) =>
        {
            var bodegaAEditar = bodegas.FirstOrDefault(p => p.Id == bodega.Id);

            if (bodegaAEditar is null) return Results.NotFound("La bodega no existe");

            bodegaAEditar.Nombre = !string.IsNullOrEmpty(bodega.Nombre) ? bodega.Nombre : bodegaAEditar.Nombre;
            bodegaAEditar.Descripcion = !string.IsNullOrEmpty(bodega.Descripcion) ? bodega.Descripcion : bodegaAEditar.Descripcion;

            return Results.Ok("Bodega actualizado correctamente");
        }).RequireAuthorization();

        app.MapDelete("bodegas/{id}", (int id) =>
        {
            var bodegaABorrar = bodegas.FirstOrDefault(bodega => bodega.Id == id);

            if (bodegaABorrar is null) return Results.NotFound("La bodega no existe");

            bodegas.Remove(bodegaABorrar);

            return Results.Ok("Bodega borrado correctamente");
        }).RequireAuthorization();
    }
}
