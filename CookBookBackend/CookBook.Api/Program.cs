using Microsoft.EntityFrameworkCore;
using CookBookBackend.Infrastructure.Foundation;
using CookBookBackend.Application.Repositories;
using CookBookBackend.Infrastructure.Repositories;
using CookBook.Application.Queries;
using CookBook.Infrastructure.Queries;

var builder = WebApplication.CreateBuilder( args );

// Хранение
string connection = builder.Configuration.GetConnectionString( "DefaultConnection" );
builder.Services.AddDbContext<CookBookDbContext>( x => x.UseSqlServer( connection, b => b.MigrationsAssembly( "CookBookApi" ) ) );

// Контроллеры
builder.Services.AddControllers();

//DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRecipeQuery, RecipeQuery>();

// Swagger UI
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Use( async ( context, next ) =>
{
  Console.WriteLine( context.Request.Path );
  await next.Invoke();
} );

app.MapControllers();

// CORS
app.UseCors( builder => builder.WithOrigins( "http://localhost:4200" ) );

// http://localhost:5277/swagger/index.html
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
