using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class IdiomaController : ApiControllerBase
{

    private readonly IIdiomaServiceDomain _IdiomaService;
    public IdiomaController(IIdiomaServiceDomain service) //injeção de dependencia
    {
        _IdiomaService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] IdiomaDTO Idioma)
    {
        ApplicationResult<long> result = await _IdiomaService.Inserir(Idioma);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _IdiomaService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(IdiomaDTO Idioma)
    {
       var result = await _IdiomaService.Atualizar(Idioma);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _IdiomaService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}