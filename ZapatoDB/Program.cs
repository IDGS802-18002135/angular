using Microsoft.EntityFrameworkCore;
using ZapatoDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtenemos la cadena de conexion
var connectionaString = builder.Configuration.GetConnectionString("cadenaSQL");

//Agregamos la configuracion para SQLSERVER
builder.Services.AddDbContext<ZapateriaContext>(options => options.UseSqlServer(connectionaString));

//Definimos la nueva politica CORS(CROSS-ORIGIN Resource Sharing) para la API

builder.Services.AddCors(options =>
{
	options.AddPolicy("NuevaPolitica", app =>
	{
		app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
