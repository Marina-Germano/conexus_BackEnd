using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class TipoDocumento
{
    public int IdtipoDocumento { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<DocumentoAluno> DocumentoAlunos { get; set; } = new List<DocumentoAluno>();
}
