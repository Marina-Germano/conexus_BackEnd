using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class TurmaDTO
{
    public int Idturma { get; set; }

    public int Ididioma { get; set; }

    public int Idnivel { get; set; }

    public int Idfuncionario { get; set; }

    public string Descricao { get; set; } = null!;

    public string? DiasSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public int CapacidadeMaxima { get; set; }

    public string? Sala { get; set; }

    public string? Imagem { get; set; }

    public string? TipoRecorrencia { get; set; }
}
