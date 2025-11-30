using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class CalendarioAula
{
    public int Idaula { get; set; }

    public DateOnly DataAula { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFim { get; set; }

    public int Idfuncionario { get; set; }

    public int Idturma { get; set; }

    public int Idmaterial { get; set; }

    public string? Sala { get; set; }

    public string? Observacoes { get; set; }

    public string? LinkReuniao { get; set; }

    public bool? AulaExtra { get; set; }

    public virtual Funcionario IdfuncionarioNavigation { get; set; } = null!;

    public virtual Material IdmaterialNavigation { get; set; } = null!;

    public virtual Turma IdturmaNavigation { get; set; } = null!;
}
