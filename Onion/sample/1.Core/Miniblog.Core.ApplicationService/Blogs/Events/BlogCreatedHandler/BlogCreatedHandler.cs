using Microsoft.Extensions.Logging;
using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Core.Domain.Blogs.Events;
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
        try
        {
            Person person = new Person
            {
                FirstName = DateTime.Now.ToString(),
                LastName = DateTime.Now.ToLongTimeString(),
            };
            await _personCommandRepository.InsertAsync(person);
            await _personCommandRepository.CommitAsync();

            _logger.LogInformation("Handeled {Event} in BlogCreatedHandler", Event.GetType().Name);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
