using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface ITipoDocumentoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(TipoDocumentoDTO tipoDocumentoDTO);

    Task<ApplicationResult<TipoDocumentoDTO>> Atualizar(TipoDocumentoDTO tipoDocumentoDTO);

    Task<ApplicationResult<TipoDocumentoDTO>> Excluir(int id);

    Task<ApplicationResult<List<TipoDocumentoDTO>>> BuscarTodos();
}