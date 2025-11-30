using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface ITipoMaterialServiceDomain
{
    Task<ApplicationResult<long>> Inserir(TipoMaterialDTO tipoMaterialDTO);

    Task<ApplicationResult<TipoMaterialDTO>> Atualizar(TipoMaterialDTO tipoMaterialDTO);

    Task<ApplicationResult<TipoMaterialDTO>> Excluir(int id);

    Task<ApplicationResult<List<TipoMaterialDTO>>> BuscarTodos();
}