using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class EmprestimoMaterialDTO
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
}
