using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class MaterialController : ApiControllerBase
{

    private readonly IMaterialServiceDomain _MaterialService;
    public MaterialController(IMaterialServiceDomain service) //injeção de dependencia
    {
        _MaterialService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] MaterialDTO Material)
    {
        ApplicationResult<long> result = await _MaterialService.Inserir(Material);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _MaterialService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(MaterialDTO Material)
    {
       var result = await _MaterialService.Atualizar(Material);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _MaterialService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}