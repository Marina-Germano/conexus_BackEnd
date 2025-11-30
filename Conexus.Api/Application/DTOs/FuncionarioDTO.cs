using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class FuncionarioDTO
{
    public int Idfuncionario { get; set; }

    public int Idusuario { get; set; }

    public string? Cargo { get; set; }

    public string? Especialidade { get; set; }
}
