using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class TipoMaterialController : ApiControllerBase
{

    private readonly ITipoMaterialServiceDomain _TipoMaterialService;
    public TipoMaterialController(ITipoMaterialServiceDomain service) //injeção de dependencia
    {
        _TipoMaterialService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] TipoMaterialDTO TipoMaterial)
    {
        ApplicationResult<long> result = await _TipoMaterialService.Inserir(TipoMaterial);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _TipoMaterialService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(TipoMaterialDTO TipoMaterial)
    {
       var result = await _TipoMaterialService.Atualizar(TipoMaterial);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _TipoMaterialService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}