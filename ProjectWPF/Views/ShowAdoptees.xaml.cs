using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectWPF.Repos;
using ProjectWPF.Adoptees;

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for ShowAdoptees.xaml
    /// </summary>
    public partial class ShowAdoptees : Window
    {
        public ShowAdoptees()
        {
            InitializeComponent();
            DisplayAdoptees();
        }
        private void DisplayAdoptees()
        {
            List<Adoptee> adoptees = AdopteeDatabase.GetAdopteesInDatabase();
            TextBlock[] adopteeNameTextBlocks = {adoptee1Name, adoptee2Name, adoptee3Name, adoptee4Name};
            TextBlock[] adopteePetTextBlocks = { adoptee1Pets, adoptee2Pets, adoptee3Pets, adoptee4Pets };

            for (int i = 0; i < adoptees.Count && i < adopteeNameTextBlocks.Length; i++)
            {
                adopteeNameTextBlocks[i].Text = $"{adoptees[i].Name}";
                adopteePetTextBlocks[i].Text= $"Adopted {adoptees[i].AdoptedPet[0].Name}"; // For now, only showing one pet they adopted
            }

        }
    }
    
}
