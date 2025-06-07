using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("username")]
    public string? Username { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("password_hash")]
    public string? PasswordHash { get; set; }

    [Column("google_id")]
    public string? GoogleId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    public Role? Role { get; set; } 

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
