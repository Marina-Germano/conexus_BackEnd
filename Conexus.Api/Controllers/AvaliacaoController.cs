using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class AvaliacaoController : ApiControllerBase
{

    private readonly IAvaliacaoServiceDomain _AvaliacaoService;
    public AvaliacaoController(IAvaliacaoServiceDomain service) //injeção de dependencia
    {
        _AvaliacaoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] AvaliacaoDTO Avaliacao)
    {
        ApplicationResult<long> result = await _AvaliacaoService.Inserir(Avaliacao);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _AvaliacaoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(AvaliacaoDTO Avaliacao)
    {
       var result = await _AvaliacaoService.Atualizar(Avaliacao);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _AvaliacaoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}