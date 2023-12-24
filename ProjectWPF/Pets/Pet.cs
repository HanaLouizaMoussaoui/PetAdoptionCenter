using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.Pets
{
    internal class Pet
    {
        #region Fields
        private const string DEFAULT_DESCRIPTION = "No further information about this pet.";
        private string _type;
        private string _name;
        private string _description;
        private int _age;
        private bool _isAdopted;
        #endregion
        #region Constructors
        public Pet(string name, int age, bool isAdopted, string type)
        {
            Name = name;
            Age = age;
            IsAdopted = isAdopted;
            Description = DEFAULT_DESCRIPTION;
            _type = type;
        }
        public Pet(string name, int age, bool isAdopted, string type, string description) : this (name,age,isAdopted,type)
        {
            Description= description;
        }
        #endregion
        #region Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
            }
        }
        public bool IsAdopted
        {
            get { return _isAdopted; }
            set
            {
                _isAdopted = value;
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }
        #endregion
        #region Methods
        public void AdoptPet()
        {
            if (!IsAdopted)
            {
                IsAdopted = true;
            }
            
        }
        protected string RandomTraitPicker(string[] traits)
        {
            Random randomizer = new Random();
            int randomIndex = randomizer.Next(0, traits.Length - 1);
            return traits[randomIndex];
        }
        #endregion
    }
}
