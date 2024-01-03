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
        private const int MAX_DISPLAYFRAMES = 4;
        //note the 50 here is a placeholder
        private int[] alreadyGeneratedPets = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
        private int[] previousGeneratedPets = new int[MAX_DISPLAYFRAMES];
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
            alreadyGeneratedPets = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
            Pet[] pets = new Pet[MAX_DISPLAYFRAMES];
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

        /// <summary>
        /// Shows pets previously generated on the page if the user returns to the main screen from the pet details page.
        /// </summary>
        /// <param name="petsToGenerate">An array containing the index of the previously generated pets.</param>
        /// <returns>An array of 4 pets that were previously generated.</returns>
        private Pet[] PreviousPetsGenerator(int[] petsToGenerate)
        {
            Pet[] pets = new Pet[MAX_DISPLAYFRAMES];
            for (int i = 0; i < pets.Length; i++)
            {
                int petIndex = petsToGenerate[i];
                pets[i] = PetDatabase.PetsInDatabase[petIndex];
            }
            return pets;
        }

        /// <summary>
        /// Displays 4 randomized pets and their images. Also makes 2 buttons appear to filter the generated pets.
        /// </summary>
        private void ShowPets()
        {
            seePetsButton.Content = "Refresh to see more pets!";
            seeAvailablePetsButton.Visibility = Visibility.Visible;
            seeAdoptedPetsButton.Visibility = Visibility.Visible;
            Pet[] petsToShow = GetPetsToShow();
            pet1Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[0].Name}.png", UriKind.Relative));
            pet1Name.Text = petsToShow[0].Name;
            pet2Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[1].Name}.png", UriKind.Relative));
            pet2Name.Text = petsToShow[1].Name;
            pet3Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[2].Name}.png", UriKind.Relative));
            pet3Name.Text = petsToShow[2].Name;
            pet4Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[3].Name}.png", UriKind.Relative));
            pet4Name.Text = petsToShow[3].Name;
        }

        private Pet[] GetPetsToShow()
        {
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
            return petsToShow;
        }

        /// <summary>
        /// Shows the pets that are available for adoption from the 4 generated options.
        /// </summary>
        private void Btn_Click_ShowAvailablePets(object sender, RoutedEventHandler e)
        {
            ShowAvailablePets();
        }

        private void ShowAvailablePets()
        {
Pet[] petsToShow = GetPetsToShow();
            int usedDisplayFrames = 0; //this variable will hold the number of used display frames for the pets (max 4) 
            for (int i = 0; i <  petsToShow.Length; i++)
            {
                if (petsToShow[i].IsAdopted == false )
                {
                    switch (usedDisplayFrames)
                    {
                        case 1:
                            pet1Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[i].Name}.png", UriKind.Relative));
                            pet1Name.Text = petsToShow[i].Name;
                            break;
                        case 2:
                            pet2Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[i].Name}.png", UriKind.Relative));
                            pet2Name.Text = petsToShow[i].Name;
                            break;
                        case 3:
                            pet3Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[i].Name}.png", UriKind.Relative));
                            pet3Name.Text = petsToShow[i].Name;
                            break;
                        case 4:
                            pet4Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[i].Name}.png", UriKind.Relative));
                            pet4Name.Text = petsToShow[i].Name;
                            break;
                    }
                    usedDisplayFrames++;
                }
            }
            if (usedDisplayFrames == 0 ) 
            {
                pet1Name.Text = "None of these pets are available for adoption.";
            }
        }

        /// <summary>
        /// Shows the pets that have been adopted from the 4 generated options
        /// </summary>
        private void Btn_Click_ShowAdoptedPets(object sender, RoutedEventHandler e)
        {
            Pet[] petsToShow = GetPetsToShow();
        }

        /// <summary>
        /// Opens a new window displaying 4 adopters. Closes the currently opened window.
        /// </summary>
        /// <param name="sender">The calling object (in this case the main window).</param>
        /// <param name="e"></param>
        private void Btn_Click_Show_Adopters(object sender, RoutedEventArgs e)
        {    
            ShowAdopters adoptees = new ShowAdopters();
            adoptees.Show();
            this.Close();
        }
  
    }
}
