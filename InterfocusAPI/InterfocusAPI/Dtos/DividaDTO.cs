using System.ComponentModel.DataAnnotations;

namespace InterfocusAPI.Dtos;

public class DividaDTO
{
    [Required]
    public bool Situacao { get; set; }
}
