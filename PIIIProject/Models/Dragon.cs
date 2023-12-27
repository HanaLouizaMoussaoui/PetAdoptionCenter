using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Dragon : Pet
    {
        #region Fields
        private string[] FAVORITE_ACTIVITIES = { "Breathe Fire", "Flying", "BBQing" };
        private string _favoriteGame;
        #endregion
        #region Constructors
        public Dragon(string name, int age, bool isAdopted) : base(name, age, isAdopted)
        {
            FavoriteGame = RandomTraitPicker(FAVORITE_ACTIVITIES);
        }
        #endregion
        #region Properties
        public string FavoriteGame
        {
            get { return _favoriteGame; }
            set { _favoriteGame = value; }
        }
        #endregion
    }
}
