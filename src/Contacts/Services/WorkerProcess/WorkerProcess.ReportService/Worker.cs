namespace WorkerProcess.ReportService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMqService _rabbitMqService;

        public Worker(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _rabbitMqService.Consume();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}