using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class AvaliacaoServiceDomain : IAvaliacaoServiceDomain
{

    private readonly AppDbContext _context;
    public AvaliacaoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(AvaliacaoDTO avaliacaoDTO)
    {
        if(avaliacaoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Avaliacao avaliacao = new Avaliacao();
        avaliacao.IdalunoTurma = avaliacaoDTO.IdalunoTurma ;
        avaliacao.Idfuncionario = avaliacaoDTO.Idfuncionario;
        avaliacao.Descricao = avaliacaoDTO.Descricao;
        avaliacao.Titulo = avaliacaoDTO.Titulo;
        avaliacao.DataAvaliacao = avaliacaoDTO.DataAvaliacao;
        avaliacao.Nota = avaliacaoDTO.Nota;
        avaliacao.Peso = avaliacaoDTO.Peso;
        avaliacao.Observacao = avaliacaoDTO.Observacao;

        await _context.Avaliacaos.AddAsync(avaliacao); //adiciona o Avaliacao no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(avaliacao.Idavaliacao, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<AvaliacaoDTO>>> BuscarTodos()
    {
        var avaliacaos = _context.Avaliacaos;
        var resultAvaliacaos = await avaliacaos.Select(a => new AvaliacaoDTO
        {
            Idavaliacao = a.Idavaliacao,
            IdalunoTurma = a.IdalunoTurma,
            Idfuncionario = a.Idfuncionario,
            Idturma = a.Idturma,
            Descricao = a.Descricao,
            Titulo = a.Titulo,
            Nota = a.Nota,
            Peso = a.Peso,
            Observacao = a.Observacao
        }).ToListAsync();

        var result = ApplicationResult<List<AvaliacaoDTO>>.Success(resultAvaliacaos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<AvaliacaoDTO>> Atualizar(AvaliacaoDTO avaliacaoDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (avaliacaoDTO == null || avaliacaoDTO.Idavaliacao <= 0)
        {
            return ApplicationResult<AvaliacaoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Avaliacaos.Where(a => a.Idavaliacao == avaliacaoDTO.Idavaliacao).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<AvaliacaoDTO>.Failure(info.Message, 400);

        result.Idavaliacao = avaliacaoDTO.Idavaliacao;
        result.IdalunoTurma = avaliacaoDTO.IdalunoTurma;
        result.Idfuncionario = avaliacaoDTO.Idfuncionario;
        result.Idturma = avaliacaoDTO.Idturma;
        result.Descricao = avaliacaoDTO.Descricao;
        result.Titulo = avaliacaoDTO.Titulo;
        result.DataAvaliacao = avaliacaoDTO.DataAvaliacao;
        result.Nota = avaliacaoDTO.Nota;
        result.Peso = avaliacaoDTO.Peso;
        result.Observacao = avaliacaoDTO.Observacao;

        _context.Entry<Avaliacao>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<AvaliacaoDTO>.Success(avaliacaoDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<AvaliacaoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<AvaliacaoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Avaliacaos.Where(a => a.Idavaliacao == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<AvaliacaoDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Avaliacao>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var avaliacaoDTO = new AvaliacaoDTO
        {
            Idavaliacao = result.Idavaliacao,
            IdalunoTurma = result.IdalunoTurma,
            Idfuncionario = result.Idfuncionario,
            Idturma = result.Idturma,
            Descricao = result.Descricao,
            Titulo = result.Titulo,
            DataAvaliacao = result.DataAvaliacao,
            Nota = result.Nota,
            Peso = result.Peso,
            Observacao = result.Observacao
        };

        return ApplicationResult<AvaliacaoDTO>.Success(avaliacaoDTO, message: info.Message);
    }
    
}