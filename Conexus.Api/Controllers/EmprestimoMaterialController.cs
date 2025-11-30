using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class EmprestimoMaterialController : ApiControllerBase
{

    private readonly IEmprestimoMaterialServiceDomain _EmprestimoMaterialService;
    public EmprestimoMaterialController(IEmprestimoMaterialServiceDomain service) //injeção de dependencia
    {
        _EmprestimoMaterialService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] EmprestimoMaterialDTO EmprestimoMaterial)
    {
        ApplicationResult<long> result = await _EmprestimoMaterialService.Inserir(EmprestimoMaterial);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _EmprestimoMaterialService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(EmprestimoMaterialDTO EmprestimoMaterial)
    {
       var result = await _EmprestimoMaterialService.Atualizar(EmprestimoMaterial);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _EmprestimoMaterialService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}