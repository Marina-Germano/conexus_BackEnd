using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Cartao
{
    public int Idcartao { get; set; }

    public int Idaluno { get; set; }

    public string NomeTitular { get; set; } = null!;

    public string Bandeira { get; set; } = null!;

    public string UltimosDigitos { get; set; } = null!;

    public string NumeroCriptografado { get; set; } = null!;

    public string ValidadeMes { get; set; } = null!;

    public string ValidadeAno { get; set; } = null!;

    public virtual Aluno IdalunoNavigation { get; set; } = null!;
}
