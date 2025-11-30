using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class NivelController : ApiControllerBase
{

    private readonly INivelServiceDomain _NivelService;
    public NivelController(INivelServiceDomain service) //injeção de dependencia
    {
        _NivelService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] NivelDTO Nivel)
    {
        ApplicationResult<long> result = await _NivelService.Inserir(Nivel);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _NivelService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(NivelDTO Nivel)
    {
       var result = await _NivelService.Atualizar(Nivel);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _NivelService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}