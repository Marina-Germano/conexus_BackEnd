using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IUsuarioServiceDomain
{
    Task<ApplicationResult<long>> Inserir(UsuarioDTO usuarioDTO);

    Task<ApplicationResult<UsuarioDTO>> Atualizar(UsuarioDTO usuarioDTO);

    Task<ApplicationResult<UsuarioDTO>> Excluir(int id);

    Task<ApplicationResult<List<UsuarioDTO>>> BuscarTodos();
}