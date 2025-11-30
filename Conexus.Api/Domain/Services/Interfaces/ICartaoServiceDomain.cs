using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface ICartaoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(CartaoDTO cartaoDTO);

    Task<ApplicationResult<CartaoDTO>> Atualizar(CartaoDTO cartaoDTO);

    Task<ApplicationResult<CartaoDTO>> Excluir(int id);

    Task<ApplicationResult<List<CartaoDTO>>> BuscarTodos();
}