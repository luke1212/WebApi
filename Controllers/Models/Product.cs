using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class Product
{
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public Boolean IsAvailable { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }
}