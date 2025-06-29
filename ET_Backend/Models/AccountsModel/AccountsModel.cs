using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models.AccountsModel;

[Table("accounts")]
public class AccountsModel : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Column("balance")]
    public decimal Balance { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = default!;
}
