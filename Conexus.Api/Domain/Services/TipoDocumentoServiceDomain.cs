using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class TipoDocumentoServiceDomain : ITipoDocumentoServiceDomain
{

    private readonly AppDbContext _context;
    public TipoDocumentoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(TipoDocumentoDTO tipoDocumentoDTO)
    {
        if(tipoDocumentoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        TipoDocumento tipoDocumento = new TipoDocumento();
        tipoDocumento.IdtipoDocumento = tipoDocumentoDTO.IdtipoDocumento ;
        tipoDocumento.Descricao = tipoDocumentoDTO.Descricao;

        await _context.TipoDocumentos.AddAsync(tipoDocumento); //adiciona o TipoDocumento no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(tipoDocumento.IdtipoDocumento, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<TipoDocumentoDTO>>> BuscarTodos()
    {
        var tipoDocumentos = _context.TipoDocumentos;
        var resultTipoDocumentos = await tipoDocumentos.Select(e => new TipoDocumentoDTO
        {
            IdtipoDocumento = e.IdtipoDocumento,
            Descricao = e.Descricao
        }).ToListAsync();

        var result = ApplicationResult<List<TipoDocumentoDTO>>.Success(resultTipoDocumentos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<TipoDocumentoDTO>> Atualizar(TipoDocumentoDTO tipoDocumentoDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (tipoDocumentoDTO == null || tipoDocumentoDTO.IdtipoDocumento <= 0)
        {
            return ApplicationResult<TipoDocumentoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.TipoDocumentos.Where(e => e.IdtipoDocumento == tipoDocumentoDTO.IdtipoDocumento).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<TipoDocumentoDTO>.Failure(info.Message, 400);

        result.IdtipoDocumento = tipoDocumentoDTO.IdtipoDocumento;
        result.Descricao = tipoDocumentoDTO.Descricao;

        _context.Entry<TipoDocumento>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<TipoDocumentoDTO>.Success(tipoDocumentoDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<TipoDocumentoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<TipoDocumentoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.TipoDocumentos.Where(e => e.IdtipoDocumento == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<TipoDocumentoDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<TipoDocumento>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var tipoDocumentoDTO = new TipoDocumentoDTO
        {
            IdtipoDocumento = result.IdtipoDocumento,
            Descricao = result.Descricao
        };

        return ApplicationResult<TipoDocumentoDTO>.Success(tipoDocumentoDTO, message: info.Message);
    }
    
}