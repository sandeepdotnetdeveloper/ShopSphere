using ShopSphere.Api.Models;
using ShopSphere.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ProductCatalog>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Health endpoint
app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        message = "ShopSphere API is running."
    });
});

// Get all products
app.MapGet("/api/products", (ProductCatalog catalog) =>
{
    return Results.Ok(catalog.GetAll());
});

// Get product by ID
app.MapGet("/api/products/{id:int}",
    (int id, ProductCatalog catalog) =>
    {
        var product = catalog.GetById(id);

        return product is null
            ? Results.NotFound()
            : Results.Ok(product);
    });

// Add product
app.MapPost("/api/products",
    (Product product, ProductCatalog catalog) =>
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return Results.BadRequest(
                new { message = "Product name is required." });
        }

        if (product.Price <= 0)
        {
            return Results.BadRequest(
                new { message = "Price must be greater than zero." });
        }

        var createdProduct = catalog.Add(product);

        return Results.Created(
            $"/api/products/{createdProduct.Id}",
            createdProduct);
    });

// Delete product
app.MapDelete("/api/products/{id:int}",
    (int id, ProductCatalog catalog) =>
    {
        return catalog.Delete(id)
            ? Results.NoContent()
            : Results.NotFound();
    });

app.Run();

