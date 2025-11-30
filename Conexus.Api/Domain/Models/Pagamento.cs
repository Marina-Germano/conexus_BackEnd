using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Pagamento
{
    public int Idpagamento { get; set; }

    public int IdformaPagamento { get; set; }

    public int Idaluno { get; set; }

    public decimal? Valor { get; set; }

    public DateOnly? DataVencimento { get; set; }

    public string? StatusPagamento { get; set; }

    public DateOnly? DataPagamento { get; set; }

    public decimal? ValorPago { get; set; }

    public string? Observacoes { get; set; }

    public decimal? Multa { get; set; }

    public virtual Aluno IdalunoNavigation { get; set; } = null!;

    public virtual FormaPagamento IdformaPagamentoNavigation { get; set; } = null!;
}
