using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace MiniBlog.Core.Domain.Blogs.Entities;

public sealed class BlogPost : Entity<int>
{
    #region Properties
    public Title Title { get; private set; }
    public int BlogId { get; private set; }
    #endregion

    #region Constructors
    private BlogPost()
    {
    }

    public BlogPost(Title title)
    {
        Title = title;
    }
    #endregion

    #region Commands
    public static BlogPost Create(Title title) => new(title);
    public void Update(Title title) => Title = title;
    #endregion
}
