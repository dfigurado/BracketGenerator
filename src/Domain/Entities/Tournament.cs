using Domain.Common.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Tournaments")]
public class Tournament : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public DateTimeOffset TournamentStartDate { get; set; }

    [Required]
    public DateTimeOffset TournamentEndDate { get; set; }

    public ICollection<Team>? Teams { get; set; }

    public ICollection<Match>? Matches { get; set; }
}