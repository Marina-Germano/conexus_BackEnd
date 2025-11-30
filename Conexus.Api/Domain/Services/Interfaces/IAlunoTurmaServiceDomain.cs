using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IAlunoTurmaServiceDomain
{
    Task<ApplicationResult<long>> Inserir(AlunoTurmaDTO alunoTurmaDTO);

    Task<ApplicationResult<AlunoTurmaDTO>> Atualizar(AlunoTurmaDTO alunoTurmaDTO);

    Task<ApplicationResult<AlunoTurmaDTO>> Excluir(int id);

    Task<ApplicationResult<List<AlunoTurmaDTO>>> BuscarTodos();
}