using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase(databaseName: "ProductDb")
    .Options;

var context = new AppDbContext(options);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.MapOpenApi();

app.UseHttpsRedirection();

app.MapGet("/products", () =>
{
    var products = context.Products;
    return products;
})
.WithName("GetProducts")
.WithDescription("Gets a list of all available products");

app.MapGet("/products/{id}", (int id) =>
{
    var product = context.Products.Find(id);
    if (product == null) return Results.NotFound();
    return Results.Ok(product);
})
.WithName("GetProductById")
.WithDescription("Gets a product by its id");

app.MapDelete("/products/{id}", (int id) =>
{
    var product = context.Products.Find(id);
    if (product == null) return Results.NotFound();
    context.Products.Remove(product);
    context.SaveChanges();
    return Results.NoContent();
})
.WithName("DeleteProductById")
.WithDescription("Deletes a product by its id");

app.MapPut("/products/{id}", (int id, Product input) =>
{
    var product = context.Products.Find(id);
    if (product == null) return Results.NotFound();
    product.name = input.name;
    product.description = input.description;
    product.price = input.price;
    context.SaveChanges();
    return Results.Ok(product);
})
.WithName("UpdateProductById")
.WithDescription("Updates the following attributes of a product by its id: name, description, price");


app.MapPost("/products", (Product product) =>
{
    context.Products.Add(product);
    context.SaveChanges();
    return Results.Ok(product);
})
.WithName("AddProduct")
.WithDescription("Creates a new product by the providing the following attributes: name, description, price");

app.Run();
