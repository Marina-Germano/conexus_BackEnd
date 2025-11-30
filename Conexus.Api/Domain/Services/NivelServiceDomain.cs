using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class NivelServiceDomain : INivelServiceDomain
{

    private readonly AppDbContext _context;
    public NivelServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(NivelDTO nivelDTO)
    {
        if(nivelDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Nivel nivel = new Nivel();
        nivel.Idnivel = nivelDTO.Idnivel ;
        nivel.Descricao = nivelDTO.Descricao;

        await _context.Nivels.AddAsync(nivel); //adiciona o Nivel no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(nivel.Idnivel, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<NivelDTO>>> BuscarTodos()
    {
        var nivels = _context.Nivels;
        var resultNivels = await nivels.Select(e => new NivelDTO
        {
            Idnivel = e.Idnivel,
            Descricao = e.Descricao
        }).ToListAsync();

        var result = ApplicationResult<List<NivelDTO>>.Success(resultNivels, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<NivelDTO>> Atualizar(NivelDTO nivelDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (nivelDTO == null || nivelDTO.Idnivel <= 0)
        {
            return ApplicationResult<NivelDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Nivels.Where(e => e.Idnivel == nivelDTO.Idnivel).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<NivelDTO>.Failure(info.Message, 400);

        result.Idnivel = nivelDTO.Idnivel;
        result.Descricao = nivelDTO.Descricao;

        _context.Entry<Nivel>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<NivelDTO>.Success(nivelDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<NivelDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<NivelDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Nivels.Where(e => e.Idnivel == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<NivelDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Nivel>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var nivelDTO = new NivelDTO
        {
            Idnivel = result.Idnivel,
            Descricao = result.Descricao
        };

        return ApplicationResult<NivelDTO>.Success(nivelDTO, message: info.Message);
    }
    
}