using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class AlunoTurma //fica 'partial' mesmo?
{
    public int IdalunoTurma { get; set; }

    public int Idaluno { get; set; }

    public int Idturma { get; set; }

    public DateOnly? DataMatricula { get; set; }

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    public virtual Aluno IdalunoNavigation { get; set; } = null!;

    public virtual Turma IdturmaNavigation { get; set; } = null!;

    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
}
