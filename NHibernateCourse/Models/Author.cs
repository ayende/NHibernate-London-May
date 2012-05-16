using System.Collections.Generic;

namespace NHibernateCourse.Models
{
	public class Author
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual ICollection<Book> Books { get; set; }

		public virtual IDictionary<string, string> Attributes { get; set; }
		public virtual IDictionary<Book, int> Rating { get; set; }

		public Author()
		{
			Attributes = new Dictionary<string, string>();
			Rating = new Dictionary<Book, int>();
			Books = new List<Book>();
		}
	}
}