using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterfocusAPI.Entidades;

public class Cliente
{
    public int ClienteId { get; set; }
    [Required]
    public string? Nome { get; set; }
    [Required]
    public string? CPF { get; set; }
    [Required]
    public DateTime? DataNasc {  get; set; }
    [Required]
    public string? Email {  get; set; }
    public ICollection<Divida>? Dividas { get; set; } = new List<Divida>();
}
