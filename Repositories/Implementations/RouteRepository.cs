using Microsoft.EntityFrameworkCore;
using RM_API.Models;
using RM_API.Repositories.Interfaces;
using Route = RM_API.Models.Route;

namespace RM_API.Repositories.Implementations
{
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        public RouteRepository(RM_APIContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Route> GetAllRoutes()
        {
            return FindAll()
                .Include(udi => udi.Locations)
                .ToList();
        }

        public Route FindRouteById(long id)
        {
            return FindByCondition(udi => udi.Id == id)
                .Include(udi => udi.Locations)
                .FirstOrDefault();
        }

        public IEnumerable<Route> FindRouteByDrivingDate(DateTime date)
        {
            return FindAll()
                .Where(udi => udi.DrivingDate.Date == date.Date)
                .Include(udi => udi.Locations)
                .ToList();
        }

        public void Save(Route Route)
        {
            if (Route.Id == 0) Create(Route);
            else Update(Route);

            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }

        public void DeleteRoute(Route Route)
        {
            Delete(Route);
            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }
    }
}
