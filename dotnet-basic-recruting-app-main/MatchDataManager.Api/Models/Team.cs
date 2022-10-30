using System.ComponentModel.DataAnnotations;

namespace MatchDataManager.Api.Models;

public class Team : Entity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(55)]
    public string CoachName { get; set; }
}