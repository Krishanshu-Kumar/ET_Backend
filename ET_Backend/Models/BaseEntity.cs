using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models;

public class BaseEntity
{
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [Column("modified_by")]
    public string? ModifiedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Column("modified_at")]
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

}
