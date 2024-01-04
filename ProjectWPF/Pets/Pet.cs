using System;
using System.Windows.Input;
using System.Windows.Media;

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
        public Pet()
        {
            Name = "N/A";
            Age = 0;
            IsAdopted = false;
            Description = DEFAULT_DESCRIPTION;
            Type = "N/A";

        }
        public Pet(string name, int age, bool isAdopted, string type)
        {
            Name = name;
            Age = age;
            IsAdopted = isAdopted;
            Description = DEFAULT_DESCRIPTION;
            Type = type;
        }
        public Pet(string name, int age, bool isAdopted, string type, string description) : this(name, age, isAdopted, type)
        {
            Description = description;
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
                if (value < 0)
                {
                    _age = 0;
                }
                _age = value;
            }
        }
        public bool IsAdopted
        {
            get { return _isAdopted; }
            set { _isAdopted = value; }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// Calculated property for the photo source
        /// </summary>
        public Uri PhotoSource
        {
            get { return new Uri($"/Images/{Name}.png", UriKind.Relative); }
        }
        #endregion
    }
}
