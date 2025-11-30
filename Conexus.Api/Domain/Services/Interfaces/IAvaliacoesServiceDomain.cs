using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IAvaliacaoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(AvaliacaoDTO avaliacaoDTO);

    Task<ApplicationResult<AvaliacaoDTO>> Atualizar(AvaliacaoDTO avaliacaoDTO);

    Task<ApplicationResult<AvaliacaoDTO>> Excluir(int id);

    Task<ApplicationResult<List<AvaliacaoDTO>>> BuscarTodos();
}