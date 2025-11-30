using Conexus.Api.Domain.Services;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;         // ✅ necessário
using Microsoft.Extensions.DependencyInjection;  // ✅ necessário
using Microsoft.Extensions.Hosting;         // ✅ necessário



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger NSwag
builder.Services.AddOpenApiDocument();

// Injeção de dependência
builder.Services.AddScoped<IAlunoServiceDomain, AlunoServiceDomain>();
builder.Services.AddScoped<IAlunoTurmaServiceDomain, AlunoTurmaServiceDomain>();
builder.Services.AddScoped<IAvaliacaoServiceDomain, AvaliacaoServiceDomain>();
builder.Services.AddScoped<ICalendarioAulaServiceDomain, CalendarioAulaServiceDomain>();
builder.Services.AddScoped<ICartaoServiceDomain, CartaoServiceDomain>();
builder.Services.AddScoped<IContatoServiceDomain, ContatoServiceDomain>();
builder.Services.AddScoped<IDocumentoAlunoServiceDomain, DocumentoAlunoServiceDomain>();
builder.Services.AddScoped<IEmprestimoMaterialServiceDomain, EmprestimoMaterialServiceDomain>();
builder.Services.AddScoped<IFormaPagamentoServiceDomain, FormaPagamentoServiceDomain>();
builder.Services.AddScoped<IFuncionarioServiceDomain, FuncionarioServiceDomain>();
builder.Services.AddScoped<IIdiomaServiceDomain, IdiomaServiceDomain>();
builder.Services.AddScoped<IMaterialServiceDomain, MaterialServiceDomain>();
builder.Services.AddScoped<INivelServiceDomain, NivelServiceDomain>();
builder.Services.AddScoped<IPagamentoServiceDomain, PagamentoServiceDomain>();
builder.Services.AddScoped<IPresencaServiceDomain, PresencaServiceDomain>();
builder.Services.AddScoped<ITipoDocumentoServiceDomain, TipoDocumentoServiceDomain>();
builder.Services.AddScoped<ITipoMaterialServiceDomain, TipoMaterialServiceDomain>();
builder.Services.AddScoped<ITurmaServiceDomain, TurmaServiceDomain>();
builder.Services.AddScoped<IUsuarioServiceDomain, UsuarioServiceDomain>();

// String de conexão
var strConn = "server=localhost;database=escola_idiomas;user=root;password=";

// MySQL AutoDetect
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(strConn, ServerVersion.AutoDetect(strConn))
);

var app = builder.Build();

// Middleware
app.UseRouting();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapControllers();

app.Run();
