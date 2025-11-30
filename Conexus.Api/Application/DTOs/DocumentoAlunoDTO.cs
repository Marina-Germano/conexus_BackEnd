using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class DocumentoAlunoDTO
{
    public int Iddocumento { get; set; }

    public int Idaluno { get; set; }

    public int IdtipoDocumento { get; set; }

    public string? CaminhoArquivo { get; set; }

    public DateTime? DataEnvio { get; set; }

    public string? Observacoes { get; set; }

    public string? StatusDocumento { get; set; }
}
