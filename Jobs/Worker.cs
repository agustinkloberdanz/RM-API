using RM_API.Tasks.Interfaces;

namespace RM_API.Jobs
{
    public class Worker : IHostedService, IDisposable
    {
        private Timer _timer;
        
        private readonly IRoutesCleaner _routesCleaner;
        public Worker(IRoutesCleaner routesCleaner)
        {
            this._routesCleaner = routesCleaner;
        }

        private void DoWork(object state)
        {
            _routesCleaner.Clean();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
