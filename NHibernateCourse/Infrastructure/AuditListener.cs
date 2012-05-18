using System;
using System.Text;
using NHibernate;
using NHibernate.Event;
using NHibernateCourse.Models;

namespace NHibernateCourse.Infrastructure
{
	public class AuditListener : IPreUpdateEventListener
	{
		public bool OnPreUpdate(PreUpdateEvent @event)
		{
			if (@event.Entity is Log)
				throw new InvalidOperationException("You cannot modify logs, they are immutable!");

			var sb = new StringBuilder("Updating " + @event.Persister.EntityName + " " + @event.Id)
				.AppendLine();
			for (int i = 0; i < @event.OldState.Length; i++)
			{
				if(@event.Persister.PropertyTypes[i]
					.IsEqual(@event.OldState[i], @event.State[i], @event.Session.EntityMode))
					continue;

				sb.Append("\t")
					.Append(@event.Persister.PropertyNames[i])
					.Append(": ")
					.Append(@event.OldState[i])
					.Append(" -> ")
					.Append(@event.State[i])
					.AppendLine();
			}

			using(var child = @event.Session.GetSession(EntityMode.Poco))
			{
				child.Save(new Log
				{
					EntityId = (int) @event.Id,
					Entity = @event.Persister.EntityName,
					Changes = sb.ToString()
				});
				child.Flush();
			}

			return false;
		}
	}
}