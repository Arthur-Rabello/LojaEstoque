using DotNetEnv;
using LojaEstoque.Api.Middlewares;
using LojaEstoque.Aplicacao.Aplic;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Dominio.Services;
using LojaEstoque.Repositories.Contexto;
using LojaEstoque.Repositories.Interfaces;
using LojaEstoque.Repositories.Reps;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

Env.Load();


var builder = WebApplication.CreateBuilder(args);

string postgresHost = Environment.GetEnvironmentVariable("POSTGRES_HOST");
string postgresPort = Environment.GetEnvironmentVariable("POSTGRES_PORT");
string postgresDatabase = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");
string postgresUsername = Environment.GetEnvironmentVariable("POSTGRES_USERNAME");
string postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
string postgresSslMode = Environment.GetEnvironmentVariable("POSTGRES_SSL_MODE");

string connectionString = $"Host={postgresHost};Port={postgresPort};Database={postgresDatabase};Username={postgresUsername};Password={postgresPassword};SSL Mode={postgresSslMode};Trust Server Certificate=true";

builder.Services.AddDbContext<LojaContext>(options =>
{
    options.UseNpgsql(connectionString);
});

string jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddScoped<IAplicCarrinho, AplicCarrinho>();
builder.Services.AddScoped<IServCarrinho, ServCarrinho>();
builder.Services.AddScoped<IRepCarrinho, RepCarrinho>();
builder.Services.AddScoped<CarrinhoValidator>();
builder.Services.AddScoped<IAplicProduto, AplicProduto>();
builder.Services.AddScoped<IServProduto, ServProduto>();
builder.Services.AddScoped<IRepProduto, RepProduto>();
builder.Services.AddScoped<ProdutoValidator>();
builder.Services.AddScoped<IAplicUsuario, AplicUsuario>();
builder.Services.AddScoped<IServUsuario, ServUsuario>();
builder.Services.AddScoped<IRepUsuario, RepUsuario>();
builder.Services.AddScoped<UsuarioValidator>();
builder.Services.AddScoped<IServToken, ServToken>();
builder.Services.AddScoped<LoginValidator>();
builder.Services.AddScoped<IServLogin, ServLogin>();
builder.Services.AddScoped<IAplicLogin, AplicLogin>();
builder.Services.AddScoped<UsuarioEditarValidator>();
builder.Services.AddScoped<IServSenha, ServSenha>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();