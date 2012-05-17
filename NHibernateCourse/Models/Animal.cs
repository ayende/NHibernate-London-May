namespace NHibernateCourse.Models
{
    public class Animal
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
        
    }

    public class GermanSheppard : Dog
    {
        
    }

    public class Bulldog : Dog
    {
        
    }
}