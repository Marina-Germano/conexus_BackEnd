using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Turma
{
    public int Idturma { get; set; }

    public int Ididioma { get; set; }

    public int Idnivel { get; set; }

    public int Idfuncionario { get; set; }

    public string Descricao { get; set; } = null!;

    public string? DiasSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public int CapacidadeMaxima { get; set; }

    public string? Sala { get; set; }

    public string? Imagem { get; set; }

    public string? TipoRecorrencia { get; set; }

    public virtual ICollection<AlunoTurma> AlunoTurmas { get; set; } = new List<AlunoTurma>();

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    public virtual ICollection<CalendarioAula> CalendarioAulas { get; set; } = new List<CalendarioAula>();

    public virtual Funcionario IdfuncionarioNavigation { get; set; } = null!;

    public virtual Idioma IdidiomaNavigation { get; set; } = null!;

    public virtual Nivel IdnivelNavigation { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
