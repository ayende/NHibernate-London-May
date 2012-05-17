﻿using System.Web.Mvc;
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

        public ActionResult Book(int id)
        {
            var author = Session.Get<Author>(id);
            var book = new Book
            {
                Title = "a",
                ISBN = "1234",
                NumberOfPages = 23
            };
            author.Books.Add(book);
            book.Authors.Add(author);
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

            return Json(data.Authors.Select(x => x == null ? null : x.Name).ToArray());
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
            var dog = new Dog { Name = name, Sex = sex };
            Session.Save(dog);
            return Json(dog);
        }

        public ActionResult SaveCat(string name, string sex)
        {
            var cat = new Cat { Name = name, Sex = sex };
            Session.Save(cat);
            return Json(cat);
        }

        public ActionResult FindDog(string name)
        {
            var dog = Session.Query<Dog>().Single(d => d.Name == name);
            return Json(dog);
        }

        public ActionResult FindCat(string name)
        {
            var cat = Session.Query<Cat>().Single(c => c.Name == name);
            return Json(cat);
        }

        public ActionResult FindAllDogs()
        {
            var d = Session.Query<Dog>().ToList();
            return Json(d);
        }

        public ActionResult FindAllCats()
        {
            var d = Session.Query<Cat>().ToList();
            return Json(d);
        }

		public ActionResult LoadDog(long id)
		{
			return Json(Session.Get<Dog>(id));
		}
    }
}