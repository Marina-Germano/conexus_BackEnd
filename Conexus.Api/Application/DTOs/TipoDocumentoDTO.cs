using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class TipoDocumentoDTO
{
    public int IdtipoDocumento { get; set; }

    public string Descricao { get; set; } = null!;
}
