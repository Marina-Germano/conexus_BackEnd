using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class AlunoTurmaServiceDomain : IAlunoTurmaServiceDomain
{

    private readonly AppDbContext _context;
    public AlunoTurmaServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(AlunoTurmaDTO alunoTurmaDTO)
    {
        if(alunoTurmaDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        AlunoTurma alunoTurma = new AlunoTurma();
        alunoTurma.IdalunoTurma = alunoTurmaDTO.IdalunoTurma;
        alunoTurma.Idaluno = alunoTurmaDTO.Idaluno;
        alunoTurma.Idturma = alunoTurmaDTO.Idturma;
        alunoTurma.DataMatricula = alunoTurmaDTO.DataMatricula;

        await _context.AlunoTurmas.AddAsync(alunoTurma); //adiciona o AlunoTurma no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(alunoTurma.IdalunoTurma, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<AlunoTurmaDTO>>> BuscarTodos()
    {
        var alunoTurmas = _context.AlunoTurmas;
        var resultAlunoTurmas = await alunoTurmas.Select(a => new AlunoTurmaDTO
        {
            IdalunoTurma = a.IdalunoTurma,
            Idaluno = a.Idaluno,
            Idturma = a.Idturma,
            DataMatricula = a.DataMatricula
        }).ToListAsync();

        var result = ApplicationResult<List<AlunoTurmaDTO>>.Success(resultAlunoTurmas, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<AlunoTurmaDTO>> Atualizar(AlunoTurmaDTO alunoTurmaDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (alunoTurmaDTO == null || alunoTurmaDTO.IdalunoTurma <= 0)
        {
            return ApplicationResult<AlunoTurmaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.AlunoTurmas.Where(a => a.IdalunoTurma == alunoTurmaDTO.IdalunoTurma).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<AlunoTurmaDTO>.Failure(info.Message, 400);

        result.IdalunoTurma = alunoTurmaDTO.IdalunoTurma;
        result.Idaluno = alunoTurmaDTO.Idaluno;
        result.Idturma = alunoTurmaDTO.Idturma;
        result.DataMatricula = alunoTurmaDTO.DataMatricula;

        _context.Entry<AlunoTurma>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<AlunoTurmaDTO>.Success(alunoTurmaDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<AlunoTurmaDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<AlunoTurmaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.AlunoTurmas.Where(a => a.IdalunoTurma == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<AlunoTurmaDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<AlunoTurma>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var alunoTurmaDTO = new AlunoTurmaDTO
        {
            IdalunoTurma = result.IdalunoTurma,
            Idaluno = result.Idaluno,
            Idturma = result.Idturma
        };

        return ApplicationResult<AlunoTurmaDTO>.Success(alunoTurmaDTO, message: info.Message);
    }
    
}