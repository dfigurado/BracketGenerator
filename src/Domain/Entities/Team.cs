using Domain.Common.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Team : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Seed { get; set; }

    [Required]
    public string? TeamName { get; set; }

    [Required]
    public int EloRating { get; set; }

    [Required]
    public int TournamentId { get; set; }

    [ForeignKey("TournamentId")]
    public Tournament? Tournament { get; set; }
}