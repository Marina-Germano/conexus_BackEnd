using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IPagamentoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(PagamentoDTO pagamentoDTO);

    Task<ApplicationResult<PagamentoDTO>> Atualizar(PagamentoDTO pagamentoDTO);

    Task<ApplicationResult<PagamentoDTO>> Excluir(int id);

    Task<ApplicationResult<List<PagamentoDTO>>> BuscarTodos();
}