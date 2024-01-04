using ProjectWPF.Adopters;
using ProjectWPF.Pets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProjectWPF.Repos
{
    /// <summary>
    /// This class manages the file saving/retrieving of the adopters and links them with their pets
    /// </summary>
    internal static class AdopterDatabase
    {
        // This will be used to keep track of adopters that already exist
        private static List<Adopter> adoptersInDatabase = new List<Adopter> { };
        public static List<Adopter> RetrieveAdopterDatabase()
        {
            // List that will hold adopter objects
            List<Adopter> adoptersInDB = new List<Adopter> { };

            // Path to our adoptee file
            string filePath = ".\\adopter_information.txt";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    Adopter newAdopter = new Adopter();
                    string line;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        newAdopter = GetAdopterFromTextLine(line);
                        Pet adoptedPet = GetPetFromTextLine(line);
                        if (!CheckAdopterAlreadyExist(newAdopter, adoptedPet)) // Checking that the adopter doesn't already exist in our list of adoptees
                        {
                            newAdopter.AddPetToAdopter(adoptedPet); // Assigning the adopted pet to this new adopter
                            adoptersInDB.Add(newAdopter); // Adding the adopter to the list

                            // This copies the contents of adoptersInDb to adoptersInDatabase without them having the same pointer
                            adoptersInDatabase = adoptersInDB.ToList();
                        }
                    }
                    // Clearing the list so we can re-use it properly next time we fetch the database
                    adoptersInDatabase.Clear();
                    return adoptersInDB;
                }
            }
            else
                throw new Exception("The adopter database file couldn't be found");
        }

        /// <summary>
        /// Splits the line by the commas and creates adopter objects with the information.
        /// </summary>
        /// <param name="line">the string of data from the file</param>
        /// <returns>a new Adopter object</returns>
        private static Adopter GetAdopterFromTextLine(string line)
        {
            string[] seperatedAdopterInfo = line.Split(',');
            string name = seperatedAdopterInfo[0];
            string address = seperatedAdopterInfo[1];
            string email = seperatedAdopterInfo[2];
            string phoneNumber = seperatedAdopterInfo[3];
            string home = seperatedAdopterInfo[4];
            string residents = seperatedAdopterInfo[5];
            string pets = seperatedAdopterInfo[6];
            Adopter newAdopter = new Adopter(name, address, email, phoneNumber, home, residents, pets);
            return newAdopter;
        }

        /// <summary>
        /// Splits the line by the comma and retrieves the pet object based on the pet's name in the file.
        /// </summary>
        /// <param name="line">string of data from the file</param>
        /// <returns>a Pet object</returns>
        private static Pet GetPetFromTextLine(string line)
        {
            string[] seperatedAdopterInfo = line.Split(',');

            string petName = seperatedAdopterInfo[7];
            foreach (Pet pet in PetDatabase.PetsInDatabase)
            {
                if (pet.Name == petName) return pet; // Finding the pet  in the database with this name
            }
            return null;

        }

        /// <summary>
        /// Checks if an adopter already exists, and if so it will not create a new adopter object.
        /// </summary>
        /// <param name="newAdopter">A new adopter that might exist</param>
        /// <param name="adoptedPet">The adopted pet to be linked to the adopter</param>
        /// <returns></returns>
        private static bool CheckAdopterAlreadyExist(Adopter newAdopter, Pet adoptedPet)
        {
            bool alreadyExists = false;
            string id = newAdopter.ID;
            // Using adopteesInDatabase to check which adoptees already exist
            foreach (Adopter adopter in adoptersInDatabase)
            {
                // Using IDs to check for duplicate adoptees
                if (adopter.ID == id)
                {
                    alreadyExists = true;
                    // If the adoptee already exists we just add their new pet
                    adopter.AddPetToAdopter(adoptedPet);
                }
            }
            return alreadyExists;
        }
    }

}
