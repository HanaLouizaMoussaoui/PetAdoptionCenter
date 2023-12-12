using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Turtle : Pet
    {
        #region Fields
        private string[] FAVORITE_FOODS = { "Watermelon", "Lettuce", "Kiwi" };
        private string _favoriteFood;
        #endregion
        #region Constructors
        public Turtle(string name, int age, bool isAdopted) : base(name, age, isAdopted)
        {
            FavoriteFood = RandomTraitPicker(FAVORITE_FOODS);
        }
        #endregion
        #region Properties
        public string FavoriteFood
        {
            get { return _favoriteFood; }
            set { _favoriteFood = value; }
        }
        #endregion
    }
}
