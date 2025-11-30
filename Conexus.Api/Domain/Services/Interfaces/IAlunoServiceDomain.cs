using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IAlunoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(AlunoDTO alunoDTO);

    Task<ApplicationResult<AlunoDTO>> Atualizar(AlunoDTO alunoDTO);

    Task<ApplicationResult<AlunoDTO>> Excluir(int id);

    Task<ApplicationResult<List<AlunoDTO>>> BuscarTodos();
}