using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TheNewYorkTimes;
using TheNewYorkTimes.Data;
using TheNewYorkTimes.Infrastructure.Configurations;
using TheNewYorkTimes.Interfaces.Repositories;
using TheNewYorkTimes.Interfaces.Services;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Models.InputModels;
using TheNewYorkTimes.Models.ViewModels;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.Extensions.Azure;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using FluentValidation;
using FluentValidation.Results;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsTelemetry();

//SecretClientOptions options = new()
//{
//    Retry =
//    {
//        Delay = TimeSpan.FromSeconds(2),
//        MaxDelay = TimeSpan.FromSeconds(16),
//        MaxRetries = 5,
//        Mode = RetryMode.Exponential
//    }
//};

//string kvUrl = builder.Configuration["KeyVaultConfig:KVUrl"] ?? string.Empty;
//string tenantId = builder.Configuration["KeyVaultConfig:TenantId"] ?? string.Empty;
//string clientId = builder.Configuration["KeyVaultConfig:ClientId"] ?? string.Empty;
//string clientSecret = builder.Configuration["KeyVaultConfig:ClientSecretId"] ?? string.Empty;

//var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
//var client = new SecretClient(new Uri(kvUrl), credential, options);
//KeyVaultSecret secret = client.GetSecret("ConnectionStringDataBase");
//KeyVaultSecret tokenSecret = client.GetSecret("TokenSecret");

//var connectionString = secret.Value;
//var key = Encoding.ASCII.GetBytes(tokenSecret.Value);

//if (builder.Environment.IsDevelopment())
//{
//    builder.Configuration
//        .SetBasePath(Directory.GetCurrentDirectory())
//        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
//        .Build();
//}


var connectionString = builder.Configuration.GetConnectionString("AzureConnection");
//var connectionString = builder.Configuration.GetConnectionString("LocalConnection");

builder.Services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHealthChecks(); 

builder.Services.DependencyMap();
builder.Services.ValidationMap();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header usando o schema Bearer
                       \r\n\r\n Informe 'Bearer'[space].
                        Exemplo: \'Bearer 12345abcdef\'",
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

    x.ResolveConflictingActions(x => x.First());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Keys:KeyToken"] ?? string.Empty)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("User", policy => policy.RequireRole("user"));
});

var app = builder.Build();

app.UseHealthChecks("/health"); 

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

#region Noticia

app.MapPost("/api/v1/Noticia", async (IValidator<Noticia> validators, INoticiaRepository repository, IMapper _mapper, NoticiaInputModel model) =>
{
    try
    {
        var viewModel = _mapper.Map<NoticiaViewModel>(model);
        var objModel = _mapper.Map<Noticia>(viewModel);

        ValidationResult validationResult = await validators.ValidateAsync(objModel);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        await repository.Add(objModel);

        return Results.Ok(new
        {
            message = "Noticia cadastrada com sucesso!"
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new
        {
            message = $"Erro ao cadastrar a noticia. {ex.Message}"
        });
    }
})
    .RequireAuthorization()
    .WithTags("Noticia");

app.MapGet("/api/v1/Noticias", async (INoticiaRepository repository, IMapper _mapper) =>
{
    var noticias = await repository.GetAll();

    if (noticias != null)
        return Results.Ok(noticias);

    return Results.NotFound(new
    {
        message = "Nenhuma notícia localizada."
    });
})
    .RequireAuthorization()
    .WithTags("Noticia");

app.MapGet("/api/v1/Noticia", async (INoticiaRepository repository, IMapper _mapper, int idNoticia) =>
{
    var noticia = await repository.GetById(idNoticia);

    if (noticia != null)
        return Results.Ok(noticia);

    return Results.NotFound(new
    {
        message = "Notícia não localizada."
    });
})
    .RequireAuthorization()
    .WithTags("Noticia");

#endregion

#region Usuario

app.MapPost("/api/v1/Usuario", async (IValidator<Usuario> validators, IUsuarioRepository usuarioRepository, IMapper _mapper, IHashService hashService, UsuarioInputModel model) =>
{
    try
    {
        var viewModel = _mapper.Map<UsuarioViewModel>(model);

        var objModel = _mapper.Map<Usuario>(viewModel);

        ValidationResult validationResult = await validators.ValidateAsync(objModel);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        if (await usuarioRepository.VerificaSeUsuarioExiste(objModel.Email))
            return Results.Ok(new
            {
                message = "E-mail informado ja esta cadastrado!"
            });

        objModel.Senha = hashService.CriptografarSenha(objModel.Senha);

        await usuarioRepository.Add(objModel);

        return Results.Ok(new
        {
            message = $"Usuario {objModel.Nome} cadastrado com sucesso!"
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new
        {
            message = $"Erro ao cadastrar o usuario. {ex.Message}"
        });
    }
})
    .AllowAnonymous()
    .WithTags("Usuario");

app.MapGet("/api/v1/Usuarios", async (IUsuarioRepository repository, IMapper _mapper) =>
{
    var usuarios = await repository.GetAll();

    var viewModels = _mapper.Map<List<UsuariosViewModel>>(usuarios);

    if (viewModels != null)
        return Results.Ok(viewModels);

    return Results.NotFound(new
    {
        message = "Nenhum usuário encontrado."
    });
})
    .RequireAuthorization("admin")
    .WithTags("Usuario");

app.MapGet("/api/v1/Usuario", async (IUsuarioRepository repository, IMapper _mapper, int idUsuario) =>
{
    var usuario = await repository.GetById(idUsuario);

    var viewModel = _mapper.Map<UsuariosViewModel>(usuario);

    if (viewModel != null)
        return Results.Ok(viewModel);

    return Results.NotFound(new
    {
        message = "Usuário não encontrado."
    });
})
    .RequireAuthorization("admin")
    .WithTags("Usuario");

#endregion

#region Login

app.MapPost("/api/v1/login", async (IValidator<Login> validators, IUsuarioRepository usuarioRepository, IMapper _mapper, IHashService hashService, ITokenService tokenService, LoginInputModel model) =>
{
    var viewModel = _mapper.Map<LoginViewModel>(model);
    var objModel = _mapper.Map<Login>(viewModel);

    ValidationResult validationResult = await validators.ValidateAsync(objModel);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    var usuario = await usuarioRepository.GetUsuarioByLogin(objModel.Email);

    if (usuario == null)
        return Results.NotFound(new
        {
            message = "Usuario nao cadastrado!"
        });

    if (!usuario.Ativo)
        return Results.Ok(new
        {
            message = "Usuario bloqueado!"
        });

    var senhaDigitadaCripto = hashService.CriptografarSenha(model.Senha);

    if (senhaDigitadaCripto != usuario.Senha)
        return Results.NotFound(new
        {
            message = "Usuario ou senha incorreto!"
        });

    var token = tokenService.GenereteToken(usuario);

    usuario.Senha = "";

    return Results.Ok(new
    {
        usuario.Id,
        usuario.Nome,
        usuario.Email,
        usuario.Perfil,
        token
    });
})
    .AllowAnonymous()
    .WithTags("Login");

#endregion

app.Run();
