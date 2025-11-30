using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;
using Conexus.Api.Infra;
namespace Conexus.Api.Domain.Services;

public class AlunoServiceDomain : IAlunoServiceDomain
{

    private readonly AppDbContext _context;
    public AlunoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(AlunoDTO alunoDTO)
    {
        if (alunoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }

        Aluno aluno = new Aluno();
        
        aluno.Idaluno = alunoDTO.Idaluno;
        aluno.Idusuario = alunoDTO.Idusuario;
        aluno.Cep = alunoDTO.Cep;
        aluno.Rua = alunoDTO.Rua;
        aluno.Numero = alunoDTO.Numero;
        aluno.Bairro = alunoDTO.Bairro;
        aluno.Complemento = alunoDTO.Complemento;
        aluno.Responsavel = alunoDTO.Responsavel;
        aluno.TelResponsavel = alunoDTO.TelResponsavel;
        aluno.Situacao = alunoDTO.Situacao;

        await _context.Alunos.AddAsync(aluno); //adiciona o Aluno no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(aluno.Idaluno, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<AlunoDTO>>> BuscarTodos()
    {
        var alunos = _context.Alunos;
        var resultAlunos = await alunos.Select(a => new AlunoDTO
        {
            Id = a.Idaluno,
            Idaluno = a.Idaluno,
            Idusuario = a.Idusuario,
            Cep = a.Cep,
            Situacao = a.Situacao,
            TelResponsavel = a.TelResponsavel,
            Responsavel = a.Responsavel,
            Complemento = a.Complemento,
            Bairro = a.Bairro,
            Numero = a.Numero,
            Rua = a.Rua

        }).ToListAsync();

        var result = ApplicationResult<List<AlunoDTO>>.Success(resultAlunos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<AlunoDTO>> Atualizar(AlunoDTO alunoDTO)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (alunoDTO == null || alunoDTO.Id <= 0)
        {
            return ApplicationResult<AlunoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Alunos.Where(a => a.Idaluno == alunoDTO.Id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<AlunoDTO>.Failure(info.Message, 400);

        result.Idaluno = alunoDTO.Idaluno;
        result.Idusuario = alunoDTO.Idusuario;
        result.Cep = alunoDTO.Cep;
        result.Situacao = alunoDTO.Situacao;
        result.TelResponsavel = alunoDTO.TelResponsavel;
        result.Responsavel = alunoDTO.Responsavel;
        result.Complemento = alunoDTO.Complemento;
        result.Bairro = alunoDTO.Bairro;
        result.Numero = alunoDTO.Numero;
        result.Rua = alunoDTO.Rua;

        _context.Entry<Aluno>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<AlunoDTO>.Success(alunoDTO, message: info.Message);
    }

    [HttpDelete("excluir")]
    public async Task<ApplicationResult<AlunoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<AlunoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Alunos.Where(a => a.Idaluno == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<AlunoDTO>.Failure(info.Message, 400);
        }

        _context.Entry<Aluno>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };

        var alunoDTO = new AlunoDTO
        {
            Id = result.Idaluno,
            Cep = result.Cep,
            Situacao = result.Situacao,
            TelResponsavel = result.TelResponsavel,
            Responsavel = result.Responsavel,
            Complemento = result.Complemento,
            Bairro = result.Bairro,
            Numero = result.Numero,
            Rua = result.Rua
        };

        return ApplicationResult<AlunoDTO>.Success(alunoDTO, message: info.Message);
    }
}