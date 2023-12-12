using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Dog : Pet
    {
        #region Fields
        private string[] FAVORITE_GAMES = {"Fetch", "Hide and Seek", "Swimming"};
        private string _favoriteGame;
        #endregion
        #region Constructors
        public Dog(string name, int age, bool isAdopted) : base(name, age, isAdopted)
        {
            FavoriteGame = RandomTraitPicker(FAVORITE_GAMES);
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
