using System;
using System.Collections.Generic;
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
                Name = name,
                Attributes =
				{
					{"Great", "True"},
					{"Nice", "Not"},
					{"Dead", "1923"}
				}
            });
            return Json(new { id });
        }

        public ActionResult Batch()
        {
            var book1 = Session.Load<Book>(1);
            var book2 = Session.Load<Book>(2);

            return Json(new
            {
                book1Authors = book1.Authors.Select(x => x.Name).ToList(),
                book2Authors = book2.Authors.Select(x => x.Name).ToList()
            });
        }

        public ActionResult Rate(int id, int bookId)
        {
            var author = Session.Get<Author>(id);
            var book = Session.Load<Book>(bookId);
            author.Rating[book] = 5;

            return Json(author.Rating.Select(x => x.Key + ": " + x.Value).ToArray());
        }
		public ActionResult Update(int id)
		{
			var book = Session.Get<Book>(id);
			book.ISBN = DateTime.Now.Ticks.ToString();
			book.NumberOfPages++;
			return Json(book.Title);

		}
        public ActionResult Book()
        {
            var book = new Book
            {
                Title = "a",
                ISBN = "1234",
                NumberOfPages = 23,
				Tags = new List<string>
				{
					"hello",
					"there",
					"good"
				}
            };
            var bookId = Session.Save(book);
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

            return Json(data);
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
                titles = books.Select(x => x.Title).ToArray()
            });
        }

        public ActionResult SaveDog(string name, string sex)
        {
        	var dog = new Animal
        	{
        		Name = name,
        		Sex = sex,
        		Properties = new GermanSheppard
        		{
        			Cute = true,
					Bark = true
        		}
        	};
            Session.Save(dog);
            return Json(dog);
        }

		public ActionResult FindAllAnimals()
		{
			var d = Session.Query<Animal>().ToList();
			return Json(d);
		}

		public ActionResult LoadAnimal(long id)
		{
			var animal = Session.Get<Animal>(id);
			animal.Name += "*";
			return Json(animal.Name);
		}
    }
}