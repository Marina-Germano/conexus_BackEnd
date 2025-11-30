using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface IDocumentoAlunoServiceDomain
{
    Task<ApplicationResult<long>> Inserir(DocumentoAlunoDTO documentoAlunoDTO);

    Task<ApplicationResult<DocumentoAlunoDTO>> Atualizar(DocumentoAlunoDTO documentoAlunoDTO);

    Task<ApplicationResult<DocumentoAlunoDTO>> Excluir(int id);

    Task<ApplicationResult<List<DocumentoAlunoDTO>>> BuscarTodos();
}