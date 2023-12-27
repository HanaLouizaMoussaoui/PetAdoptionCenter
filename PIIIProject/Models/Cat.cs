using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Cat: Pet
    {
        #region Fields
        private string[] FAVORITE_TOYS = { "Mouse", "Spring", "Ribbon" };
        private string _favoriteToy;
        #endregion
        #region Constructors
        public Cat(string name, int age, bool isAdopted) : base(name, age, isAdopted)
        {
            FavoriteToy = RandomTraitPicker(FAVORITE_TOYS);
        }
        #endregion
        #region Properties
        public string FavoriteToy
        {
            get { return _favoriteToy; }
            set { _favoriteToy = value; }
        }
        #endregion
    }
}
