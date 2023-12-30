using ProjectWPF.Adoptees;
using ProjectWPF.Pets;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectWPF.Repos
{
    internal static class AdopteeDatabase
    {
        // This will be used to keep track of adoptees that already exist
        private static List<Adoptee> adopteesInDatabase = new List<Adoptee> { };
        public static List<Adoptee> RetrieveAdopteeDatabase()
        {
            // List that will hold adoptee objects
            List<Adoptee> adopteesInDB = new List<Adoptee> { };

            // Path to our adoptee file
            string filePath = "..\\..\\..\\AdopterInfo\\adoptee_information.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    Adoptee newAdoptee = new Adoptee();
                    string line;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        newAdoptee = GetAdopteeFromTextLine(line);
                        Pet adoptedPet = GetPetFromTextLine(line);
                        if (!CheckAdopteeAlreadyExist(newAdoptee, adoptedPet)) // Checking that the adoptee doesn't already exist in our list of adoptees
                        {
                            newAdoptee.AddPetToAdoptee(adoptedPet); // Assigning the adopted pet to this new adoptee
                            adopteesInDB.Add(newAdoptee); // Adding the adoptee to the list

                            // This copies the contents of adopteesInDb to adopteesInDatabase without them having the same pointer
                            adopteesInDatabase = adopteesInDB.ToList();
                        }
                    }
                    // Clearing the list so we can re-use it properly next time we fetch the database
                    adopteesInDatabase.Clear();
                    return adopteesInDB;
                }

            }
            else
            {
                throw new Exception("The adoptee database file couldn't be found");
            }
            
        }


        private static Adoptee GetAdopteeFromTextLine(string line)
        {
            string[] seperatedAdopteeInfo = line.Split(',');
            string name = seperatedAdopteeInfo[0];
            string email = seperatedAdopteeInfo[1];
            string address = seperatedAdopteeInfo[2];
            long phoneNumber = long.Parse(seperatedAdopteeInfo[3]);
            int residents = 1; // TEMP
            int pets = 0; // TEMP 
            Adoptee newAdoptee = new Adoptee(name, email, address, phoneNumber, residents, pets);
            return newAdoptee;

        }
        private static Pet GetPetFromTextLine(string line)
        {
            string[] seperatedAdopteeInfo = line.Split(',');

            string petName = seperatedAdopteeInfo[7];
            foreach (Pet pet in PetDatabase.GetPetsInDatabase())
            {
                if (pet.Name == petName) return pet; // Finding the pet  in the database with this name
            }
            return null;

        }
        private static bool CheckAdopteeAlreadyExist(Adoptee newAdoptee, Pet adoptedPet)
        {
            bool alreadyExists = false;
            string id = newAdoptee.ID;
            // Using adopteesInDatabase to check which adoptees already exist
            foreach (Adoptee adoptee in adopteesInDatabase)
            {
                // Using IDs to check for duplicate adoptees
                if (adoptee.ID == id)
                {
                    alreadyExists = true;
                    // If the adoptee already exists we just add their new pet
                    adoptee.AddPetToAdoptee(adoptedPet);
                }
            }
            return alreadyExists;
        }
    }

}
