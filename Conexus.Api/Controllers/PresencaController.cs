using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class PresencaController : ApiControllerBase
{

    private readonly IPresencaServiceDomain _PresencaService;
    public PresencaController(IPresencaServiceDomain service) //injeção de dependencia
    {
        _PresencaService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] PresencaDTO Presenca)
    {
        ApplicationResult<long> result = await _PresencaService.Inserir(Presenca);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _PresencaService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(PresencaDTO Presenca)
    {
       var result = await _PresencaService.Atualizar(Presenca);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _PresencaService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}