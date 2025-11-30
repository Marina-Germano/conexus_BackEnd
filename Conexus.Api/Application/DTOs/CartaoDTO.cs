using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class CartaoDTO
{
    public int Idcartao { get; set; }

    public int Idaluno { get; set; }

    public string NomeTitular { get; set; } = null!;

    public string Bandeira { get; set; } = null!;

    public string UltimosDigitos { get; set; } = null!;

    public string NumeroCriptografado { get; set; } = null!;

    public string ValidadeMes { get; set; } = null!;

    public string ValidadeAno { get; set; } = null!;
}
