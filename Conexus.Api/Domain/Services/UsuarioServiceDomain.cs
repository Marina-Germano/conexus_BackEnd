using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;
using Conexus.Api.Infra;
using Conexus.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conexus.Api.Aplication;

namespace Conexus.Api.Domain.Services;

public class UsuarioServiceDomain : IUsuarioServiceDomain
{

    private readonly AppDbContext _context;
    public UsuarioServiceDomain(AppDbContext context) //injeção de dependencia
    {
        _context = context;
    }

    [HttpPost("inserir")]
    public async Task<ApplicationResult<long>> Inserir(UsuarioDTO usuarioDTO)
    {
        if(usuarioDTO == null)
        {
            return ApplicationResult<long>.Failure("Dados inválidos.", 400);
        }
        
        Usuario usuario = new Usuario();
        usuario.Idusuario = usuarioDTO.Idusuario ;
        usuario.Nome = usuarioDTO.Nome;
        usuario.Telefone = usuarioDTO.Telefone;
        usuario.Email = usuarioDTO.Email;
        usuario.DataNascimento = usuarioDTO.DataNascimento;
        usuario.Cpf = usuarioDTO.Cpf;
        usuario.Senha = usuarioDTO.Senha;
        usuario.Papel = usuarioDTO.Papel;
        usuario.Ativo = usuarioDTO.Ativo;
        usuario.Foto = usuarioDTO.Foto;
        usuario.TentativasLogin = usuarioDTO.TentativasLogin;
        usuario.Bloqueado = usuarioDTO.Bloqueado;

        await _context.Usuarios.AddAsync(usuario); //adiciona o Usuario no contexto
        await _context.SaveChangesAsync(); //salva as alterações no banco de dados

        var result = ApplicationResult<long>.Success(usuario.Idusuario, 200, "Registro Salvo com Sucesso.");
        return result;
    }

    public async Task<ApplicationResult<List<UsuarioDTO>>> BuscarTodos()
    {
        var usuarios = _context.Usuarios;
        var resultUsuarios = await usuarios.Select(e => new UsuarioDTO
        {
            Idusuario = e.Idusuario,
            Nome = e.Nome,
            Telefone = e.Telefone,
            Email = e.Email,
            DataNascimento = e.DataNascimento,
            Cpf = e.Cpf,
            Senha = e.Senha,
            Papel = e.Papel,
            Ativo = e.Ativo,
            Foto = e.Foto,
            TentativasLogin = e.TentativasLogin,
            Bloqueado = e.Bloqueado
        }).ToListAsync();

        var result = ApplicationResult<List<UsuarioDTO>>.Success(resultUsuarios, 200, "Registros Localizados.");

        return result;
    }

    public async Task<ApplicationResult<UsuarioDTO>> Atualizar(UsuarioDTO usuarioDTO)
    {
        var info = new {Message = "Parâmetros inválidos." };
        
        if (usuarioDTO == null || usuarioDTO.Idusuario <= 0)
        {
            return ApplicationResult<UsuarioDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Usuarios.Where(e => e.Idusuario == usuarioDTO.Idusuario).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
        }
        ApplicationResult<UsuarioDTO>.Failure(info.Message, 400);

        result.Idusuario = usuarioDTO.Idusuario;
        result.Nome = usuarioDTO.Nome;
        result.Telefone = usuarioDTO.Telefone;
        result.Email = usuarioDTO.Email;
        result.DataNascimento = usuarioDTO.DataNascimento;
        result.Cpf = usuarioDTO.Cpf;
        result.Senha = usuarioDTO.Senha;
        result.Papel = usuarioDTO.Papel;
        result.Ativo = usuarioDTO.Ativo;
        result.Foto = usuarioDTO.Foto;
        result.TentativasLogin = usuarioDTO.TentativasLogin;
        result.Bloqueado = usuarioDTO.Bloqueado;

        _context.Entry<Usuario>(result).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados alterados" };

        return ApplicationResult<UsuarioDTO>.Success(usuarioDTO, message: info.Message);
    }
    
    [HttpDelete("excluir")]
    public async Task<ApplicationResult<UsuarioDTO>> Excluir(int id)
    {
        var info = new { Message = "Parâmetros inválidos." };

        if (id <= 0)
        {
            return ApplicationResult<UsuarioDTO>.Failure(info.Message, 400);
        }

        var result = await _context.Usuarios.Where(e => e.Idusuario == id).FirstOrDefaultAsync();

        if (result == null)
        {
            info = new { Message = "Registro não encontrado." };
            return ApplicationResult<UsuarioDTO>.Failure(info.Message, 400);
        }
        
        _context.Entry<Usuario>(result).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluidos" };
        
        var usuarioDTO = new UsuarioDTO
        {
            Idusuario = result.Idusuario,
            Nome = result.Nome,
            Telefone = result.Telefone,
            Email = result.Email,
            DataNascimento = result.DataNascimento,
            Cpf = result.Cpf,
            Senha = result.Senha,
            Papel = result.Papel,
            Ativo = result.Ativo,
            Foto = result.Foto,
            TentativasLogin = result.TentativasLogin,
            Bloqueado = result.Bloqueado
        };

        return ApplicationResult<UsuarioDTO>.Success(usuarioDTO, message: info.Message);
    }
    
}