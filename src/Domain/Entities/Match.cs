using Domain.Common.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Matches")]
public class Match : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required]
    public int TeamAId { get; set; }

    [ForeignKey("TeamAId")]
    public Team? TeamA { get; set; }

    [Required]
    public int TeamBId { get; set; }

    [ForeignKey("TeamBId")]
    public Team? TeamB { get; set; }

    [Required]
    public DateTimeOffset MatchDate { get; set; }

    [Required]
    public int ScoreId { get; set; }

    [ForeignKey("ScoreId")]
    public Score? Score { get; set; }

    [Required]
    public int TournamentId { get; set; }

    [ForeignKey("TournamentId")]
    public virtual Tournament? Tournament { get; set; }
}