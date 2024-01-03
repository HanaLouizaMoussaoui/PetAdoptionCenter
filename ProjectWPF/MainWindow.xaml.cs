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
        private int[] generatedPetsIndex = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
        private int[] previousGeneratedPets = new int[MAX_DISPLAYFRAMES];
        private bool showingOldPetsAgain = false;

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(int[] petsToGenerate) : this()
        {
            //setting the pets generated and the previous pets generated to the current generated pets
            //previous generated pets only gets updated if we run the generate pets again
            previousGeneratedPets = petsToGenerate;
            generatedPetsIndex = petsToGenerate;
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
            int petIndex;
            if (sender is Image clickedImage)
            {
                switch (clickedImage.Name)
                {
                    case "pet1Photo":
                        petIndex = generatedPetsIndex[0];
                        break;
                    case "pet2Photo":
                        petIndex = generatedPetsIndex[1];
                        break;
                    case "pet3Photo":
                        petIndex = generatedPetsIndex[2];
                        break;
                    case "pet4Photo":
                        petIndex = generatedPetsIndex[3];
                        break;
                    default: petIndex = 0; break;
                }
                PetDetails petDetails = new(petIndex, generatedPetsIndex);
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
            generatedPetsIndex = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
            Pet[] pets = new Pet[MAX_DISPLAYFRAMES];
            Random randomPetSelector = new Random();
            for (int i = 0; i < pets.Length; i++)
            {
                int randomPetIndex = randomPetSelector.Next(0, MAX_PETS);
                while (generatedPetsIndex.Contains(randomPetIndex))
                {
                    randomPetIndex = randomPetSelector.Next(0, MAX_PETS);
                }
                pets[i] = PetDatabase.PetsInDatabase[randomPetIndex];
                generatedPetsIndex[i] = randomPetIndex;
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
                Trace.WriteLine("ran previous");
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
        private void Btn_Click_ShowAvailablePets(object sender, RoutedEventArgs e)
        {
            //showingOldPetsAgain = true;
            ClearDisplays();
            foreach (int i in generatedPetsIndex)
            {

            }
            Pet[] petsToShow = PreviousPetsGenerator(previousGeneratedPets);
            int usedDisplayFrames = 0; //this variable will hold the number of used display frames for the pets (max 4) 
            for (int i = 0; i < petsToShow.Length; i++)
            {
                if (!petsToShow[i].IsAdopted)
                {
                    Trace.WriteLine(petsToShow[i].Name);
                    switch (generatedPetsIndex[0])
                    {
                        case 1:
                            pet1Photo.Source = new BitmapImage(petsToShow[i].PhotoSource);
                            pet1Name.Text = petsToShow[i].Name;
                            break;
                        case 2:
                            pet2Photo.Source = new BitmapImage(petsToShow[i].PhotoSource);
                            pet2Name.Text = petsToShow[i].Name;
                            break;
                        case 3:
                            pet3Photo.Source = new BitmapImage(petsToShow[i].PhotoSource);
                            pet3Name.Text = petsToShow[i].Name;
                            break;
                        case 4:
                            pet4Photo.Source = new BitmapImage(petsToShow[i].PhotoSource);
                            pet4Name.Text = petsToShow[i].Name;
                            break;
                    }
                    usedDisplayFrames++;
                }
            }
            if (usedDisplayFrames == 0)
            {
                pet1Name.Text = "No pets are available for adoption.";
            }
        }
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


        /// <summary>
        /// Shows the pets that have been adopted from the 4 generated options
        /// </summary>
        private void Btn_Click_ShowAdoptedPets(object sender, RoutedEventArgs e)
        {
            //showingOldPetsAgain = true;
            ClearDisplays();
            Trace.WriteLine(previousGeneratedPets[0]);
            int usedDisplayFrames = 0; //this variable will hold the number of used display frames for the pets (max 4) 
            foreach (int petIndex in generatedPetsIndex)
            {
                Pet selectedPet = PetDatabase.PetsInDatabase[petIndex];
                if (!selectedPet.IsAdopted)
                {
                    switch (usedDisplayFrames)
                    {
                        case 1:
                            pet1Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet1Name.Text = selectedPet.Name;
                            break;
                        case 2:
                            pet2Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet2Name.Text = selectedPet.Name;
                            break;
                        case 3:
                            pet3Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet3Name.Text = selectedPet.Name;
                            break;
                        case 4:
                            pet4Photo.Source = new BitmapImage(selectedPet.PhotoSource);
                            pet4Name.Text = selectedPet.Name;
                            break;
                    }
                    usedDisplayFrames++;
                }
            }
            if (usedDisplayFrames == 0)
            {
                pet1Name.Text = "No pets.";
            }
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
