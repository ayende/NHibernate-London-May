using System.Web.Mvc;
using NHibernate;
using NHibernateCourse.Models;
using NHibernate.Linq;
using System.Linq;

namespace NHibernateCourse.Controllers
{
	public class HomeController : NHibernateController
	{
		public ActionResult Author(string name)
		{
			var id = Session.Save(new Author
			{
				Name = name
			});
			return Json(new { id });
		}

		public ActionResult Book(int id)
		{
			var bookId = Session.Save(new Book
			{
				Authors = { Session.Load<Author>(id) },
				Title = "a",
				ISBN = "1234",
				NumberOfPages = 23
			});
			return Json(new { bookId });
		}

		public ActionResult Plagarize(int id, int authorId)
		{
			var author = Session.Load<Author>(authorId);

			 Session.Get<Book>(id).Authors.Add(author);

			return Json(new
			{
			});
		}

		public ActionResult Load(int id)
		{
			var data = Session.Get<Book>(id);

			return Json(data.Authors.Select(x => x.Name).ToArray());
		}

		public ActionResult BooksBy(int id)
		{
			var author = Session.Get<Author>(id);

			var books = Session.Query<Book>()
				.Where(x => x.Authors.Any(a => a == author))
				.ToList();

			return Json(new
			{
				author,
				titles = books.Select(x=>x.Title).ToArray()
			});
		}
	}
}