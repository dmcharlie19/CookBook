using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using CookBookBackend.Infrastructure.Foundation;
using CookBookBackend.Application.Repositories;
using CookBookBackend.Infrastructure.Repositories;
using CookBook.Application.Queries;
using CookBook.Infrastructure.Queries;
using CookBook.Application.Entities.Users;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder( args );

// Аутентификация
builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
    .AddJwtBearer( options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        // указывает, будет ли валидироваться издатель при валидации токена
        ValidateIssuer = true,
        // строка, представляющая издателя
        ValidIssuer = AuthOptions.ISSUER,
        // будет ли валидироваться потребитель токена
        ValidateAudience = true,
        // установка потребителя токена
        ValidAudience = AuthOptions.AUDIENCE,
        // будет ли валидироваться время существования
        ValidateLifetime = true,
        // установка ключа безопасности
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        // валидация ключа безопасности
        ValidateIssuerSigningKey = true,
      };
    } );

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

builder.Services.Configure<FormOptions>( o =>
{
  o.ValueLengthLimit = int.MaxValue;
  o.MultipartBodyLengthLimit = int.MaxValue;
  o.MemoryBufferThreshold = int.MaxValue;
} );

var app = builder.Build();

app.UseAuthentication();

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
