using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface ITurmaServiceDomain
{
    Task<ApplicationResult<long>> Inserir(TurmaDTO turmaDTO);

    Task<ApplicationResult<TurmaDTO>> Atualizar(TurmaDTO turmaDTO);

    Task<ApplicationResult<TurmaDTO>> Excluir(int id);

    Task<ApplicationResult<List<TurmaDTO>>> BuscarTodos();
}