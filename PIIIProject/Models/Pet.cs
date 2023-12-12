using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Pet
    {
        #region Fields
        private const string DEFAULT_DESCRIPTION = "No further information about this pet.";
        private string _name;
        private string _description;
        private int _age;
        private bool _isAdopted;
        #endregion
        #region Constructors
        public Pet(string name, int age, bool isAdopted)
        {
            Name= name;
            Age = age;
            IsAdopted= isAdopted;
            Description = DEFAULT_DESCRIPTION;
        }
        public Pet(string name, int age, bool isAdopted, string description) : this (name,age,isAdopted)
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
        #endregion
        public void AdoptPet()
        {
            if (!IsAdopted)
            {
                IsAdopted = true;
            }
            
        }
    }
}
