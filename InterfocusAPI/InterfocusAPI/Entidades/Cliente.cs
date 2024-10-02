using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterfocusAPI.Entidades;

public class Cliente
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int ClienteId { get; set; }
    [Required]
    public string? Nome { get; set; }
    [Required]
    public string? CPF { get; set; }
    [Required]
    public DateTime? DataNasc {  get; set; }
    public string? Email {  get; set; }
    [JsonIgnore]
    public ICollection<Divida>? Dividas { get; set; } = new List<Divida>();
}
