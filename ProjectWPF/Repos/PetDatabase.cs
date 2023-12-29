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
               new Pet("Kelp", 10, false, "Sea Turtle", "An elegant sea turtle"),
               new Pet("Charlotte", 1, false, "Dog", "A playful puppy"),
               new Pet("Carrot", 7, false, "Dog", "A cute and endearing puppy"),
               new Pet("Speedy", 1, false, "Hamster", "A very fast and active hamster"),
               new Pet("Keywe", 2, false, "Bird", "A kiwi bird that is guaranteed to sweeten your day"),
               new Pet("Plumpo", 4, false, "Bird", "A cockatiel with minimal thoughts")

        };
        public static Pet[] GetPetsInDatabase()
        {
            return petsInDatabase;
        }
    }
}
