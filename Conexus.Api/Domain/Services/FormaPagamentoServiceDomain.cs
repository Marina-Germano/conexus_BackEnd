using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class FormaPagamentoServiceDomain : IFormaPagamentoServiceDomain
{

    private readonly AppDbContext _context;
    public FormaPagamentoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(FormaPagamentoDTO formaPagamentoDTO)
    {
        if(formaPagamentoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        FormaPagamento formaPagamento = new FormaPagamento();
        formaPagamento.IdformaPagamento = formaPagamentoDTO.IdformaPagamento ;
        formaPagamento.FormaPagamento1 = formaPagamentoDTO.FormaPagamento1;

        await _context.FormaPagamentos.AddAsync(formaPagamento); //adiciona o FormaPagamento no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(formaPagamento.IdformaPagamento, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<FormaPagamentoDTO>>> BuscarTodos()
    {
        var formaPagamentos = _context.FormaPagamentos;
        var resultFormaPagamentos = await formaPagamentos.Select(e => new FormaPagamentoDTO
        {
            IdformaPagamento = e.IdformaPagamento,
            FormaPagamento1 = e.FormaPagamento1
        }).ToListAsync();

        var result = ApplicationResult<List<FormaPagamentoDTO>>.Success(resultFormaPagamentos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<FormaPagamentoDTO>> Atualizar(FormaPagamentoDTO formaPagamentoDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (formaPagamentoDTO == null || formaPagamentoDTO.IdformaPagamento <= 0)
        {
            return ApplicationResult<FormaPagamentoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.FormaPagamentos.Where(e => e.IdformaPagamento == formaPagamentoDTO.IdformaPagamento).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<FormaPagamentoDTO>.Failure(info.Message, 400);

        result.IdformaPagamento = formaPagamentoDTO.IdformaPagamento;
        result.FormaPagamento1 = formaPagamentoDTO.FormaPagamento1;

        _context.Entry<FormaPagamento>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<FormaPagamentoDTO>.Success(formaPagamentoDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<FormaPagamentoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<FormaPagamentoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.FormaPagamentos.Where(e => e.IdformaPagamento == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<FormaPagamentoDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<FormaPagamento>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var formaPagamentoDTO = new FormaPagamentoDTO
        {
            IdformaPagamento = result.IdformaPagamento,
            FormaPagamento1 = result.FormaPagamento1
        };

        return ApplicationResult<FormaPagamentoDTO>.Success(formaPagamentoDTO, message: info.Message);
    }
    
}