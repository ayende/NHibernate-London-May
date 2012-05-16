using NHibernate;

namespace NHibernateCourse.Models
{
	public class Book
	{
		public virtual string ISBN { get; set; }
		public virtual string Title { get; set; }
		public virtual string Author { get; set; }
		public virtual int NumberOfPages { get; set; }
	}

}