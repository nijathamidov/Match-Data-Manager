using MatchDataManager.Api.Data;
using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public interface ITeamsRepository
{
    void AddTeam(Team team);
    void DeleteTeam(Guid teamId);
    IEnumerable<Team> GetAllTeams();
    Team GetTeamById(Guid id);
    void UpdateTeam(Team team);
}
public  class TeamsRepository:ITeamsRepository
{
    private ApplicationDbContext _db;

    public TeamsRepository(ApplicationDbContext db)
    {
        _db = db;

    }

    public  void AddTeam(Team team)
    {
        team.Id = Guid.NewGuid();
        if (!_db.Teams.Any(x => x.Name == team.Name))
        {
            _db.Teams.Add(team);
            _db.SaveChanges();
        }
        else
        {
            throw new ArgumentException("This team is already exist.", nameof(team));
        }
    }

    public  void DeleteTeam(Guid teamId)
    {
        var team = _db.Teams.FirstOrDefault(x => x.Id == teamId);
        if (team is not null)
        {
            _db.Teams.Remove(team);
            _db.SaveChanges();
        }
    }

    public  IEnumerable<Team> GetAllTeams()
    {
        return _db.Teams.ToList();
    }

    public  Team GetTeamById(Guid id)
    {
        return _db.Teams.FirstOrDefault(x => x.Id == id);
    }

    public  void UpdateTeam(Team team)
    {
        var existingTeam = _db.Teams.FirstOrDefault(x => x.Id == team.Id);
        if (existingTeam is null || team is null)
        {
            throw new ArgumentException("Team doesn't exist.", nameof(team));
        }
        else if (existingTeam.Name != team.Name && _db.Locations.Any(x => x.Name == team.Name))
        {
            throw new ArgumentException("This team is already exist.", nameof(team));
        }

        existingTeam.CoachName = team.CoachName;
        existingTeam.Name = team.Name;
        _db.SaveChanges();
    }
}