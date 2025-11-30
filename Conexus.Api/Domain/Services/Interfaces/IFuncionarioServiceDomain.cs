using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IFuncionarioServiceDomain
{
    Task<ApplicationResult<long>> Inserir(FuncionarioDTO funcionarioDTO);

    Task<ApplicationResult<FuncionarioDTO>> Atualizar(FuncionarioDTO funcionarioDTO);

    Task<ApplicationResult<FuncionarioDTO>> Excluir(int id);

    Task<ApplicationResult<List<FuncionarioDTO>>> BuscarTodos();
}