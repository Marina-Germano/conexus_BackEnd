using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class DocumentoAlunoController : ApiControllerBase
{

    private readonly IDocumentoAlunoServiceDomain _DocumentoAlunoService;
    public DocumentoAlunoController(IDocumentoAlunoServiceDomain service) //injeção de dependencia
    {
        _DocumentoAlunoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] DocumentoAlunoDTO DocumentoAluno)
    {
        ApplicationResult<long> result = await _DocumentoAlunoService.Inserir(DocumentoAluno);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _DocumentoAlunoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(DocumentoAlunoDTO DocumentoAluno)
    {
       var result = await _DocumentoAlunoService.Atualizar(DocumentoAluno);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _DocumentoAlunoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}