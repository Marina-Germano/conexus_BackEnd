using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class CalendarioAulaServiceDomain : ICalendarioAulaServiceDomain
{

    private readonly AppDbContext _context;
    public CalendarioAulaServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(CalendarioAulaDTO calendarioAulaDTO)
    {
        if(calendarioAulaDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        CalendarioAula calendarioAula = new CalendarioAula();
        calendarioAula.Idaula  = calendarioAulaDTO.Idaula  ;
        calendarioAula.DataAula = calendarioAulaDTO.DataAula;
        calendarioAula.HoraInicio = calendarioAulaDTO.HoraInicio;
        calendarioAula.HoraFim = calendarioAulaDTO.HoraFim;
        calendarioAula.Idfuncionario = calendarioAulaDTO.Idfuncionario;
        calendarioAula.Idturma = calendarioAulaDTO.Idturma;
        calendarioAula.Idmaterial = calendarioAulaDTO.Idmaterial;
        calendarioAula.Observacoes = calendarioAulaDTO.Observacoes;
        calendarioAula.LinkReuniao = calendarioAulaDTO.LinkReuniao;
        calendarioAula.AulaExtra = calendarioAulaDTO.AulaExtra;

        await _context.CalendarioAulas.AddAsync(calendarioAula); //adiciona o CalendarioAula no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(calendarioAula.Idaula, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<CalendarioAulaDTO>>> BuscarTodos()
    {
        var calendarioAulas = _context.CalendarioAulas;
        var resultCalendarioAulas = await calendarioAulas.Select(e => new CalendarioAulaDTO
        {
            Idaula = e.Idaula,
            DataAula = e.DataAula,
            HoraInicio = e.HoraInicio,
            HoraFim = e.HoraFim,
            Idfuncionario = e.Idfuncionario,
            Idturma = e.Idturma,
            Idmaterial = e.Idmaterial,
            Sala = e.Sala,
            Observacoes = e.Observacoes,
            LinkReuniao = e.LinkReuniao,
            AulaExtra = e.AulaExtra
        }).ToListAsync();

        var result = ApplicationResult<List<CalendarioAulaDTO>>.Success(resultCalendarioAulas, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<CalendarioAulaDTO>> Atualizar(CalendarioAulaDTO calendarioAulaDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (calendarioAulaDTO == null || calendarioAulaDTO.Idaula <= 0)
        {
            return ApplicationResult<CalendarioAulaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.CalendarioAulas.Where(e => e.Idaula == calendarioAulaDTO.Idaula).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<CalendarioAulaDTO>.Failure(info.Message, 400);

        result.Idaula = calendarioAulaDTO.Idaula;
        result.DataAula = calendarioAulaDTO.DataAula;
        result.HoraInicio = calendarioAulaDTO.HoraInicio;
        result.HoraFim = calendarioAulaDTO.HoraFim;
        result.Idfuncionario = calendarioAulaDTO.Idfuncionario;
        result.Idturma = calendarioAulaDTO.Idturma;
        result.Idmaterial = calendarioAulaDTO.Idmaterial;
        result.Sala = calendarioAulaDTO.Sala;
        result.Observacoes = calendarioAulaDTO.Observacoes;
        result.LinkReuniao = calendarioAulaDTO.LinkReuniao;
        result.AulaExtra = calendarioAulaDTO.AulaExtra;

        _context.Entry<CalendarioAula>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<CalendarioAulaDTO>.Success(calendarioAulaDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<CalendarioAulaDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<CalendarioAulaDTO>.Failure(info.Message, 400);
        }

        var result = await _context.CalendarioAulas.Where(e => e.Idaula == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<CalendarioAulaDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<CalendarioAula>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var calendarioAulaDTO = new CalendarioAulaDTO
        {
            Idaula = result.Idaula,
            DataAula = result.DataAula,
            HoraInicio = result.HoraInicio,
            HoraFim = result.HoraFim,
            Idfuncionario = result.Idfuncionario,
            Idturma = result.Idturma,
            Idmaterial = result.Idmaterial,
            Sala = result.Sala,
            Observacoes = result.Observacoes,
            LinkReuniao = result.LinkReuniao,
            AulaExtra = result.AulaExtra
        };

        return ApplicationResult<CalendarioAulaDTO>.Success(calendarioAulaDTO, message: info.Message);
    }
    
}