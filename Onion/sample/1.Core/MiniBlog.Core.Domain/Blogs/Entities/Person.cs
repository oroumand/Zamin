using Zamin.Core.Domain.Entities;

namespace MiniBlog.Core.Domain.Blogs.Entities
{
	public class Person : AggregateRoot<long>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
