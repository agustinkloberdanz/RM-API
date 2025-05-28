using RM_API.Models;

namespace RM_API.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAllLocations();
        Location GetLocationByAddress(string address);
        Location GetLocationById(long id);
        void DeleteLocation(Location location);
        void DeleteAllLocationsByRouteId(long routeId);
        void Save(Location location);
        IEnumerable<Location> GetLocationsAfterOrder(int order);
    }
}
