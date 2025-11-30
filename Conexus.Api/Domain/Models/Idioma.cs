using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Idioma
{
    public int Ididioma { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}
