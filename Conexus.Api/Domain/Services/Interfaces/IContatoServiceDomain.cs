using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IContatoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(ContatoDTO contatoDTO);

    Task<ApplicationResult<ContatoDTO>> Atualizar(ContatoDTO contatoDTO);

    Task<ApplicationResult<ContatoDTO>> Excluir(int id);

    Task<ApplicationResult<List<ContatoDTO>>> BuscarTodos();
}