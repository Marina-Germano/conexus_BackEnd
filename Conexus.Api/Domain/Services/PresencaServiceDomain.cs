using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class PresencaServiceDomain : IPresencaServiceDomain
{

    private readonly AppDbContext _context;
    public PresencaServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(PresencaDTO presencaDTO)
    {
        if(presencaDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Presenca presenca = new Presenca();
        presenca.Idpresenca = presencaDTO.Idpresenca ;
        presenca.IdalunoTurma = presencaDTO.IdalunoTurma;
        presenca.Idfuncionario = presencaDTO.Idfuncionario;
        presenca.Presente = presencaDTO.Presente;
        presenca.Data = presencaDTO.Data;

        await _context.Presencas.AddAsync(presenca); //adiciona o Presenca no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(presenca.Idpresenca, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<PresencaDTO>>> BuscarTodos()
    {
        var presencas = _context.Presencas;
        var resultPresencas = await presencas.Select(e => new PresencaDTO
        {
            Idpresenca = e.Idpresenca,
            IdalunoTurma = e.IdalunoTurma,
            Idfuncionario = e.Idfuncionario,
            Presente = e.Presente,
            Data = e.Data
        }).ToListAsync();

        var result = ApplicationResult<List<PresencaDTO>>.Success(resultPresencas, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<PresencaDTO>> Atualizar(PresencaDTO presencaDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (presencaDTO == null || presencaDTO.Idpresenca <= 0)
        {
            return ApplicationResult<PresencaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Presencas.Where(e => e.Idpresenca == presencaDTO.Idpresenca).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<PresencaDTO>.Failure(info.Message, 400);

        result.Idpresenca = presencaDTO.Idpresenca;
        result.IdalunoTurma = presencaDTO.IdalunoTurma;
        result.Idfuncionario = presencaDTO.Idfuncionario;
        result.Presente = presencaDTO.Presente;
        result.Data = presencaDTO.Data;

        _context.Entry<Presenca>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<PresencaDTO>.Success(presencaDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<PresencaDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<PresencaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Presencas.Where(e => e.Idpresenca == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<PresencaDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Presenca>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var presencaDTO = new PresencaDTO
        {
            Idpresenca = result.Idpresenca,
            IdalunoTurma = result.IdalunoTurma,
            Idfuncionario = result.Idfuncionario,
            Presente = result.Presente,
            Data = result.Data
        };

        return ApplicationResult<PresencaDTO>.Success(presencaDTO, message: info.Message);
    }
    
}