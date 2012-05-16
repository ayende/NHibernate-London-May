using System.Web.Mvc;
using NHibernate;
using NHibernateCourse.Models;

namespace NHibernateCourse.Controllers
{
	public class HomeController : NHibernateController
	{
		public ActionResult Create()
		{
			var id = Session.Save(new Book
			{
				Author = "Ayende", Title = "Foo", ISBN = "1234"
			});
			return Json(new {id});
		} 

		public ActionResult Load(int id)
		{
			return Json(Session.Get<Book>(id));
		}
	}

	public class NHibernateController : Controller
	{
		public new ISession Session { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Session = MvcApplication.Factory.OpenSession();
			Session.BeginTransaction();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using(Session)
			{
				if (Session == null || filterContext.Exception != null)
					return;
				Session.Transaction.Commit();
			}
		}

		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}
	}
}