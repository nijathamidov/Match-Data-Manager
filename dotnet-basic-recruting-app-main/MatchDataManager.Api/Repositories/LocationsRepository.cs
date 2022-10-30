using MatchDataManager.Api.Data;
using MatchDataManager.Api.Models;

namespace MatchDataManager.Api.Repositories;

public interface ILocationsRepository
{
    void AddLocation(Location location);
    void DeleteLocation(Guid locationId);
    IEnumerable<Location> GetAllLocations();
    Location GetLocationById(Guid id);
    void UpdateLocation(Location location);
}
public class LocationsRepository: ILocationsRepository
{
    private  ApplicationDbContext _db ;

    public LocationsRepository(ApplicationDbContext db)
    {
        _db = db;

    }

    public void AddLocation(Location location)
    {
        location.Id = Guid.NewGuid();
        if (!_db.Locations.Any(x=>x.Name==location.Name))
        {
            _db.Locations.Add(location);
            _db.SaveChanges();
        }
        else
        {
            throw new ArgumentException(" This location is already exist.", nameof(location));
        }
    }

    public void DeleteLocation(Guid locationId)
    {
        var location = _db.Locations.FirstOrDefault(x => x.Id == locationId);
        if (location is not null)
        {
            _db.Locations.Remove(location);
            _db.SaveChanges();
        }
    }

    public IEnumerable<Location> GetAllLocations()
    {
        return _db.Locations.ToList();
    }

    public Location GetLocationById(Guid id)
    {
        return _db.Locations.FirstOrDefault(x => x.Id == id);
    }

    public void UpdateLocation(Location location)
    {
        var existingLocation = _db.Locations.FirstOrDefault(x => x.Id == location.Id);
        if (existingLocation is null || location is null)
        {
            throw new ArgumentException("Location doesn't exist.", nameof(location));
        }
        else if(existingLocation.Name != location.Name && _db.Locations.Any(x => x.Name == location.Name))
        {
            throw new ArgumentException("This location is already exist.", nameof(location));
        }
        existingLocation.City = location.City;
        existingLocation.Name = location.Name;
        _db.SaveChanges();
    }
}