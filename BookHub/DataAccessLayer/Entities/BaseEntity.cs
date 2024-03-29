using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}