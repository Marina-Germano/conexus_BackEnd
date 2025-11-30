using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class IdiomaServiceDomain : IIdiomaServiceDomain
{

    private readonly AppDbContext _context;
    public IdiomaServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(IdiomaDTO idiomaDTO)
    {
        if(idiomaDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Idioma idioma = new Idioma();
        idioma.Ididioma = idiomaDTO.Ididioma ;
        idioma.Descricao = idiomaDTO.Descricao;

        await _context.Idiomas.AddAsync(idioma); //adiciona o Idioma no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(idioma.Ididioma, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<IdiomaDTO>>> BuscarTodos()
    {
        var idiomas = _context.Idiomas;
        var resultIdiomas = await idiomas.Select(e => new IdiomaDTO
        {
            Ididioma = e.Ididioma,
            Descricao = e.Descricao
        }).ToListAsync();

        var result = ApplicationResult<List<IdiomaDTO>>.Success(resultIdiomas, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<IdiomaDTO>> Atualizar(IdiomaDTO idiomaDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (idiomaDTO == null || idiomaDTO.Ididioma <= 0)
        {
            return ApplicationResult<IdiomaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Idiomas.Where(e => e.Ididioma == idiomaDTO.Ididioma).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<IdiomaDTO>.Failure(info.Message, 400);

        result.Ididioma = idiomaDTO.Ididioma;
        result.Descricao = idiomaDTO.Descricao;

        _context.Entry<Idioma>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<IdiomaDTO>.Success(idiomaDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<IdiomaDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<IdiomaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Idiomas.Where(e => e.Ididioma == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<IdiomaDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Idioma>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var idiomaDTO = new IdiomaDTO
        {
            Ididioma = result.Ididioma,
            Descricao = result.Descricao
        };

        return ApplicationResult<IdiomaDTO>.Success(idiomaDTO, message: info.Message);
    }
    
}