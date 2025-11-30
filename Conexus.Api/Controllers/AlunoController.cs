using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class AlunoController : ApiControllerBase
{

    private readonly IAlunoServiceDomain _AlunoService;
    public AlunoController(IAlunoServiceDomain service) //injeção de dependencia
    {
        _AlunoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] AlunoDTO Aluno)
    {
        ApplicationResult<long> result = await _AlunoService.Inserir(Aluno);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _AlunoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(AlunoDTO Aluno)
    {
       var result = await _AlunoService.Atualizar(Aluno);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _AlunoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}