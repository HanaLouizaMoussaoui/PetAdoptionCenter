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
            List<Adoptee> adoptees = AdopteeDatabase.RetrieveAdopteeDatabase();
            TextBlock[] adopteeNameTextBlocks = {adoptee1Name, adoptee2Name, adoptee3Name, adoptee4Name};
            TextBlock[] adopteePetTextBlocks = { adoptee1Pets, adoptee2Pets, adoptee3Pets, adoptee4Pets };
            if (adoptees.Count==0)
            {
                adopteeNameTextBlocks[0].Text = "No one has adopted a pet yet.";
            }
            for (int i = 0; i < adoptees.Count && i < adopteeNameTextBlocks.Length; i++)
            {
                adopteeNameTextBlocks[i].Text = $"{adoptees[i].Name}";
                StringBuilder sb = new StringBuilder();
                sb.Append("Adopted ");
                sb.Append($"{adoptees[i].AdoptedPets[0].Name} ");
        
                    for (int j = 1; j < adoptees[i].AdoptedPets.Count; j++)
                    {
                        sb.Append($"and ");
                        string petName = adoptees[i].AdoptedPets[j].Name;
                        sb.Append($"{petName} ");
                    }

                adopteePetTextBlocks[i].Text = sb.ToString();
            }

        }
        public void BtnClick_GoBackMain(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            this.Close();

        }
    }
    
}
