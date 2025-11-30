using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class FormaPagamentoController : ApiControllerBase
{

    private readonly IFormaPagamentoServiceDomain _FormaPagamentoService;
    public FormaPagamentoController(IFormaPagamentoServiceDomain service) //injeção de dependencia
    {
        _FormaPagamentoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] FormaPagamentoDTO FormaPagamento)
    {
        ApplicationResult<long> result = await _FormaPagamentoService.Inserir(FormaPagamento);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _FormaPagamentoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(FormaPagamentoDTO FormaPagamento)
    {
       var result = await _FormaPagamentoService.Atualizar(FormaPagamento);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _FormaPagamentoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}