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
        private const int MAX_PETS = 12;
        //note the 50 here is a placeholder
        private int[] alreadyGeneratedPets = new int[4] { 50, 50, 50, 50 };
        private int[] previousGeneratedPets = new int[4];
        private bool showingOldPetsAgain = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(int[] petsToGenerate): this()
        {
            previousGeneratedPets = petsToGenerate;
            alreadyGeneratedPets = petsToGenerate;
            showingOldPetsAgain = true;
            ShowPets();
        }

        private void Button_Click_Show_Pets(object sender, RoutedEventArgs e)
        {
            ShowPets();
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
                PetDetails petDetails = new PetDetails(petIndex,alreadyGeneratedPets);
                petDetails.Show();
                this.Close();
            }
        }
        private Pet[] Random_Pets_Generator()
        {
            alreadyGeneratedPets = new int[4] { 50, 50, 50, 50 };
            Pet[] pets = new Pet[4];
            Random randomPetSelector = new Random();
            for (int i = 0; i < pets.Length; i++)
            {
                int randomPet = randomPetSelector.Next(0, MAX_PETS);
                while (alreadyGeneratedPets.Contains(randomPet))
                {
                    randomPet = randomPetSelector.Next(0, MAX_PETS);
                }
                pets[i] = PetDatabase.PetsInDatabase[randomPet];
                alreadyGeneratedPets[i] = randomPet;
            }
            return pets;
        }
        private Pet[] PreviousPetsGenerator(int[] petsToGenerate)
        {
            Pet[] pets = new Pet[4];
            for (int i = 0; i < pets.Length; i++)
            {
                int petIndex = petsToGenerate[i];
                pets[i] = PetDatabase.PetsInDatabase[petIndex];
            }
            return pets;
        }
        private void ShowPets()
        {
            seePetsButton.Content = "Refresh to see more pets!";
            seeAvailablePetsButton.Visibility = Visibility.Visible;
            seeAdoptedPetsButton.Visibility = Visibility.Visible;
            Pet[] petsToShow;
            if (showingOldPetsAgain)
            {
                petsToShow = PreviousPetsGenerator(previousGeneratedPets);
                showingOldPetsAgain = false;
            }
            else
            {
                petsToShow = Random_Pets_Generator();
            }
            pet1photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[0].Name}.png", UriKind.Relative));
            pet1Name.Text = petsToShow[0].Name;
            pet2photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[1].Name}.png", UriKind.Relative));
            pet2Name.Text = petsToShow[1].Name;
            pet3photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[2].Name}.png", UriKind.Relative));
            pet3Name.Text = petsToShow[2].Name;
            pet4photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[3].Name}.png", UriKind.Relative));
            pet4Name.Text = petsToShow[3].Name;
        }
        private void Btn_Click_Show_Adopters(object sender, RoutedEventArgs e)
        {    
            ShowAdopters adoptees = new ShowAdopters();
            adoptees.Show();
            this.Close();
        }
  
    }
}
