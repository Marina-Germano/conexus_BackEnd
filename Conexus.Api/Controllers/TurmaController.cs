using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class TurmaController : ApiControllerBase
{

    private readonly ITurmaServiceDomain _TurmaService;
    public TurmaController(ITurmaServiceDomain service) //injeção de dependencia
    {
        _TurmaService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] TurmaDTO Turma)
    {
        ApplicationResult<long> result = await _TurmaService.Inserir(Turma);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _TurmaService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(TurmaDTO Turma)
    {
       var result = await _TurmaService.Atualizar(Turma);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _TurmaService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}