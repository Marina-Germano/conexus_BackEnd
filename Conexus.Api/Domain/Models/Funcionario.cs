using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Funcionario
{
    public int Idfuncionario { get; set; }

    public int Idusuario { get; set; }

    public string? Cargo { get; set; }

    public string? Especialidade { get; set; }

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    public virtual ICollection<CalendarioAula> CalendarioAulas { get; set; } = new List<CalendarioAula>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;

    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}
