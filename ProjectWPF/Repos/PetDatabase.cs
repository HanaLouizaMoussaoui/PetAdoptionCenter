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
               new Pet("NOTAPET",0,false,"nothing","nothing"), // this is my temporary solution to the random generator never choosing 0
               new Pet("Coco", 2, false, "Dog", "A cute white dog"),
               new Pet("Buddy", 5, false, "Dog", "A friendly brown dog"),
               new Pet("Whiskers", 1, false, "Cat", "A playful tabby cat"),
               new Pet("Fluffy", 3, false, "Cat", "A fluffy Persian cat"),
               new Pet("Speedy", 1, false, "Hamster", "A speedy little hamster"),
               new Pet("Slowy", 100, false, "Turtle", "A majestic turtle")
        };
        public static Pet[] GetPetsInDatabase()
        {
            return petsInDatabase;
        }
    }
}
