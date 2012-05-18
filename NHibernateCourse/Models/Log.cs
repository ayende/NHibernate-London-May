namespace NHibernateCourse.Models
{
	public class Log
	{
		public virtual int Id { get; set; }
		public virtual int EntityId { get; set; }
		public virtual string Entity { get; set; }
		public virtual string Changes { get; set; }
	}
}