using System;
using System.Collections.Generic;

namespace Conexus.Api.Domain.Models;

public partial class Material
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

    public virtual ICollection<CalendarioAula> CalendarioAulas { get; set; } = new List<CalendarioAula>();

    public virtual ICollection<EmprestimoMaterial> EmprestimoMaterials { get; set; } = new List<EmprestimoMaterial>();

    public virtual Idioma IdidiomaNavigation { get; set; } = null!;

    public virtual Nivel IdnivelNavigation { get; set; } = null!;

    public virtual TipoMaterial IdtipoMaterialNavigation { get; set; } = null!;

    public virtual Turma? IdturmaNavigation { get; set; }
}
