using ProjectWPF.Pets;
using System;
using System.Collections.Generic;

namespace ProjectWPF.Adopters
{
    /// <summary>
    /// This class contains the members and methods of an Adopter. It will validate the Adopter's information and is called when updating the adopter database.
    /// </summary>
    internal class Adopter
    {
        #region Fields
        private string _name;
        private string _email;
        private string _address;
        private string _phoneNumber;
        private string _homeType;
        private string _residentsInHome;
        private string _petsInHome;
        private List<Pet> _adoptedPets;
        #endregion

        #region Constructors
        public Adopter()
        {
            _name = "N/A";
            _email = "N/A";
            _address = "N/A";
            _phoneNumber = "";
            _homeType = "";
            _residentsInHome = "";
            _petsInHome = "";
            _adoptedPets = new List<Pet> { };
        }
        public Adopter(string name, string email, string address, string phone, string residents, string pets)
        {
            _name = name;
            _email = email;
            _address = address;
            _phoneNumber = phone;
            _residentsInHome = residents;
            _petsInHome = pets;
            _adoptedPets = new List<Pet> { };
        }

        public Adopter(string name, string email, string address, string phone, string homeType, string residents, string pets)
        {
            _name = name;
            _email = email;
            _address = address;
            _phoneNumber = phone;
            _homeType = homeType;
            _residentsInHome = residents;
            _petsInHome = pets;
            _adoptedPets = new List<Pet> { };
        }
        #endregion

        #region Properties
        // The validation here is a bit redundant since it checks already on file submit, but it doesn't do much harm to leave it in in case of adopters
        // being made not on submission.
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Name cannot be null"); //TODO: figure out how to validate at form submission (aka, send an error to the user)
                _name = value;
            }
        }
        public List<Pet> AdoptedPets
        {
            get { return _adoptedPets; }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Email cannot be null");
                _email = value;
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Address cannot be null");
                _email = value;
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value.Length < 10)
                    throw new ArgumentException("Phone number needs to be 10 digits long.");
                _phoneNumber = value;
            }
        }
        public string Residents
        {
            get { return _residentsInHome; }
        }
        public string PetsInHome
        {
            get { return _petsInHome; }
        }
        public string HomeType
        {
            get { return _homeType; }
        }
        public string ID
        {
            get
            {
                string id = $"{Name}{PhoneNumber.Substring(0, 3)}";
                return id;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds an adopted pet to the property _adoptedPets of the Adopter object
        /// </summary>
        /// <param name="petToAdd">The pet to add to the Adopter</param>
        public void AddPetToAdopter(Pet petToAdd)
        {
            _adoptedPets.Add(petToAdd);
        }

        public override string ToString()
        {
            return $"{Name},{Address},{Email},{PhoneNumber},{HomeType},{Residents},{PetsInHome}";
        }
        #endregion
    }
}
