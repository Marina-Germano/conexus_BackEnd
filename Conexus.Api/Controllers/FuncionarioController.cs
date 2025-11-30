using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class FuncionarioController : ApiControllerBase
{

    private readonly IFuncionarioServiceDomain _FuncionarioService;
    public FuncionarioController(IFuncionarioServiceDomain service) //injeção de dependencia
    {
        _FuncionarioService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] FuncionarioDTO Funcionario)
    {
        ApplicationResult<long> result = await _FuncionarioService.Inserir(Funcionario);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _FuncionarioService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(FuncionarioDTO Funcionario)
    {
       var result = await _FuncionarioService.Atualizar(Funcionario);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _FuncionarioService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}