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
                string filePath = ".\\PetDatabaseTextFile.txt";

                if (!File.Exists(filePath))
                {
                    // Create the file if it doesn't exist
                    using (File.Create(filePath)) { AddStartingContent(filePath); }
                }

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
        private static void AddStartingContent(string filePath)
        {
            string startingContent = "Coco,4,true,Dog,A cute white and grey dog\r\nBuddy,7,false,Dog,A friendly brown dog\r\nChichi,1,true,Hamster,A speedy little hamster\r\nSlowy,15,false,Turtle,A majestic turtle\r\nBueno,2,true,Hamster,A cute hamster that loves treats\r\nKelp,10,false,Sea Turtle,An elegant sea turtle\r\nCharlotte,1,false,Dog,A playful puppy\r\nCarrot,7,false,Dog,A cute and endearing puppy\r\nSpeedy,1,false,Hamster,A very fast and active hamster\r\nKeywe,2,false,Bird,A kiwi bird that is guaranteed to sweeten your day\r\nPlumpo,4,true,Bird,A cockatiel with minimal thoughts\r\nSmokey,289,true,Dragon,A fiery friend who will protect you for life";
            // Write starting content to the file
            File.WriteAllText(filePath, startingContent);
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
