using Kulba.Services.CayenneService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kulba.Services.CayenneService.Services
{
    public class GreetingHostedService : HostedService
    {
        private readonly ILogger<GreetingHostedService> _logger;
        private readonly SocketServerConfigInfo _socketServerConfigInfo;

        public GreetingHostedService(ILogger<GreetingHostedService> logger, IOptions<SocketServerConfigInfo> socketServerConfigInfo)
        {
            _logger = logger;
            _socketServerConfigInfo = socketServerConfigInfo?.Value ?? throw new ArgumentNullException(nameof(socketServerConfigInfo));
            _logger.LogInformation("SocketServerConfigInfo: " + _socketServerConfigInfo.ToString());
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var startTime = DateTimeOffset.Now;
            _logger.ProgramStarting(startTime, 42);
            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            var stopTime = DateTimeOffset.Now;
            _logger.ProgramStopping(stopTime);
            await base.StopAsync(cancellationToken);

        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Greeting Service Says Hello Joe at: {time}", DateTime.Now);
                await Task.Delay(1000, cancellationToken);
            }
        }

    }
}
