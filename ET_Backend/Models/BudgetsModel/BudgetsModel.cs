using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models.BudgetsModel;

[Table("budgets")]
public class BudgetsModel : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("category_id")]
    public int CategoryID { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = string.Empty;

    [Column("month")]
    public int Month { get; set; } 

    [Column("year")]
    public int Year { get; set; } 
}
