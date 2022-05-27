using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using CookBook.Infrastructure.Foundation;
using CookBook.Application.Repositories;
using CookBook.Infrastructure.Repositories;
using CookBook.Application.Queries;
using CookBook.Infrastructure.Queries;
using CookBook.Application.Entities.Users;
using Microsoft.AspNetCore.Http.Features;
using CookBook.Application.Services;
using CookBook.Api.Middleware;

var builder = WebApplication.CreateBuilder( args );

// Аутентификация и авторизация
builder.Services.AddAuthorization();
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
builder.Services.AddDbContext<CookBookDbContext>( x => x.UseSqlServer( connection, b => b.MigrationsAssembly( "CookBook.Api" ) ) );

// Контроллеры
builder.Services.AddControllers();

//DI
builder.Services.AddScoped<IRecipeQuery, RecipeQuery>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeStepRepository, RecipeStepRepository>();
builder.Services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagRecipeRepository, TagRecipeRepository>();

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
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Use( async ( context, next ) =>
{
  Console.WriteLine( context.Request.Path );
  await next.Invoke();
} );

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

// CORS
app.UseCors( builder => builder.WithOrigins( "http://localhost:4200" ) );

// http://localhost:5277/swagger/index.html
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
