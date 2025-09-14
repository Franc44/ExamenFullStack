using Microsoft.EntityFrameworkCore;
using ExamenBackEnd.API.Data;
using ExamenBackEnd.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Añadimos la configuracion de la conexión a base de datos con EntityFramework
builder.Services.AddDbContext<ExamenBackEndDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestLocal")));

//Registrar los servicios (inyeccion de dependencias del CRUD  de Tarea)
builder.Services.AddScoped<ITareaService, TareaService>();

//Agregamos la configuracion de CORS para el fronted
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swg =>
{
    swg.SwaggerDoc("v1", new() { Title = "Examen BackEnd API", Version = "V1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontEnd");
app.UseAuthorization();
app.MapControllers();

//Se crea la base de datos si es que no existe
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ExamenBackEndDbContext>();
    context.Database.EnsureCreated();
}

//Se inicializa la API
app.Run();
