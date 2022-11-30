using Microsoft.Extensions.Logging;
using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Core.Domain.Blogs.Events;
using System.Diagnostics;
using Zamin.Core.Contracts.ApplicationServices.Events;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Events.BlogCreatedHandler;
public class BlogCreatedHandler : IDomainEventHandler<BlogCreated>
{
    private readonly ILogger<BlogCreatedHandler> _logger;
    private readonly IPersonCommandRepository _personCommandRepository;

    public BlogCreatedHandler(ILogger<BlogCreatedHandler> logger ,
                                IPersonCommandRepository personCommandRepository) 
    {
        _logger = logger;
        _personCommandRepository = personCommandRepository;
    }
    public async Task Handle(BlogCreated Event)
    {
        var activity = Activity.Current;

        Person person = new Person
        {
            FirstName = "Arash",
            LastName = "Azhdari",
        };
        await _personCommandRepository.InsertAsync(person);
        await _personCommandRepository.CommitAsync();

        _logger.LogInformation("Handeled {Event} in BlogCreatedHandler",Event.GetType().Name);
        await Task.CompletedTask;
    }
}
