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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectWPF.Pets;
using ProjectWPF.Repos;
using ProjectWPF.Views;

namespace ProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MAX_PETS = 7;
        private int[] alreadyGeneratedPets = new int[4];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Show_Pets(object sender, RoutedEventArgs e)
        {
            seePetsButton.Content = "Refresh to see more pets!";
            Pet[] petsToShow = Random_Pets_Generator();
            pet1photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[0].Name}.jpg", UriKind.Relative));
            pet1Name.Text = petsToShow[0].Name;
            pet2photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[1].Name}.jpg", UriKind.Relative));
            pet2Name.Text = petsToShow[1].Name;
            pet3photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[2].Name}.jpg", UriKind.Relative));
            pet3Name.Text = petsToShow[2].Name;
            pet4photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[3].Name}.jpg", UriKind.Relative));
            pet4Name.Text = petsToShow[3].Name;
        }
        private void Pet_Selected(object sender, RoutedEventArgs e)
        {
            int petIndex = 0;
            if (sender is Image clickedImage)
            {
                switch (clickedImage.Name)
                {
                    case "pet1photo":
                        petIndex = alreadyGeneratedPets[0];
                        break;
                    case "pet2photo":
                        petIndex = alreadyGeneratedPets[1];
                        break;
                    case "pet3photo":
                        petIndex = alreadyGeneratedPets[2];
                        break;
                    case "pet4photo":
                        petIndex = alreadyGeneratedPets[3];
                        break;
                }
                PetDetails petDetails = new PetDetails(petIndex);
                petDetails.Show();
                this.Close();
            }
        }
        private Pet[] Random_Pets_Generator()
        {
            Pet[] pets = new Pet[4];
            Random randomPetSelector = new Random();
            for (int i = 0; i < pets.Length; i++)
            {
                int randomPet = randomPetSelector.Next(0, MAX_PETS);
                while (alreadyGeneratedPets.Contains(randomPet))
                {
                    randomPet = randomPetSelector.Next(0, MAX_PETS);
                }
                pets[i] = PetDatabase.GetPetsInDatabase()[randomPet];
                alreadyGeneratedPets[i] = randomPet;
            }
            return pets;
        }
    }
}
