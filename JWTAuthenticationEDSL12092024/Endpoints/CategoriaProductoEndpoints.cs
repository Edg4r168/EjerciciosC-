using System;
using JWTAuthenticationEDSL12092024.Models;

namespace JWTAuthenticationEDSL12092024.Endpoints;

public static class CategoriaProductoEndpoints
{
    private static List<Categoria> categorias = [];

    public static void AddCategoriaEndpoints(this WebApplication app)
    {
        app.MapGet("/categorias", () =>
        {
            return categorias;
        });

        app.MapPost("/categorias", (Categoria categoria) =>
        {
            categorias.Add(categoria);

            return Results.Ok(categoria);
        });
    }
}
