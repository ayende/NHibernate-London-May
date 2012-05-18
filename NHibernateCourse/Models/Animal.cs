namespace NHibernateCourse.Models
{
    public class Animal
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Sex { get; set; }
		public virtual AnimalProperties Properties { get; set; }
    }

	public class Cat : AnimalProperties
    {
		public virtual bool Scratching { get; set; }
    }

	public class AnimalProperties
	{
		public virtual int Id { get; set; }
	}

	public class Dog : AnimalProperties
    {
		public virtual bool Bark { get; set; }
    }

	public class GermanSheppard : Dog
	{
		public virtual bool Cute { get; set; }
    }

	public class Bulldog : AnimalProperties
    {
		public virtual bool Bite { get; set; }
    }
}