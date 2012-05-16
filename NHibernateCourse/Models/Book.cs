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

		public Book()
		{
			Authors = new List<Author>();
		}

	}
}