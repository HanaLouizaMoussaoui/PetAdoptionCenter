using ProjectWPF.Adopters;
using ProjectWPF.Repos;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for ShowAdoptees.xaml
    /// </summary>
    public partial class ShowAdopters : Window
    {
        public ShowAdopters()
        {
            InitializeComponent();
            DisplayAdoptees();
        }
        private void DisplayAdoptees()
        {
            // Fetching the adoptees we have on file
            List<Adopter> adopters = AdopterDatabase.RetrieveAdopterDatabase();

            // Fetching our textblocks from the window 
            TextBlock[] adopterNameTextBlocks = { adoptee1Name, adoptee2Name, adoptee3Name, adoptee4Name };
            TextBlock[] adopterPetTextBlocks = { adoptee1Pets, adoptee2Pets, adoptee3Pets, adoptee4Pets };

            // If there are no adopters we show a message
            if (adopters.Count == 0)
            {
                adopterNameTextBlocks[0].Text = "No one has adopted a pet yet.";
            }
            else
            {
                for (int i = 0; i < adopters.Count && i < adopterNameTextBlocks.Length; i++)
                {
                    // Displaying the adoptee's name
                    adopterNameTextBlocks[i].Text = $"{adopters[i].Name}";
                    // Using a string builder for showing the adoptee's pets
                    StringBuilder sb = new StringBuilder();

                    // Appending their first pet (since all adoptees will have at least one pet)
                    sb.Append("Adopted ");
                    sb.Append($"{adopters[i].AdoptedPets[0].Name}");

                    // If this adopter has adopted more than one pet, we append the other pet names
                    for (int j = 1; j < adopters[i].AdoptedPets.Count; j++)
                    {
                        sb.Append($"and ");
                        string petName = adopters[i].AdoptedPets[j].Name;
                        sb.Append($"{petName} ");
                    }
                    // Displaying the adopter's pet(s)
                    adopterPetTextBlocks[i].Text = sb.ToString();
                }
            }
            // Going through each adopter


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
