using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));


var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    });
    app.UseSwagger(options =>
    {
    options.SerializeAsV2 = true;
    });
    app.UseCors("MyPolicy");

app.MapGet("/", () => "ToDoAPI is running!");

app.MapGet("/items", async (ToDoDbContext dbContext) => {
    var items = await dbContext.Items.ToListAsync();
    // Debug
    // printdebug("================");
    return items;
});

app.MapGet("/items/{id}", async (int id, ToDoDbContext dbContext) => {
    var item = await dbContext.Items.FindAsync(id);
    return item;
});

app.MapPut("/items/{id}", async (int id, Item updateItem, ToDoDbContext dbContext) => {
    var existItem = await dbContext.Items.FindAsync(id);
    if(existItem is null) return Results.NotFound();

    // existItem.Name = updateItem.Name;
    existItem.IsComplete = updateItem.IsComplete;
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/items", async (Item item, ToDoDbContext dbContext) => {
    dbContext.Items.Add(item);
    await dbContext.SaveChangesAsync();
    return dbContext.Items;
    // return Results.Created("/items/${item.Id}", item);//מחזיר 201 יוצר אובייקט לאחר שהצליח החזיר 200
});

app.MapDelete("/items/{id}", async (ToDoDbContext dbContext, int id) => {   
    var item = await dbContext.Items.FindAsync(id);
    if(item!=null){
        Console.WriteLine($"Deleting item: id:{item.Id} name:{item.Name} isComplete:{item.IsComplete}");
        dbContext.Items.Remove(item);
        await dbContext.SaveChangesAsync();
    } 
    return dbContext.Items;
});

app.Run();
