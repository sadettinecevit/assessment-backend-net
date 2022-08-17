using WorkerProcess.ReportService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<RabbitMqService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
