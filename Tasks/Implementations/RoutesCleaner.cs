
using RM_API.Repositories.Implementations;
using RM_API.Repositories.Interfaces;
using RM_API.Tasks.Interfaces;

namespace RM_API.Tasks.Implementations
{
    public class RoutesCleaner : IRoutesCleaner
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RoutesCleaner(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Clean()
        {
            int total = 0;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var routeRepository = scope.ServiceProvider.GetRequiredService<IRouteRepository>();

                var toClean = new List<RouteRepository>();

                var routes = routeRepository.GetAllRoutes().ToList();

                foreach (var route in routes)
                {
                    if (route.DrivingDate < DateTime.Now)
                    {
                        routeRepository.DeleteRoute(route);
                        total++;
                    }
                }

                Console.WriteLine("----------------------");
                Console.WriteLine("------ " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ------");
                Console.WriteLine("----------------------");
                Console.WriteLine("- Deleted routes: " + total + " -");
                Console.WriteLine("----------------------");
            }
        }
    }
}
