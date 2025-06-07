using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ET_Backend.Models;

[Table("roles")] 
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;
}
