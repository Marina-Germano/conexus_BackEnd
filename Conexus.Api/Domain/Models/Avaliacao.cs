using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Avaliacao
{
    public int Idavaliacao { get; set; }

    public int IdalunoTurma { get; set; }

    public int Idfuncionario { get; set; }

    public int Idturma { get; set; }

    public string Descricao { get; set; } = null!;

    public string? Titulo { get; set; }

    public DateOnly DataAvaliacao { get; set; }

    public decimal? Nota { get; set; }

    public decimal? Peso { get; set; }

    public string? Observacao { get; set; }

    public virtual AlunoTurma IdalunoTurmaNavigation { get; set; } = null!;

    public virtual Funcionario IdfuncionarioNavigation { get; set; } = null!;

    public virtual Turma IdturmaNavigation { get; set; } = null!;
}
