using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class ContatoServiceDomain : IContatoServiceDomain
{
    private readonly AppDbContext _context;
    public ContatoServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(ContatoDTO contatoDTO)
    {
        if(contatoDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Contato contato = new Contato();
        contato.Idcontato = contatoDTO.Idcontato ;
        contato.Idusuario = contatoDTO.Idusuario;
        contato.Nome = contatoDTO.Nome;
        contato.Email = contatoDTO.Email;
        contato.Telefone = contatoDTO.Telefone;
        contato.Arquivo = contatoDTO.Arquivo;
        contato.MotivoContato = contatoDTO.MotivoContato;
        contato.Mensagem = contatoDTO.Mensagem;

        await _context.Contatos.AddAsync(contato); //adiciona o Contato no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(contato.Idcontato, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<ContatoDTO>>> BuscarTodos()
    {
        var contatos = _context.Contatos;
        var resultContatos = await contatos.Select(e => new ContatoDTO
        {
            Idcontato = e.Idcontato,
            Idusuario = e.Idusuario,
            Nome = e.Nome,
            Email = e.Email,
            Telefone = e.Telefone,
            Arquivo = e.Arquivo,
            MotivoContato = e.MotivoContato,
            Mensagem = e.Mensagem
        }).ToListAsync();

        var result = ApplicationResult<List<ContatoDTO>>.Success(resultContatos, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<ContatoDTO>> Atualizar(ContatoDTO contatoDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (contatoDTO == null || contatoDTO.Idcontato <= 0)
        {
            return ApplicationResult<ContatoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Contatos.Where(e => e.Idcontato == contatoDTO.Idcontato).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<ContatoDTO>.Failure(info.Message, 400);

        result.Idcontato = contatoDTO.Idcontato;
        result.Idusuario = contatoDTO.Idusuario;
        result.Nome = contatoDTO.Nome;
        result.Email = contatoDTO.Email;
        result.Telefone = contatoDTO.Telefone;
        result.Arquivo = contatoDTO.Arquivo;
        result.MotivoContato = contatoDTO.MotivoContato;
        result.Mensagem = contatoDTO.Mensagem;

        _context.Entry<Contato>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<ContatoDTO>.Success(contatoDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<ContatoDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<ContatoDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Contatos.Where(e => e.Idcontato == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<ContatoDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Contato>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var contatoDTO = new ContatoDTO
        {
            Idcontato = result.Idcontato,
            Idusuario = result.Idusuario,
            Nome = result.Nome,
            Email = result.Email,
            Telefone = result.Telefone,
            Arquivo = result.Arquivo,
            MotivoContato = result.MotivoContato,
            Mensagem = result.Mensagem
        };

        return ApplicationResult<ContatoDTO>.Success(contatoDTO, message: info.Message);
    }
}