using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class CalendarioAulaController : ApiControllerBase
{

    private readonly ICalendarioAulaServiceDomain _CalendarioAulaService;
    public CalendarioAulaController(ICalendarioAulaServiceDomain service) //injeção de dependencia
    {
        _CalendarioAulaService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] CalendarioAulaDTO CalendarioAula)
    {
        ApplicationResult<long> result = await _CalendarioAulaService.Inserir(CalendarioAula);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _CalendarioAulaService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(CalendarioAulaDTO CalendarioAula)
    {
       var result = await _CalendarioAulaService.Atualizar(CalendarioAula);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _CalendarioAulaService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}