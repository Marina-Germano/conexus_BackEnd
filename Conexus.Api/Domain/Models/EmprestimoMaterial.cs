using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class EmprestimoMaterial
{
    public int Idemprestimo { get; set; }

    public int Idaluno { get; set; }

    public int Idmaterial { get; set; }

    public DateOnly DataEmprestimo { get; set; }

    public DateOnly DataPrevistaDevolucao { get; set; }

    public DateOnly? DataDevolvido { get; set; }

    public string? Status { get; set; }

    public string? Observacoes { get; set; }

    public decimal? ValorMulta { get; set; }

    public virtual Aluno IdalunoNavigation { get; set; } = null!;

    public virtual Material IdmaterialNavigation { get; set; } = null!;
}
