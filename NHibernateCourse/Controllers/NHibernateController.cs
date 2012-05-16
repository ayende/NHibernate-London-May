using System.Web.Mvc;
using NHibernate;

namespace NHibernateCourse.Controllers
{
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