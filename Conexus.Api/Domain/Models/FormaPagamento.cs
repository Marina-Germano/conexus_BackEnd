using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class FormaPagamento
{
    public int IdformaPagamento { get; set; }

    public string? FormaPagamento1 { get; set; }

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
