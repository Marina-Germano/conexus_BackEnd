using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Presenca
{
    public int Idpresenca { get; set; }

    public int IdalunoTurma { get; set; }

    public int Idfuncionario { get; set; }

    public bool Presente { get; set; }

    public DateOnly Data { get; set; }

    public virtual AlunoTurma IdalunoTurmaNavigation { get; set; } = null!;

    public virtual Funcionario IdfuncionarioNavigation { get; set; } = null!;
}
