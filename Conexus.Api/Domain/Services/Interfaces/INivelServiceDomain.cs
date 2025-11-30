using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface INivelServiceDomain
{
    Task<ApplicationResult<long>> Inserir(NivelDTO nivelDTO);

    Task<ApplicationResult<NivelDTO>> Atualizar(NivelDTO nivelDTO);

    Task<ApplicationResult<NivelDTO>> Excluir(int id);

    Task<ApplicationResult<List<NivelDTO>>> BuscarTodos();
}