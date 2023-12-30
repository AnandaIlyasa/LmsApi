using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Model;

public abstract class BaseModel
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Timestamp]
    public uint Ver { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
}
