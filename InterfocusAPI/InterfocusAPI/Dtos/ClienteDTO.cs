using System.ComponentModel.DataAnnotations;

namespace InterfocusAPI.Dtos;

public class ClienteDTO
{
    [Required]
    public string? Nome { get; set; }
    [Required]
    public string? CPF { get; set; }
    [Required]
    public DateTime? DataNasc { get; set; }
    public string? Email { get; set; }
}
