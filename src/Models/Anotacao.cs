using System.ComponentModel.DataAnnotations;

namespace AppMvcFuncional.Models;

public class Anotacao
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Range(0, 5, ErrorMessage = "O campo {0} de estar entre {1} e {2}")]
    public int Prioridade { get; set; } = 0;


    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string SubTitulo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public DateOnly DataDeCriacao { get; set; }


    public bool Editado { get; set; } = false;
    public DateOnly? DataEdicao { get; set; }
    public bool Excluido { get; set; } = false;

    public DateOnly? DataExclusao { get; set; }
}
