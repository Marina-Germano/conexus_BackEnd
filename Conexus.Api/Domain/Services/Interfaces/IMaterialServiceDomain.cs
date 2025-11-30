using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IMaterialServiceDomain
{
    Task<ApplicationResult<long>> Inserir(MaterialDTO materialDTO);

    Task<ApplicationResult<MaterialDTO>> Atualizar(MaterialDTO materialDTO);

    Task<ApplicationResult<MaterialDTO>> Excluir(int id);

    Task<ApplicationResult<List<MaterialDTO>>> BuscarTodos();
}