using MiniBlog.Core.Domain.Blogs.Events;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace MiniBlog.Core.Domain.Blogs.Entities
{
    public class Blog:AggregateRoot
    {
        #region Properties
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        #endregion

        #region Constructors
        private Blog()
        {

        }
        public Blog(Title title, Description description)
        {
            Title = title;
            Description = description;
            AddEvent(new BlogCreated(BusinessId.Value.ToString(), Title.Value, Description.Value));
        }
        #endregion

        #region Commands
        public static Blog Create(Title title, Description description) => new(title, description); 
        #endregion
    }
}
