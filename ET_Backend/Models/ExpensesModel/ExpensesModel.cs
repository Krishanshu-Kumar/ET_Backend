using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models.ExpensesModel;

[Table("expenses")]
public class ExpensesModel : BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("account_id")]
    public Guid AccountId { get; set; }

    [Column("category_id")]
    public int CatgoryId { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("expense_date")]
    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
}
