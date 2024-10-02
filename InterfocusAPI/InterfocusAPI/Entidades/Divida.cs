using System.Text.Json.Serialization;

namespace InterfocusAPI.Entidades;

public class Divida
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int DividaId { get; set; }
    public int Valor {  get; set; }
    public bool Situacao { get; set; }
    public DateTime? DataCriacao { get; set; }
    [JsonIgnore]
    public DateTime? DataPagamento { get; set; }
    public string? Descricao { get; set; }
    [JsonIgnore]
    public int ClienteId { get; set; }
    [JsonIgnore]
    public Cliente? Cliente { get; set; }
}
