using LibraNet.Data;
using LibraNet.Filters;
using LibraNet.Repository;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens;
using LibraNet.Security.Tokens.Access.Generator;
using LibraNet.Security.Tokens.Access.Validator;
using LibraNet.Security.Tokens.Interfaces;
using LibraNet.Services;
using LibraNet.Services.Cryptography;
using LibraNet.Services.Interfaces;
using LibraNet.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

    var userRepository = option.GetRequiredService<IUserRepository>();
    
    return new JwtTokenGenerator(expirationTimeMinutes, signingKey!, userRepository);
});
builder.Services.AddScoped<IAccessTokenValidator>(option =>
{
    var signingKey = builder.Configuration["Settings:Jwt:SigningKey"];
    return new JwtTokenValidator(signingKey!);
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
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IEditionService, EditionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<ILoggedUser, LoggedUser>();
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer sheme.
                  Enter 'Bearer' [space] and then your token in the text input below.
                  Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraNetContext>();
    var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    await DbInitializer.InitializeAsync(context, passwordEncripter, configuration);
}

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
