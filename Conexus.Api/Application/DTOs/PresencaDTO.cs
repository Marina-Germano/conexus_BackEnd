using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class PresencaDTO
{
    public int Idpresenca { get; set; }

    public int IdalunoTurma { get; set; }

    public int Idfuncionario { get; set; }

    public bool Presente { get; set; }

    public DateOnly Data { get; set; }
}
