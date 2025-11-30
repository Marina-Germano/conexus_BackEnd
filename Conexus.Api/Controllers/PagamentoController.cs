using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Conexus.Api.Aplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Controllers;

public class PagamentoController : ApiControllerBase
{

    private readonly IPagamentoServiceDomain _PagamentoService;
    public PagamentoController(IPagamentoServiceDomain service) //injeção de dependencia
    {
        _PagamentoService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] PagamentoDTO Pagamento)
    {
        ApplicationResult<long> result = await _PagamentoService.Inserir(Pagamento);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("buscartodos")] //sempre especificar os verbos (o que eu quero que aconteça)
    public async Task<IActionResult> BuscarTodos()
    {
        var result = await _PagamentoService.BuscarTodos();
        return StatusCode(result.StatusCode, result); //retorna o status code 200
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar(PagamentoDTO Pagamento)
    {
       var result = await _PagamentoService.Atualizar(Pagamento);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var result = await _PagamentoService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
    
}