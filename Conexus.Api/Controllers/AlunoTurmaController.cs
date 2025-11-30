using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class AlunoTurmaController : ApiControllerBase
{

    private readonly IAlunoTurmaServiceDomain _AlunoTurmaService;
    public AlunoTurmaController(IAlunoTurmaServiceDomain service) //injeção de dependencia
    {
        _AlunoTurmaService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] AlunoTurmaDTO AlunoTurma)
    {
        ApplicationResult<long> result = await _AlunoTurmaService.Inserir(AlunoTurma);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _AlunoTurmaService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(AlunoTurmaDTO AlunoTurma)
    {
       var result = await _AlunoTurmaService.Atualizar(AlunoTurma);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _AlunoTurmaService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}