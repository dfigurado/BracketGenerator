using Domain.Common.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Scores")]
public class Score : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int TeamBScore { get; set; }
    public int TeamAScore { get; set; }
    public bool IsFullTime { get; set; }
    public bool IsExtraTime { get; set; }
}