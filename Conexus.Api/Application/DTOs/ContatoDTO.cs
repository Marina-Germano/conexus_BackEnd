using System;
using System.Collections.Generic;

namespace Conexus.Api.Aplication.DTOs;

public partial class ContatoDTO
{
    public int Idcontato { get; set; }

    public int Idusuario { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string? Arquivo { get; set; }

    public string MotivoContato { get; set; } = null!;

    public string Mensagem { get; set; } = null!;

}
