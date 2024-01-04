using ProjectWPF.Pets;
using ProjectWPF.Repos;
using ProjectWPF.Views;
using System;
using System.Diagnostics;
using System.IO;
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
        //note the 50 here is a placeholder. They get overwritten with the indexes of the pets generated. -hana
        private int[] allGeneratedPetsIndex = new int[MAX_DISPLAYFRAMES] { 50, 50, 50, 50 };
        private int[] allOriginalGeneratedPets = new int[MAX_DISPLAYFRAMES];
        private bool showingOldPetsAgain = false;
        private int petIndex;

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            InitializeFiles();
        }

        public MainWindow(int[] petsToGenerate) : this()
        {
            InitializeFiles();
            //setting the pets generated and the previous pets generated to the current generated pets
            //previous generated pets only gets updated if we run the generate pets again
            allOriginalGeneratedPets = petsToGenerate;
            allGeneratedPetsIndex = petsToGenerate;
            showingOldPetsAgain = true;
            ShowPets();

        }
        #endregion

        /// <summary>
        /// Creates the 2 starting files and gives them starting data
        /// </summary>
        private static void InitializeFiles()
        {
            try
            {
                //Creating the startup pet file in the bin
                string filePathPets = ".\\pet_information.txt";

                if (!File.Exists(filePathPets))
                {
                    // File.Create(filePathPets);

                    // Create the file if it doesn't exist
                    string startingContentPets = "Coco,4,true,Dog,A cute white and grey dog\r\nBuddy,7,false,Dog,A friendly brown dog\r\nChichi,1,true,Hamster,A speedy little hamster\r\nSlowy,15,false,Turtle,A majestic turtle\r\nBueno,2,true,Hamster,A cute hamster that loves treats\r\nKelp,10,false,Sea Turtle,An elegant sea turtle\r\nCharlotte,1,false,Dog,A playful puppy\r\nCarrot,7,false,Dog,A cute and endearing puppy\r\nSpeedy,1,false,Hamster,A very fast and active hamster\r\nKeywe,2,false,Bird,A kiwi bird that is guaranteed to sweeten your day\r\nPlumpo,4,true,Bird,A cockatiel with minimal thoughts\r\nSmokey,289,true,Dragon,A fiery friend who will protect you for life";
                    // Write starting content to the file
                    File.WriteAllText(filePathPets, startingContentPets);
                }

                string filePathAdopters = ".\\adopter_information.txt";
                if (!File.Exists(filePathAdopters))
                {
                    // Create the file if it doesn't exist
                    string startingContent = "hana,hana@email.com,99 street,5141234567,House,1-2,0-2,Coco,Dog\r\nhana,hana@email.com,99 street,5141234567,House,1-2,0-2,Chichi,Hamster \r\ntaryn,taryn@email.com,78 street,5142223333,Apartment,3+,0-2,Plumpo,Bird\r\ntaryn,taryn@email.com,78 street,5142223333,Apartment,3+,0-2,Smokey,Dragon\r\nbob,bob@email.com,67 street,5144445555,Apartment,1-2,0-2,Bueno,Hamster";
                    // Write default content to the file
                    File.WriteAllText(filePathAdopters, startingContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
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

        /// <summary>
        /// Retrieves the pet array of pets to show, either by generating a new set or by reloading a previous set
        /// </summary>
        /// <returns>An array of pets to display.</returns>
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

        /// <summary>
        /// Displays the pets based on the given filter (adopted/not adopted)
        /// </summary>
        /// <param name="isAdoptedFilter">The bool that determines if we want adopted or non-adopted.</param>
        private void ShowPetsByFilter(bool isAdoptedFilter)
        {
            ClearDisplays();
            int usedDisplayFrames = 0; //the number of currently used display frames

            for (int i = 0; i < allGeneratedPetsIndex.Length; i++)
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

        /// <summary>
        /// Displays all pets images and names.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
