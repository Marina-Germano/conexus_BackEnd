using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class FuncionarioServiceDomain : IFuncionarioServiceDomain
{

    private readonly AppDbContext _context;
    public FuncionarioServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(FuncionarioDTO funcionarioDTO)
    {
        if(funcionarioDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Funcionario funcionario = new Funcionario();
        funcionario.Idfuncionario = funcionarioDTO.Idfuncionario ;
        funcionario.Idusuario = funcionarioDTO.Idusuario;
        funcionario.Cargo = funcionarioDTO.Cargo;
        funcionario.Especialidade = funcionarioDTO.Especialidade;
        
        await _context.Funcionarios.AddAsync(funcionario); //adiciona o Funcionario no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(funcionario.Idfuncionario, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<FuncionarioDTO>>> BuscarTodos()
    {
        var funcionarios = _context.Funcionarios;
        var resultFuncionarios = await funcionarios.Select(e => new FuncionarioDTO
        {
            Idfuncionario = e.Idfuncionario,
            Idusuario = e.Idusuario,
            Cargo = e.Cargo,
            Especialidade = e.Especialidade
        }).ToListAsync();

        var result = ApplicationResult<List<FuncionarioDTO>>.Success(resultFuncionarios, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<FuncionarioDTO>> Atualizar(FuncionarioDTO funcionarioDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (funcionarioDTO == null || funcionarioDTO.Idfuncionario <= 0)
        {
            return ApplicationResult<FuncionarioDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Funcionarios.Where(e => e.Idfuncionario == funcionarioDTO.Idfuncionario).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<FuncionarioDTO>.Failure(info.Message, 400);

        result.Idfuncionario = funcionarioDTO.Idfuncionario;
        result.Idusuario = funcionarioDTO.Idusuario;
        result.Cargo = funcionarioDTO.Cargo;
        result.Especialidade = funcionarioDTO.Especialidade;

        _context.Entry<Funcionario>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<FuncionarioDTO>.Success(funcionarioDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<FuncionarioDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<FuncionarioDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Funcionarios.Where(e => e.Idfuncionario == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<FuncionarioDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Funcionario>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var funcionarioDTO = new FuncionarioDTO
        {
            Idfuncionario = result.Idfuncionario,
            Idusuario = result.Idusuario,
            Cargo = result.Cargo,
            Especialidade = result.Especialidade
        };

        return ApplicationResult<FuncionarioDTO>.Success(funcionarioDTO, message: info.Message);
    }
    
}