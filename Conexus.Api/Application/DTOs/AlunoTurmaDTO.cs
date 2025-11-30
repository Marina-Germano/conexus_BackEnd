using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class AlunoTurmaDTO
{
    public int IdalunoTurma { get; set; }

    public int Idaluno { get; set; }

    public int Idturma { get; set; }

    public DateOnly? DataMatricula { get; set; }

}
