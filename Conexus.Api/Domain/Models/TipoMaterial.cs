using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class TipoMaterial
{
    public int IdtipoMaterial { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
