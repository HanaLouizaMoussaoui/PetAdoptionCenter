﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWPF.Pets;

namespace ProjectWPF.Adoptees
{
    internal class Adoptee
    {
        private string _name;
        private string _email;
        private string _address;
        private long _phoneNumber;
        private int _residentsInHome;
        private int _petsInHome;
        private List<Pet> _adoptedPets;
        public Adoptee(string name, string email, string country, string address, long phoneNumber, int residents, int pets)
        {
            _name = name;
            _email = email;
            _address = address;
            _phoneNumber = phoneNumber;
            _residentsInHome = residents;
            _petsInHome = pets;
        }
        public void AddPetToAdoptee(Pet petToAdd)
        {
            _adoptedPets.Add(petToAdd);
        }

    }
}