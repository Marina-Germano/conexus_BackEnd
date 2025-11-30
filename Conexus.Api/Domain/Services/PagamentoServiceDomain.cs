using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class PagamentoServiceDomain : IPagamentoServiceDomain
{

    private readonly AppDbContext _context;
    public PagamentoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(PagamentoDTO pagamentoDTO)
    {
        if(pagamentoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Pagamento pagamento = new Pagamento();
        pagamento.Idpagamento = pagamentoDTO.Idpagamento ;
        pagamento.IdformaPagamento = pagamentoDTO.IdformaPagamento;
        pagamento.Idaluno = pagamentoDTO.Idaluno;
        pagamento.Valor = pagamentoDTO.Valor;
        pagamento.DataVencimento = pagamentoDTO.DataVencimento;
        pagamento.StatusPagamento = pagamentoDTO.StatusPagamento;
        pagamento.DataPagamento = pagamentoDTO.DataPagamento;
        pagamento.ValorPago = pagamentoDTO.ValorPago;
        pagamento.Observacoes = pagamentoDTO.Observacoes;
        pagamento.Multa = pagamentoDTO.Multa;

        await _context.Pagamentos.AddAsync(pagamento); //adiciona o Pagamento no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(pagamento.Idpagamento, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<PagamentoDTO>>> BuscarTodos()
    {
        var pagamentos = _context.Pagamentos;
        var resultPagamentos = await pagamentos.Select(e => new PagamentoDTO
        {
            Idpagamento = e.Idpagamento,
            IdformaPagamento = e.IdformaPagamento,
            Idaluno = e.Idaluno,
            Valor = e.Valor,
            DataVencimento = e.DataVencimento,
            StatusPagamento = e.StatusPagamento,
            DataPagamento = e.DataPagamento,
            ValorPago = e.ValorPago,
            Observacoes = e.Observacoes,
            Multa = e.Multa,
        }).ToListAsync();

        var result = ApplicationResult<List<PagamentoDTO>>.Success(resultPagamentos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<PagamentoDTO>> Atualizar(PagamentoDTO pagamentoDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (pagamentoDTO == null || pagamentoDTO.Idpagamento <= 0)
        {
            return ApplicationResult<PagamentoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Pagamentos.Where(e => e.Idpagamento == pagamentoDTO.Idpagamento).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<PagamentoDTO>.Failure(info.Message, 400);

        result.Idpagamento = pagamentoDTO.Idpagamento;
        result.IdformaPagamento = pagamentoDTO.IdformaPagamento;
        result.Idaluno = pagamentoDTO.Idaluno;
        result.Valor = pagamentoDTO.Valor;
        result.DataVencimento = pagamentoDTO.DataVencimento;
        result.StatusPagamento = pagamentoDTO.StatusPagamento;
        result.DataPagamento = pagamentoDTO.DataPagamento;
        result.ValorPago = pagamentoDTO.ValorPago;
        result.Observacoes = pagamentoDTO.Observacoes;
        result.Multa = pagamentoDTO.Multa;

        _context.Entry<Pagamento>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<PagamentoDTO>.Success(pagamentoDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<PagamentoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<PagamentoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Pagamentos.Where(e => e.Idpagamento == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<PagamentoDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Pagamento>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var pagamentoDTO = new PagamentoDTO
        {
            Idpagamento = result.Idpagamento,
            Idaluno = result.Idaluno,
            Valor = result.Valor,
            DataVencimento = result.DataVencimento,
            StatusPagamento = result.StatusPagamento,
            DataPagamento = result.DataPagamento,
            ValorPago = result.ValorPago,
            Observacoes = result.Observacoes,
            Multa = result.Multa
        };

        return ApplicationResult<PagamentoDTO>.Success(pagamentoDTO, message: info.Message);
    }
    
}