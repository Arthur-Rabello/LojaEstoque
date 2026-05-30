using DotNetEnv;
using LojaEstoque.Aplicacao.Aplic;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Dominio.Services;
using LojaEstoque.Repositories.Contexto;
using LojaEstoque.Repositories.Interfaces;
using LojaEstoque.Repositories.Reps;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAplicCarrinho, AplicCarrinho>();
builder.Services.AddScoped<IServCarrinho, ServCarrinho>();
builder.Services.AddScoped<IRepCarrinho, RepCarrinho>();
builder.Services.AddScoped<CarrinhoValidator>();
builder.Services.AddScoped<IAplicProduto, AplicProduto>();
builder.Services.AddScoped<IServProduto, ServProduto>();
builder.Services.AddScoped<IRepProduto, RepProduto>();
builder.Services.AddScoped<ProdutoValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();