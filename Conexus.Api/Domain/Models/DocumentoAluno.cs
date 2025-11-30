using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class DocumentoAluno
{
    public int Iddocumento { get; set; }

    public int Idaluno { get; set; }

    public int IdtipoDocumento { get; set; }

    public string? CaminhoArquivo { get; set; }

    public DateTime? DataEnvio { get; set; }

    public string? Observacoes { get; set; }

    public string? StatusDocumento { get; set; }

    public virtual Aluno IdalunoNavigation { get; set; } = null!;

    public virtual TipoDocumento IdtipoDocumentoNavigation { get; set; } = null!;
}
