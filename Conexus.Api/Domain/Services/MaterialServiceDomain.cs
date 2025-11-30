using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class MaterialServiceDomain : IMaterialServiceDomain
{

    private readonly AppDbContext _context;
    public MaterialServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(MaterialDTO materialDTO)
    {
        if(materialDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Material material = new Material();
        material.Idmaterial = materialDTO.Idmaterial ;
        material.IdtipoMaterial = materialDTO.IdtipoMaterial;
        material.Ididioma = materialDTO.Ididioma;
        material.Idnivel = materialDTO.Idnivel;
        material.Idturma = materialDTO.Idturma;
        material.Titulo = materialDTO.Titulo;
        material.Descricao = materialDTO.Descricao;
        material.Quantidade = materialDTO.Quantidade;
        material.FormatoArquivo = materialDTO.FormatoArquivo;
        material.Arquivo = materialDTO.Arquivo;
        material.DataCadastro = materialDTO.DataCadastro;

        await _context.Materials.AddAsync(material); //adiciona o Material no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(material.Idmaterial, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<MaterialDTO>>> BuscarTodos()
    {
        var materials = _context.Materials;
        var resultMaterials = await materials.Select(e => new MaterialDTO
        {
            Idmaterial = e.Idmaterial,
            IdtipoMaterial = e.IdtipoMaterial,
            Ididioma = e.Ididioma,
            Idnivel = e.Idnivel,
            Idturma = e.Idturma,
            Titulo = e.Titulo,
            Descricao = e.Descricao,
            Quantidade = e.Quantidade,
            FormatoArquivo = e.FormatoArquivo,
            Arquivo = e.Arquivo,
            DataCadastro = e.DataCadastro
        }).ToListAsync();

        var result = ApplicationResult<List<MaterialDTO>>.Success(resultMaterials, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<MaterialDTO>> Atualizar(MaterialDTO materialDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (materialDTO == null || materialDTO.Idmaterial <= 0)
        {
            return ApplicationResult<MaterialDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Materials.Where(e => e.Idmaterial == materialDTO.Idmaterial).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<MaterialDTO>.Failure(info.Message, 400);

        result.Idmaterial = materialDTO.Idmaterial;
        result.IdtipoMaterial = materialDTO.IdtipoMaterial;
        result.Ididioma = materialDTO.Ididioma;
        result.Idnivel = materialDTO.Idnivel;
        result.Idturma = materialDTO.Idturma;
        result.Titulo = materialDTO.Titulo;
        result.Descricao = materialDTO.Descricao;
        result.Quantidade = materialDTO.Quantidade;
        result.FormatoArquivo = materialDTO.FormatoArquivo;
        result.Arquivo = materialDTO.Arquivo;
        result.DataCadastro = materialDTO.DataCadastro;

        _context.Entry<Material>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<MaterialDTO>.Success(materialDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<MaterialDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<MaterialDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Materials.Where(e => e.Idmaterial == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<MaterialDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Material>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var materialDTO = new MaterialDTO
        {
            Idmaterial = result.Idmaterial,
            IdtipoMaterial = result.IdtipoMaterial,
            Ididioma = result.Ididioma,
            Idnivel = result.Idnivel,
            Idturma = result.Idturma,
            Titulo = result.Titulo,
            Descricao = result.Descricao,
            Quantidade = result.Quantidade,
            FormatoArquivo = result.FormatoArquivo,
            Arquivo = result.Arquivo,
            DataCadastro = result.DataCadastro
        };

        return ApplicationResult<MaterialDTO>.Success(materialDTO, message: info.Message);
    }
    
}