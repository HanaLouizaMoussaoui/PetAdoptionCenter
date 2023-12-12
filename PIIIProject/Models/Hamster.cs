using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Hamster : Pet
    {
        #region Fields
        private string[] FAVORITE_TOYS = { "Exercise wheel", "Chews", "Tubes" };
        private string _favoriteToy;
        #endregion
        #region Constructors
        public Hamster(string name, int age, bool isAdopted) : base(name, age, isAdopted)
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
