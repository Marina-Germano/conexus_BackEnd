using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class CartaoController : ApiControllerBase
{

    private readonly ICartaoServiceDomain _CartaoService;
    public CartaoController(ICartaoServiceDomain service) //injeção de dependencia
    {
        _CartaoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] CartaoDTO Cartao)
    {
        ApplicationResult<long> result = await _CartaoService.Inserir(Cartao);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _CartaoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(CartaoDTO Cartao)
    {
       var result = await _CartaoService.Atualizar(Cartao);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _CartaoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}