using CookBook.Api.Middleware;
using CookBook.Application.Entities.Users;
using CookBook.Application.Queries;
using CookBook.Application.Repositories;
using CookBook.Application.Services;
using CookBook.Infrastructure.Foundation;
using CookBook.Infrastructure.Queries;
using CookBook.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            ValidIssuer = AuthOptions.Issuer,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.Audience,
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
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

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
