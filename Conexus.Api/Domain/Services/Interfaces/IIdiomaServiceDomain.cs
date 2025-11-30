using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IIdiomaServiceDomain
{
    Task<ApplicationResult<long>> Inserir(IdiomaDTO idiomaDTO);

    Task<ApplicationResult<IdiomaDTO>> Atualizar(IdiomaDTO idiomaDTO);

    Task<ApplicationResult<IdiomaDTO>> Excluir(int id);

    Task<ApplicationResult<List<IdiomaDTO>>> BuscarTodos();
}