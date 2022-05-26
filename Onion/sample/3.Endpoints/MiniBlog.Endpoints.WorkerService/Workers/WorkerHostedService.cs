using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Endpoints.WorkerService.Workers;

public class WorkerHostedService : IHostedService
{
    private readonly ILogger<WorkerHostedService> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public WorkerHostedService(ILogger<WorkerHostedService> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Job(cancellationToken);
        }
    }

    private async Task Job(CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.Now;

        _logger.LogInformation("WorkerHostedService Running at: {time}", now);

        var result = await _commandDispatcher.Send(new CreateBlogCommand()
        {
            BusunessId = Guid.NewGuid(),
            Title = $"Title-{now}-sed arcu non odio euismod lacinia at quis risus sed",
            Description = $"Description-{now}-sed sed risus pretium quam vulputate dignissim suspendisse in est ante in nibh mauris cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo elit at imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis urna id volutpat lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis aenean",
        });

        await Task.Delay(10000, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}