using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class MaterialDTO
{
    public int Idmaterial { get; set; }

    public int IdtipoMaterial { get; set; }

    public int Ididioma { get; set; }

    public int Idnivel { get; set; }

    public int? Idturma { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descricao { get; set; }

    public int Quantidade { get; set; }

    public string? FormatoArquivo { get; set; }

    public string? Arquivo { get; set; }

    public DateTime? DataCadastro { get; set; }
}
