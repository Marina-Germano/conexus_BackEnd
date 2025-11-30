using System.ComponentModel.DataAnnotations;

namespace Conexus.Api.Aplication.DTOs;

public class AlunoDTO
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Nome obrigatório")] //not null no banco de dados
    // [MinLength(6)]
    // [MaxLength(500, ErrorMessage = "Ultrapassouo limite excedido!")] //define apenas tamanho maximo
    
    public int Idaluno { get; set; }   //ID vai ser gerado automaticamente pelo banco

    public int Idusuario { get; set; }

    public string Cep { get; set; } = string.Empty;


    public string Rua { get; set; } = string.Empty;


    public string Numero { get; set; } = string.Empty;


    public string Bairro { get; set; } = string.Empty;


    public string? Complemento { get; set; }


    public string? Responsavel { get; set; }


    public string? TelResponsavel { get; set; }

    public string Situacao { get; set; } = string.Empty;

}