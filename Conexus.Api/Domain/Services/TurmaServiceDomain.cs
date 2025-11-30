using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class TurmaServiceDomain : ITurmaServiceDomain
{

    private readonly AppDbContext _context;
    public TurmaServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(TurmaDTO turmaDTO)
    {
        if(turmaDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Turma turma = new Turma();
        turma.Idturma = turmaDTO.Idturma ;
        turma.Ididioma = turmaDTO.Ididioma;
        turma.Idnivel = turmaDTO.Idnivel;
        turma.Idfuncionario = turmaDTO.Idfuncionario;
        turma.Descricao = turmaDTO.Descricao;
        turma.DiasSemana = turmaDTO.DiasSemana;
        turma.HoraInicio = turmaDTO.HoraInicio;
        turma.Sala = turmaDTO.Sala;
        turma.Imagem = turmaDTO.Imagem;
        turma.TipoRecorrencia = turmaDTO.TipoRecorrencia;

        await _context.Turmas.AddAsync(turma); //adiciona o Turma no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(turma.Idturma, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<TurmaDTO>>> BuscarTodos()
    {
        var turmas = _context.Turmas;
        var resultTurmas = await turmas.Select(e => new TurmaDTO
        {
            Idturma = e.Idturma,
            Ididioma = e.Ididioma,
            Idnivel = e.Idnivel,
            Idfuncionario = e.Idfuncionario,
            Descricao = e.Descricao,
            DiasSemana = e.DiasSemana,
            HoraInicio = e.HoraInicio,
            CapacidadeMaxima = e.CapacidadeMaxima,
            Sala = e.Sala,
            Imagem = e.Imagem,
            TipoRecorrencia = e.TipoRecorrencia
        }).ToListAsync();

        var result = ApplicationResult<List<TurmaDTO>>.Success(resultTurmas, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<TurmaDTO>> Atualizar(TurmaDTO turmaDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (turmaDTO == null || turmaDTO.Idturma <= 0)
        {
            return ApplicationResult<TurmaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Turmas.Where(e => e.Idturma == turmaDTO.Idturma).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<TurmaDTO>.Failure(info.Message, 400);

        result.Idturma = turmaDTO.Idturma;
        result.Ididioma = turmaDTO.Ididioma;
        result.Idnivel = turmaDTO.Idnivel;
        result.Idfuncionario = turmaDTO.Idfuncionario;
        result.Descricao = turmaDTO.Descricao;
        result.DiasSemana = turmaDTO.DiasSemana;
        result.HoraInicio = turmaDTO.HoraInicio;
        result.CapacidadeMaxima = turmaDTO.CapacidadeMaxima;
        result.Sala = turmaDTO.Sala;
        result.Imagem = turmaDTO.Imagem;
        result.TipoRecorrencia = turmaDTO.TipoRecorrencia;

        _context.Entry<Turma>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<TurmaDTO>.Success(turmaDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<TurmaDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<TurmaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Turmas.Where(e => e.Idturma == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<TurmaDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Turma>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var turmaDTO = new TurmaDTO
        {
            Idturma = result.Idturma,
            Ididioma = result.Ididioma,
            Idnivel = result.Idnivel,
            Idfuncionario = result.Idfuncionario,
            Descricao = result.Descricao,
            DiasSemana = result.DiasSemana,
            HoraInicio = result.HoraInicio,
            CapacidadeMaxima = result.CapacidadeMaxima,
            Sala = result.Sala,
            Imagem = result.Imagem,
            TipoRecorrencia = result.TipoRecorrencia
        };

        return ApplicationResult<TurmaDTO>.Success(turmaDTO, message: info.Message);
    }
    
}