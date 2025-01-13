using LibraNet.Data;
using LibraNet.Filters;
using LibraNet.Repository;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Access.Generator;
using LibraNet.Security.Tokens.Interfaces;
using LibraNet.Services;
using LibraNet.Services.Cryptography;
using LibraNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraNetContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
    assembly => assembly.MigrationsAssembly(typeof(LibraNetContext).Assembly.FullName));
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IAccessTokenGenerator>(option =>
{
    var expirationTimeMinutes = uint.Parse(builder.Configuration["Settings:Jwt:ExpirationTimeMinutes"]!);
    var signingKey = builder.Configuration["Settings:Jwt:SigningKey"];

    return new JwtTokenGenerator(expirationTimeMinutes, signingKey!);
});

builder.Services.AddScoped<IPasswordEncripter>(sp =>
{
    var additionalKey = builder.Configuration["Settings:Password:AdditionalKey"];
    return new PasswordEncripter(additionalKey!);
});

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IEditionRepository, EditionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IEditionService, EditionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
