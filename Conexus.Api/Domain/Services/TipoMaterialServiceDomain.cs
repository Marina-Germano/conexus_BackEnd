using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class TipoMaterialServiceDomain : ITipoMaterialServiceDomain
{

    private readonly AppDbContext _context;
    public TipoMaterialServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(TipoMaterialDTO TipoMaterialDTO)
    {
        if(TipoMaterialDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        TipoMaterial TipoMaterial = new TipoMaterial();
        TipoMaterial.IdtipoMaterial = TipoMaterialDTO.IdtipoMaterial ;
        TipoMaterial.Descricao = TipoMaterialDTO.Descricao;

        await _context.TipoMaterials.AddAsync(TipoMaterial); //adiciona o TipoMaterial no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(TipoMaterial.IdtipoMaterial, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<TipoMaterialDTO>>> BuscarTodos()
    {
        var TipoMaterials = _context.TipoMaterials;
        var resultTipoMaterials = await TipoMaterials.Select(e => new TipoMaterialDTO
        {
            IdtipoMaterial = e.IdtipoMaterial,
            Descricao = e.Descricao

        }).ToListAsync();

        var result = ApplicationResult<List<TipoMaterialDTO>>.Success(resultTipoMaterials, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<TipoMaterialDTO>> Atualizar(TipoMaterialDTO TipoMaterialDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (TipoMaterialDTO == null || TipoMaterialDTO.IdtipoMaterial <= 0)
        {
            return ApplicationResult<TipoMaterialDTO>.Failure(info.Message, 400);
        }

        var result = await _context.TipoMaterials.Where(e => e.IdtipoMaterial == TipoMaterialDTO.IdtipoMaterial).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<TipoMaterialDTO>.Failure(info.Message, 400);

        result.IdtipoMaterial = TipoMaterialDTO.IdtipoMaterial;
        result.Descricao = TipoMaterialDTO.Descricao;

        _context.Entry<TipoMaterial>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<TipoMaterialDTO>.Success(TipoMaterialDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<TipoMaterialDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<TipoMaterialDTO>.Failure(info.Message, 400);
        }

        var result = await _context.TipoMaterials.Where(e => e.IdtipoMaterial == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<TipoMaterialDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<TipoMaterial>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var TipoMaterialDTO = new TipoMaterialDTO
        {
            IdtipoMaterial = result.IdtipoMaterial,
            Descricao = result.Descricao
        };

        return ApplicationResult<TipoMaterialDTO>.Success(TipoMaterialDTO, message: info.Message);
    }
    
}