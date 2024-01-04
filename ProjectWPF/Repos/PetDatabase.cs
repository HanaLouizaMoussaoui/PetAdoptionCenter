using ProjectWPF.Pets;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ProjectWPF.Repos
{
    internal static class PetDatabase
    {
        //private static Pet[] petsInDatabase;
        private const int NUMBER_OF_PETS_IN_DB = 12;
        public static Pet[] PetsInDatabase
        {
            get
            {
                Pet[] petsInDB = new Pet[NUMBER_OF_PETS_IN_DB];
                int arrayCounter = 0;
                string filePath = ".\\pet_information.txt";

                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine();
                            petsInDB[arrayCounter] = GetPetFromTextLine(line);
                            arrayCounter++;
                        }
                    }
                }
                return petsInDB;
            }

        }

        private static Pet GetPetFromTextLine(string line)
        {
            string[] seperatedPetInfo = line.Split(',');
            try
            {
                string name = seperatedPetInfo[0];
                int age = int.Parse(seperatedPetInfo[1]);
                bool isAdopted = bool.Parse(seperatedPetInfo[2]);
                string type = seperatedPetInfo[3];
                string description = seperatedPetInfo[4];
                Pet newPet = new Pet(name, age, isAdopted, type, description);
                return newPet;
            }
            catch
            {
                throw new Exception($"{seperatedPetInfo[0]},{seperatedPetInfo[1]},{seperatedPetInfo[2]}");
            }
        }
    }
}
