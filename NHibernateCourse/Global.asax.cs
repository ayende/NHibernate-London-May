using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using Environment = System.Environment;

namespace NHibernateCourse
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		private static ISessionFactory _factory;
		public static ISessionFactory Factory
		{
			get
			{
				if(_factory != null)
					return _factory;
				lock(typeof(MvcApplication))
				{
					if (_factory != null)
						return _factory;
					return _factory = CreateSessionFactory();
				}
			}
		}

		private static ISessionFactory CreateSessionFactory()
		{
			var cfg = new Configuration()
				.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlAuto, "update")
				.SetProperty(NHibernate.Cfg.Environment.DefaultBatchFetchSize, "10")
				.DataBaseIntegration(db =>
				{
					db.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=nh;Integrated Security=SSPI";
					db.Dialect<MsSql2008Dialect>();
				})
				.AddAssembly(Assembly.GetExecutingAssembly());

			return cfg.BuildSessionFactory();
		}
	}
}