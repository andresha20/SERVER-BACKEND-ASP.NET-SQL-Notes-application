using Microsoft.EntityFrameworkCore;
using notesApplication.Models;
using System.Data.SqlClient;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();

var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("SQLServer"));
conStrBuilder.Password = builder.Configuration["Password"];
var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<NoteContext>(options =>
    options.UseSqlServer(connection)
);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

// Forces build

//app.MapGet("/", () => connection);
//app.MapPost("/", () => connection);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddCors();

app.UseCors(builder =>
{
    builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
