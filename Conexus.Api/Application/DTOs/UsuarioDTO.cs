using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class UsuarioDTO
{
    public int Idusuario { get; set; }

    public string Nome { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DataNascimento { get; set; }

    public string Cpf { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Papel { get; set; } = null!;

    public bool? Ativo { get; set; }

    public string? Foto { get; set; }

    public int? TentativasLogin { get; set; }

    public bool? Bloqueado { get; set; }
}
