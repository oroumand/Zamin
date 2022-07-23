namespace MiniBlog.Core.Contracts.Blogs.Queries.GetBlogByBusinessId;

public class BlogQr
{
    public long Id { get; set; }
    public Guid BusinessId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
