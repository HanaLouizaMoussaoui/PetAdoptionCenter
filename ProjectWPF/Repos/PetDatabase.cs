using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectWPF.Pets;

namespace ProjectWPF.Repos
{
    internal static class PetDatabase
    {
        private static Pet[] petsInDatabase;
        static PetDatabase()
        {
            petsInDatabase = RetrievePetDatabase();
        }
        private static Pet[] RetrievePetDatabase()
        {
            Pet[] petsInDB = new Pet[11];
            int arrayCounter = 0;
            StringBuilder sb = new StringBuilder();
            string filePath = "..\\..\\..\\PetDatabaseInfo\\PetDatabaseTextFile.txt";
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
        private static Pet GetPetFromTextLine(string line)
        {
            string[] seperatedPetInfo = line.Split(',');
            string name = seperatedPetInfo[0];
            int age = int.Parse(seperatedPetInfo[1]);
            bool isAdopted= bool.Parse(seperatedPetInfo[2]);
            string type = seperatedPetInfo[3];
            string description = seperatedPetInfo[4];
            Pet newPet = new Pet(name, age, isAdopted, type, description);
            return newPet;
        }
        public static Pet[] GetPetsInDatabase()
        {
            petsInDatabase = RetrievePetDatabase();
            return petsInDatabase;
        }
    }
}
