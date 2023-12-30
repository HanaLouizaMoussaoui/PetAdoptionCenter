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
        private static List<Adoptee> adopteesInDatabase;

        private static List<Adoptee> RetrieveAdopteeDatabase(Pet adoptedPet,string adopteeInfo)
        {
            List<Adoptee> adopteesInDB = new List<Adoptee>();
            StringBuilder sb = new StringBuilder();
            string filePath = "..\\..\\..\\AdopterInfo\\adoptee_information.txt";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        adopteesInDB.Add(GetAdopteeFromTextLine(line,adoptedPet,adopteeInfo));
                    }
                }

            }
            else
            { throw new Exception("womp womp"); };
            return adopteesInDatabase;
        }
        private static Adoptee GetAdopteeFromTextLine(string line, Pet adoptedPet, string adopteeInfo)
        {
            string[] seperatedNewAdopteeInfo = adopteeInfo.Split(',');
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
                if (seperatedNewAdopteeInfo[0] == name)
                {
                    newAdoptee.AddPetToAdoptee(adoptedPet);
                }
                return newAdoptee;
            }
            catch
            {
                throw new Exception($"{seperatedAdopteeInfo[0]},{seperatedAdopteeInfo[1]},{seperatedAdopteeInfo[2]}");
            }
        }
        public static List<Adoptee> GetAdopteesInDatabase(Pet adoptedPet,string adopteeInfo)
        {
            adopteesInDatabase = RetrieveAdopteeDatabase(adoptedPet,adopteeInfo);
            return adopteesInDatabase;
        }
        public static List<Adoptee> FetchAdoptees()
        {
            return adopteesInDatabase;
        }
    }
}
