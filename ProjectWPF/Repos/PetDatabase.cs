using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWPF.Pets;

namespace ProjectWPF.Repos
{
    internal static class PetDatabase
    {
        private static Pet[] petsInDatabase =
        {
             
               new Pet("Coco", 4, false, "Dog", "A cute white and grey dog"),
               new Pet("Buddy", 7, false, "Dog", "A friendly brown dog"),
               new Pet("Chichi", 1, false, "Hamster", "A speedy little hamster"),
               new Pet("Slowy", 15, false, "Turtle", "A majestic turtle"),
               new Pet("Bueno", 2, false, "Hamster", "A cute hamster that loves treats"),
               new Pet("Lily", 10, false, "Turtle", "An elegant turtle"),
               new Pet("Charlotte", 1, false, "Dog", "A playful puppy"),
        };
        public static Pet[] GetPetsInDatabase()
        {
            return petsInDatabase;
        }
    }
}
