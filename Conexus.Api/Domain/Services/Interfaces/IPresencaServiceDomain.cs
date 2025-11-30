using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IPresencaServiceDomain
{
    Task<ApplicationResult<long>> Inserir(PresencaDTO presencaDTO);

    Task<ApplicationResult<PresencaDTO>> Atualizar(PresencaDTO presencaDTO);

    Task<ApplicationResult<PresencaDTO>> Excluir(int id);

    Task<ApplicationResult<List<PresencaDTO>>> BuscarTodos();
}