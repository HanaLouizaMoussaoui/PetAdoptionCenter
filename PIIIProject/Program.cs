using PIIIProject.Models;

namespace PIIIProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hamster hamster = new Hamster("Chichi", 3, true);
            Console.WriteLine(hamster.FavoriteToy);
            Dog dog = new Dog("Bella", 10, true);
            Console.WriteLine(dog.FavoriteGame);
            Turtle turtle= new Turtle("Bob",5,true);
            Console.WriteLine(turtle.FavoriteFood);
        }
    }
}