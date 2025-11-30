using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class ContatoController : ApiControllerBase
{

    private readonly IContatoServiceDomain _ContatoService;
    public ContatoController(IContatoServiceDomain service) //injeção de dependencia
    {
        _ContatoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] ContatoDTO Contato)
    {
        ApplicationResult<long> result = await _ContatoService.Inserir(Contato);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _ContatoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(ContatoDTO Contato)
    {
       var result = await _ContatoService.Atualizar(Contato);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _ContatoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}