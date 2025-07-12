using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models.CategoriesModel;

[Table("categories")]
public class CategoriesModel : BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Column("icon")]
    public string Icon { get; set; } = string.Empty;

    [Column("color")]
    public string Color { get; set; } = string.Empty;
}
