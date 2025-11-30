using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class DocumentoAlunoServiceDomain : IDocumentoAlunoServiceDomain
{

    private readonly AppDbContext _context;
    public DocumentoAlunoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(DocumentoAlunoDTO documentoAlunoDTO)
    {
        if(documentoAlunoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        DocumentoAluno documentoAluno = new DocumentoAluno();
        documentoAluno.Iddocumento = documentoAlunoDTO.Iddocumento ;
        documentoAluno.Idaluno = documentoAlunoDTO.Idaluno;
        documentoAluno.IdtipoDocumento = documentoAlunoDTO.IdtipoDocumento;
        documentoAluno.CaminhoArquivo = documentoAlunoDTO.CaminhoArquivo;
        documentoAluno.DataEnvio = documentoAlunoDTO.DataEnvio;
        documentoAluno.Observacoes = documentoAlunoDTO.Observacoes;
        documentoAluno.StatusDocumento = documentoAlunoDTO.StatusDocumento;
        documentoAluno.Observacoes = documentoAlunoDTO.Observacoes;

        await _context.DocumentoAlunos.AddAsync(documentoAluno); //adiciona o DocumentoAluno no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(documentoAluno.Iddocumento, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<DocumentoAlunoDTO>>> BuscarTodos()
    {
        var documentoAlunos = _context.DocumentoAlunos;
        var resultDocumentoAlunos = await documentoAlunos.Select(e => new DocumentoAlunoDTO
        {
            Iddocumento = e.Iddocumento,
            Idaluno = e.Idaluno,
            IdtipoDocumento = e.IdtipoDocumento,
            CaminhoArquivo = e.CaminhoArquivo,
            DataEnvio = e.DataEnvio,
            Observacoes = e.Observacoes,
            StatusDocumento = e.StatusDocumento
        }).ToListAsync();

        var result = ApplicationResult<List<DocumentoAlunoDTO>>.Success(resultDocumentoAlunos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<DocumentoAlunoDTO>> Atualizar(DocumentoAlunoDTO documentoAlunoDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (documentoAlunoDTO == null || documentoAlunoDTO.Iddocumento <= 0)
        {
            return ApplicationResult<DocumentoAlunoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.DocumentoAlunos.Where(e => e.Iddocumento == documentoAlunoDTO.Iddocumento).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<DocumentoAlunoDTO>.Failure(info.Message, 400);

        result.Iddocumento = documentoAlunoDTO.Iddocumento;
        result.Idaluno = documentoAlunoDTO.Idaluno;
        result.IdtipoDocumento = documentoAlunoDTO.IdtipoDocumento;
        result.CaminhoArquivo = documentoAlunoDTO.CaminhoArquivo;
        result.DataEnvio = documentoAlunoDTO.DataEnvio;
        result.Observacoes = documentoAlunoDTO.Observacoes;
        result.StatusDocumento = documentoAlunoDTO.StatusDocumento;

        _context.Entry<DocumentoAluno>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<DocumentoAlunoDTO>.Success(documentoAlunoDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<DocumentoAlunoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<DocumentoAlunoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.DocumentoAlunos.Where(e => e.Iddocumento == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<DocumentoAlunoDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<DocumentoAluno>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var documentoAlunoDTO = new DocumentoAlunoDTO
        {
            Iddocumento = result.Iddocumento,
            Idaluno = result.Idaluno,
            IdtipoDocumento = result.IdtipoDocumento,
            CaminhoArquivo = result.CaminhoArquivo,
            DataEnvio = result.DataEnvio,
            Observacoes = result.Observacoes,
            StatusDocumento = result.StatusDocumento
        };

        return ApplicationResult<DocumentoAlunoDTO>.Success(documentoAlunoDTO, message: info.Message);
    }
    
}