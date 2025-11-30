using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Usuario
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

    public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}
