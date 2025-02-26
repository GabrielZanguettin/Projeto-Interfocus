using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterfocusAPI.Entidades;

public class Divida
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int DividaId { get; set; }
    [Required]
    public int Valor {  get; set; }
    public bool Situacao { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataPagamento { get; set; }
    [Required]
    public string? Descricao { get; set; }
    public int ClienteId { get; set; }
    [JsonIgnore]
    public Cliente? Cliente { get; set; }
}
