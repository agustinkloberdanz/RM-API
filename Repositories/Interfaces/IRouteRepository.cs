using Route = RM_API.Models.Route;

namespace RM_API.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        IEnumerable<Route> GetAllRoutes();
        Route FindRouteById(long id);
        IEnumerable<Route> FindRouteByDrivingDate(DateTime date);
        void Save(Route Route);
        void DeleteRoute(Route Route);
    }
}
