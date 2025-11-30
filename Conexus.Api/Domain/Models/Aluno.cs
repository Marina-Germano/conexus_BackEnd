using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conexus.Api.Domain.Models;

public partial class Aluno
{
    // [Key]
    public int Idaluno { get; set; }

    public int Idusuario { get; set; }

    public string Cep { get; set; } = string.Empty;


    public string Rua { get; set; } = string.Empty;


    public string Numero { get; set; } = string.Empty;


    public string Bairro { get; set; } = string.Empty;


    public string? Complemento { get; set; }


    public string? Responsavel { get; set; }


    public string? TelResponsavel { get; set; }

    public string Situacao { get; set; } = string.Empty;

    public virtual ICollection<AlunoTurma> AlunoTurmas { get; set; } = new List<AlunoTurma>();

    public virtual ICollection<Cartao> Cartaos { get; set; } = new List<Cartao>();

    public virtual ICollection<DocumentoAluno> DocumentoAlunos { get; set; } = new List<DocumentoAluno>();

    public virtual ICollection<EmprestimoMaterial> EmprestimoMaterials { get; set; } = new List<EmprestimoMaterial>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
