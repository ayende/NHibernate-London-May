using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace NHibernateCourse.Models
{
	public class Book
	{
		public virtual int Id { get; set; }
		public virtual string ISBN { get; set; }
		public virtual string Title { get; set; }
		public virtual ICollection<Author> Authors { get; set; }
		public virtual int NumberOfPages { get; set; }
		public virtual IList<string> Tags { get; set; }

		public Book()
		{
			Authors = new List<Author>();
		}

		//public virtual string PublisherName { get; set; }
		//public virtual bool PublisherInGoodStanding { get; set; }
		//public virtual bool PublisherStatus { get; set; }

		public virtual Publisher Publisher { get; set; }
	}

	public class Publisher
	{

		public virtual string Name { get; set; }
		public virtual bool InGoodStanding { get; set; }
		public virtual bool Status { get; set; }
	}
}