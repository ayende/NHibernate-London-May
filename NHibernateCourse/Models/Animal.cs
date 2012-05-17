namespace NHibernateCourse.Models
{
    public abstract class Animal
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Sex { get; set; }      
    }

    public class Cat : Animal
    {
        
    }

    public class Dog : Animal
    {
		public virtual bool Bark { get; set; }
        
    }

    public class GermanSheppard : Dog
    {
        
    }

    public class Bulldog : Dog
    {
        
    }
}