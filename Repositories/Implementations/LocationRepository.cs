using Microsoft.EntityFrameworkCore;
using RM_API.Models;
using RM_API.Repositories.Interfaces;

namespace RM_API.Repositories.Implementations
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(RM_APIContext repositoryContext) : base(repositoryContext) { }

        public void DeleteLocation(Location location)
        {
            Delete(location);
            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }

        public void DeleteAllLocationsByRouteId(long routeId)
        {
            var locations = FindByCondition(l => l.RouteId == routeId)
                .ToList();
            foreach (var location in locations)
            {
                Delete(location);
            }
            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return FindAll()
                .ToList();
        }

        public void Save(Location location)
        {
            if (location.Id == 0) Create(location);
            else Update(location);

            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }

        public Location GetLocationByAddress(string address)
        {
            return FindByCondition(l => l.Address == address)
                .FirstOrDefault();
        }

        public Location GetLocationById(long id)
        {
            return FindByCondition(l => l.Id == id)
                .Include(l => l.Route)
                .FirstOrDefault();
        }

        public IEnumerable<Location> GetLocationsAfterOrder(int order)
        {
            return FindByCondition(l => l.Order > order)
                .OrderBy(l => l.Order)
                .ToList();
        }
    }
}
