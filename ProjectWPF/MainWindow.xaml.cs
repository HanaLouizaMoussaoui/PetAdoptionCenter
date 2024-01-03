using ProjectWPF.Pets;
using ProjectWPF.Repos;
using ProjectWPF.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        private int[] allGeneratedPetsIndex = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
        private int[] allOriginalGeneratedPets = new int[MAX_DISPLAYFRAMES];
        private bool showingOldPetsAgain = false;
        private int petIndex;

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(int[] petsToGenerate) : this()
        {
            //setting the pets generated and the previous pets generated to the current generated pets
            //previous generated pets only gets updated if we run the generate pets again
            allOriginalGeneratedPets = petsToGenerate;
            allGeneratedPetsIndex = petsToGenerate;
            showingOldPetsAgain = true;
            ShowPets();
        }
        #endregion
        private void Btn_Click_ShowPets(object sender, RoutedEventArgs e)
        {
            ShowPets();
        }

        /// <summary>
        /// When the user selects a pet, it will call the pet details window and pass it the index of the pet they have selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Click_PetSelected(object sender, RoutedEventArgs e)
        {
            if (sender is Image clickedImage)
            {
                switch (clickedImage.Name)
                {
                    case "pet1Photo":
                        petIndex = allGeneratedPetsIndex[0];
                        break;
                    case "pet2Photo":
                        petIndex = allGeneratedPetsIndex[1];
                        break;
                    case "pet3Photo":
                        petIndex = allGeneratedPetsIndex[2];
                        break;
                    case "pet4Photo":
                        petIndex = allGeneratedPetsIndex[3];
                        break;
                    default: petIndex = 0; break;
                }
                PetDetails petDetails = new(petIndex, allGeneratedPetsIndex);
                petDetails.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Creates two arrays. One contains the index of each pet generated and the other contains the pet object.
        /// </summary>
        /// <returns>A pet array of the pets contained at each index.</returns>
        private Pet[] Random_Pets_Generator()
        {
            allGeneratedPetsIndex = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
            Pet[] pets = new Pet[MAX_DISPLAYFRAMES];
            Random randomPetSelector = new Random();
            for (int i = 0; i < pets.Length; i++)
            {
                int randomPetIndex = randomPetSelector.Next(0, MAX_PETS);
                while (allGeneratedPetsIndex.Contains(randomPetIndex))
                {
                    randomPetIndex = randomPetSelector.Next(0, MAX_PETS);
                }
                pets[i] = PetDatabase.PetsInDatabase[randomPetIndex];
                allGeneratedPetsIndex[i] = randomPetIndex;
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
            seeAllPetsButton.Visibility = Visibility.Visible;
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
                petsToShow = PreviousPetsGenerator(allOriginalGeneratedPets);
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
        
        private void ClearDisplays()
        {
            pet1Photo.Source = null;
            pet1Name.Text = null;
            pet2Photo.Source = null;
            pet2Name.Text = null;
            pet3Photo.Source = null;
            pet3Name.Text = null;
            pet4Photo.Source = null;
            pet4Name.Text = null;
        }

        private void ShowPetsByFilter(bool isAdoptedFilter)
        {
            ClearDisplays();
            int usedDisplayFrames = 0;

            for (int i = 0; i < allGeneratedPetsIndex.Length; i ++)
            {
                Pet selectedPet = PetDatabase.PetsInDatabase[allGeneratedPetsIndex[i]];

                if (selectedPet.IsAdopted == isAdoptedFilter)
                {
                    switch (usedDisplayFrames)
                    {
                        case 0:
                            pet1Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet1Name.Text = selectedPet.Name;
                            break;
                        case 1:
                            pet2Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet2Name.Text = selectedPet.Name;
                            break;
                        case 2:
                            pet3Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet3Name.Text = selectedPet.Name;
                            break;
                        case 3:
                            pet4Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet4Name.Text = selectedPet.Name;
                            break;
                    }
                    //swapping the indexes to keep the order of the pets in the generated array
                    int temp = allGeneratedPetsIndex[usedDisplayFrames]; //original pet location stored in temp
                    allGeneratedPetsIndex[usedDisplayFrames] = allGeneratedPetsIndex[i]; //overwrite first slot with new pet's location
                    allGeneratedPetsIndex[i] = temp; //make the other pet's location equal to the original
                    Trace.WriteLine("all gen pet index:" + allGeneratedPetsIndex[i]);
                    usedDisplayFrames++;
                }
            }
            if (usedDisplayFrames == 0)
            {
                pet1Name.Text = "No pets.";
            }
        }

        private void Btn_Click_ShowAvailablePets(object sender, RoutedEventArgs e)
        {
            ShowPetsByFilter(false);
        }

        private void Btn_Click_ShowAdoptedPets(object sender, RoutedEventArgs e)
        {
            ShowPetsByFilter(true);
        }

        private void Btn_Click_ShowAllPets(object sender, RoutedEventArgs e)
        {
            Pet[] petsToShow = PreviousPetsGenerator(allGeneratedPetsIndex);
            pet1Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[0].Name}.png", UriKind.Relative));
            pet1Name.Text = petsToShow[0].Name;
            pet2Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[1].Name}.png", UriKind.Relative));
            pet2Name.Text = petsToShow[1].Name;
            pet3Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[2].Name}.png", UriKind.Relative));
            pet3Name.Text = petsToShow[2].Name;
            pet4Photo.Source = new BitmapImage(new Uri($"/Images/{petsToShow[3].Name}.png", UriKind.Relative));
            pet4Name.Text = petsToShow[3].Name;
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
