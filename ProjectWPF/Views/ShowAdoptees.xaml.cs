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
            // Fetching the adoptees we have on file
            List<Adoptee> adoptees = AdopteeDatabase.RetrieveAdopteeDatabase();

            // Fetching our textblocks from the window 
            TextBlock[] adopteeNameTextBlocks = {adoptee1Name, adoptee2Name, adoptee3Name, adoptee4Name};
            TextBlock[] adopteePetTextBlocks = {adoptee1Pets, adoptee2Pets, adoptee3Pets, adoptee4Pets};

            // If there are no adoptees we show a message
            if (adoptees.Count==0)
            {
                adopteeNameTextBlocks[0].Text = "No one has adopted a pet yet.";
            }

            // Going through each adoptee
            for (int i = 0; i < adoptees.Count && i < adopteeNameTextBlocks.Length; i++)
            {
                // Displaying the adoptee's name
                adopteeNameTextBlocks[i].Text = $"{adoptees[i].Name}";
                // Using a string builder for showing the adoptee's pets
                StringBuilder sb = new StringBuilder();

                // Appending their first pet (since all adoptees will have at least one pet)
                sb.Append("Adopted ");
                sb.Append($"{adoptees[i].AdoptedPets[0].Name}");

                // If this adoptee has adopted more than one pet, we append the other pet names
                for (int j = 1; j < adoptees[i].AdoptedPets.Count; j++)
                {
                    sb.Append($"and ");
                    string petName = adoptees[i].AdoptedPets[j].Name;
                    sb.Append($"{petName} ");
                }
                // Displaying the adoptee's pet(s)
                adopteePetTextBlocks[i].Text = sb.ToString();
            }

        }
        // Allows the user to return to the main window
        public void BtnClick_GoBackMain(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            this.Close();

        }
    }
    
}
