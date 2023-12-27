using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Bird : Pet
    {
        #region Fields
        private string[] FAVORITE_FOODS = { "Sunflower Seeds", "Corn", "Nuts", "Blueberry", "Mealworm"};
        private string _favoriteFood;
        #endregion
        #region Constructors
        public Bird(string name, int age, bool isAdopted) : base(name, age, isAdopted)
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
