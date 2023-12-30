﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectWPF.Pets;

namespace ProjectWPF.Adoptees
{
    internal class Adoptee
    {
        private string _id;
        private string _name;
        private string _email;
        private string _address;
        private long _phoneNumber;
        private int _residentsInHome;
        private int _petsInHome;
        private List<Pet> _adoptedPets;
        public Adoptee()
        {
            _name = "N/A";
            _email = "N/A";
            _address = "N/A";
            _phoneNumber = 0;
            _residentsInHome = 0;
            _petsInHome = 0;
            _adoptedPets = new List<Pet> { };
        }


        public Adoptee(string name, string email, string address, long phone, int residents, int pets)
        {
            _name = name;
            _email = email;
            _address = address;
            _phoneNumber = phone;
            _residentsInHome = residents;
            _petsInHome = pets;
            _adoptedPets = new List<Pet> { };
            _id = GetId();
        }
        public string Name
        {
            get { return _name; }
        }
        public List<Pet> AdoptedPets
        {
            get { return _adoptedPets; }
        }
        public string ID
        {
            get { return _id; }
        }
        public void AddPetToAdoptee(Pet petToAdd)
        {
            _adoptedPets.Add(petToAdd);
        }
        private string GetId()
        {
            string phoneNumberSubString = (_phoneNumber.ToString()).Substring(0, 3);
            string id = $"{Name}{phoneNumberSubString}";
            return id;
        }

    }
}
