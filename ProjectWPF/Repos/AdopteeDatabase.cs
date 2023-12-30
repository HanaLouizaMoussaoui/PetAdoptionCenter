﻿using ProjectWPF.Adoptees;
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
        private static List<Adoptee> adopteesInDatabase = new List<Adoptee> { };

  
        private static List<Adoptee> RetrieveAdopteeDatabase()
        {
            List<Adoptee> adopteesInDB = new List<Adoptee> { };
            StringBuilder sb = new StringBuilder();
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
                        newAdoptee.AddPetToAdoptee(adoptedPet);
                        adopteesInDB.Add(newAdoptee);
                    }
                }
                adopteesInDatabase = adopteesInDB;
                return adopteesInDB;
            }
            else
            { throw new Exception("womp womp"); };
        }
        private static Adoptee GetAdopteeFromTextLine(string line)
        {
           
            string[] seperatedAdopteeInfo = line.Split(',');
            try
            {
                string name = seperatedAdopteeInfo[0];
                string email = seperatedAdopteeInfo[1];
                string address = seperatedAdopteeInfo[2];
                long phoneNumber = long.Parse(seperatedAdopteeInfo[3]);
                int residents = 1; // TEMP
                int pets = 0; // TEMP 
                Adoptee newAdoptee = new Adoptee(name, email, address, phoneNumber, residents,pets);
                return newAdoptee;
            }
            catch
            {
                throw new Exception();
            }
        }
        private static Pet GetPetFromTextLine(string line)
        {
            string[] seperatedAdopteeInfo = line.Split(',');
            try
            {
                string petName = seperatedAdopteeInfo[7];
                foreach (Pet pet in PetDatabase.GetPetsInDatabase())
                {
                    if (pet.Name==petName) return pet;
                }
                return null;
            }
            catch
            {
                throw new Exception();
            }
        }
        public static List<Adoptee> GetAdopteesInDatabase()
        {
            List<Adoptee> adopteesInDB = new List<Adoptee> { };

            adopteesInDB= RetrieveAdopteeDatabase();
 
            return adopteesInDB;
        }
        private static bool CheckAdopteeAlreadyExist(string adopteeInfo)
        {
            bool alreadyExists = false;
            string[] seperatedAdopteeInfo = adopteeInfo.Split(',');
            string name = seperatedAdopteeInfo[0];
            foreach (Adoptee adoptee in adopteesInDatabase)
            {
                if (adoptee.Name == name)
                {
                    alreadyExists = true;
                }
            }
            return alreadyExists;
        }
    }
}
