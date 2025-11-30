using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class UsuarioController : ApiControllerBase
{

    private readonly IUsuarioServiceDomain _UsuarioService;
    public UsuarioController(IUsuarioServiceDomain service) //injeção de dependencia
    {
        _UsuarioService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] UsuarioDTO Usuario)
    {
        ApplicationResult<long> result = await _UsuarioService.Inserir(Usuario);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _UsuarioService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(UsuarioDTO Usuario)
    {
       var result = await _UsuarioService.Atualizar(Usuario);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _UsuarioService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}