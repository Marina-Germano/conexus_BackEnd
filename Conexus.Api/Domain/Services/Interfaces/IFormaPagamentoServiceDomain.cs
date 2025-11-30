using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IFormaPagamentoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(FormaPagamentoDTO formaPagamentoDTO);

    Task<ApplicationResult<FormaPagamentoDTO>> Atualizar(FormaPagamentoDTO formaPagamentoDTO);

    Task<ApplicationResult<FormaPagamentoDTO>> Excluir(int id);

    Task<ApplicationResult<List<FormaPagamentoDTO>>> BuscarTodos();
}