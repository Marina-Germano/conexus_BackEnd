using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class EmprestimoMaterialServiceDomain : IEmprestimoMaterialServiceDomain
{

    private readonly AppDbContext _context;
    public EmprestimoMaterialServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(EmprestimoMaterialDTO emprestimoMaterialDTO)
    {
        if(emprestimoMaterialDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        EmprestimoMaterial emprestimoMaterial = new EmprestimoMaterial();
        emprestimoMaterial.Idemprestimo = emprestimoMaterialDTO.Idemprestimo ;
        emprestimoMaterial.Idaluno = emprestimoMaterialDTO.Idaluno;
        emprestimoMaterial.Idmaterial = emprestimoMaterialDTO.Idmaterial;
        emprestimoMaterial.DataEmprestimo = emprestimoMaterialDTO.DataEmprestimo;
        emprestimoMaterial.DataPrevistaDevolucao = emprestimoMaterialDTO.DataPrevistaDevolucao;
        emprestimoMaterial.DataDevolvido = emprestimoMaterialDTO.DataDevolvido;
        emprestimoMaterial.Status = emprestimoMaterialDTO.Status;
        emprestimoMaterial.ValorMulta = emprestimoMaterialDTO.ValorMulta;

        await _context.EmprestimoMaterials.AddAsync(emprestimoMaterial); //adiciona o EmprestimoMaterial no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(emprestimoMaterial.Idemprestimo, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<EmprestimoMaterialDTO>>> BuscarTodos()
    {
        var emprestimoMaterials = _context.EmprestimoMaterials;
        var resultEmprestimoMaterials = await emprestimoMaterials.Select(e => new EmprestimoMaterialDTO
        {
            Idemprestimo = e.Idemprestimo,
            Idaluno = e.Idaluno,
            Idmaterial = e.Idmaterial,
            DataEmprestimo = e.DataEmprestimo,
            DataPrevistaDevolucao = e.DataPrevistaDevolucao,
            DataDevolvido = e.DataDevolvido,
            Status = e.Status,
            Observacoes = e.Observacoes,
            ValorMulta = e.ValorMulta
        }).ToListAsync();

        var result = ApplicationResult<List<EmprestimoMaterialDTO>>.Success(resultEmprestimoMaterials, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<EmprestimoMaterialDTO>> Atualizar(EmprestimoMaterialDTO emprestimoMaterialDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (emprestimoMaterialDTO == null || emprestimoMaterialDTO.Idemprestimo <= 0)
        {
            return ApplicationResult<EmprestimoMaterialDTO>.Failure(info.Message, 400);
        }

        var result = await _context.EmprestimoMaterials.Where(e => e.Idemprestimo == emprestimoMaterialDTO.Idemprestimo).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<EmprestimoMaterialDTO>.Failure(info.Message, 400);

        result.Idemprestimo = emprestimoMaterialDTO.Idemprestimo;
        result.Idaluno = emprestimoMaterialDTO.Idaluno;
        result.Idmaterial = emprestimoMaterialDTO.Idmaterial;
        result.DataEmprestimo = emprestimoMaterialDTO.DataEmprestimo;
        result.DataPrevistaDevolucao = emprestimoMaterialDTO.DataPrevistaDevolucao;
        result.DataDevolvido = emprestimoMaterialDTO.DataDevolvido;
        result.Status = emprestimoMaterialDTO.Status;
        result.Observacoes = emprestimoMaterialDTO.Observacoes;
        result.ValorMulta = emprestimoMaterialDTO.ValorMulta;

        _context.Entry<EmprestimoMaterial>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<EmprestimoMaterialDTO>.Success(emprestimoMaterialDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<EmprestimoMaterialDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<EmprestimoMaterialDTO>.Failure(info.Message, 400);
        }

        var result = await _context.EmprestimoMaterials.Where(e => e.Idemprestimo == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<EmprestimoMaterialDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<EmprestimoMaterial>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var emprestimoMaterialDTO = new EmprestimoMaterialDTO
        {
            Idemprestimo = result.Idemprestimo,
            Idaluno = result.Idaluno,
            Idmaterial = result.Idmaterial,
            DataEmprestimo = result.DataEmprestimo,
            DataPrevistaDevolucao = result.DataPrevistaDevolucao,
            DataDevolvido = result.DataDevolvido,
            Status = result.Status,
            Observacoes = result.Observacoes,
            ValorMulta = result.ValorMulta,
        };

        return ApplicationResult<EmprestimoMaterialDTO>.Success(emprestimoMaterialDTO, message: info.Message);
    }
    
}