using MiniBlog.Core.Domain.Blogs.Events;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace MiniBlog.Core.Domain.Blogs.Entities;

public class Blog : AggregateRoot<int>
{
    #region Properties
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public IReadOnlyList<BlogPost> Posts => _posts.ToList();
    private List<BlogPost> _posts = new();
    #endregion

    #region Constructors
    private Blog()
    {

    }
    public Blog(Title title, Description description)
    {
        Title = title;
        Description = description;

        AddEvent(new BlogCreated(BusinessId.Value, Title.Value, Description.Value));
    }
    #endregion

    #region Commands
    public static Blog Create(Title title, Description description) => new(title, description);

    public void Update(Title title, Description description)
    {
        Title = title;
        Description = description;

        AddEvent(new BlogUpdated(BusinessId.Value, Title.Value, Description.Value));
    }
    public void Delete()
    {
        if (_posts.Any())
            throw new InvalidEntityStateException("این بلاگ دارای پست می باشد");

        AddEvent(new BlogDeleted(BusinessId.Value));
    }

    public void DeleteGraph()
    {
        AddEvent(new BlogDeleted(BusinessId.Value));

        _posts.ForEach((post) =>
        {
            AddEvent(new BlogPostDeleted(post.BusinessId.Value, BusinessId.Value));
        });
    }

    public void AddPost(Title title)
    {
        if (_posts.Any(p => p.Title.Equals(title)))
            throw new InvalidEntityStateException("این پست قبلا ثبت شده است");

        var post = BlogPost.Create(title);
        _posts.Add(post);

        AddEvent(new BlogPostCreated(post.BusinessId.Value, BusinessId.Value, post.Title.Value));
    }

    public void RemovePost(long id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post is null)
            throw new InvalidEntityStateException("این پست وجود ندارد");

        _posts.Remove(post);

        AddEvent(new BlogPostDeleted(post.BusinessId.Value, BusinessId.Value));
    }
    #endregion
}
