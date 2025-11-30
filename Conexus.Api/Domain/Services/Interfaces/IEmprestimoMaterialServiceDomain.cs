using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IEmprestimoMaterialServiceDomain
{
    Task<ApplicationResult<long>> Inserir(EmprestimoMaterialDTO emprestimoMaterialDTO);

    Task<ApplicationResult<EmprestimoMaterialDTO>> Atualizar(EmprestimoMaterialDTO emprestimoMaterialDTO);

    Task<ApplicationResult<EmprestimoMaterialDTO>> Excluir(int id);

    Task<ApplicationResult<List<EmprestimoMaterialDTO>>> BuscarTodos();
}