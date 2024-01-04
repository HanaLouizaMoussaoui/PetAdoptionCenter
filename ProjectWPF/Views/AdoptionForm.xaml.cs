using ProjectWPF.Adopters;
using ProjectWPF.Pets;
using ProjectWPF.Repos;
using System;
using System.IO;
using System.Windows;

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for AdoptionForm.xaml
    /// </summary>
    public partial class AdoptionForm : Window
    {
        static Pet _selectedPet;
        private int _petIndex;

        #region Constructors
        public AdoptionForm()
        {
            InitializeComponent();
        }
        public AdoptionForm(int petIndex) : this()
        {
            _petIndex = petIndex;
            _selectedPet = PetDatabase.PetsInDatabase[petIndex];
            selectedPetName.Text = _selectedPet.Name;
            selectedPetType.Text = _selectedPet.Type;
        }
        #endregion

        /// <summary>
        /// Checks that the form has valid information. If so, saves the information to the adopter file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnClick_SubmittedAdoptionForm(object sender, RoutedEventArgs e)
        {
            //Checking that each form item has a value that is not null or empty
            if (string.IsNullOrWhiteSpace(txbName.Text) ||
                string.IsNullOrWhiteSpace(txbAddress.Text) ||
                string.IsNullOrWhiteSpace(txbEmail.Text) ||
                string.IsNullOrWhiteSpace(txbPhone.Text) || txbPhone.Text.Length < 3 ||
                (!radioBtnHouse.IsChecked.HasValue || !radioBtnApartment.IsChecked.HasValue) ||
                (!radioBtnResidents1_2.IsChecked.HasValue || !radioBtnResidents3_plus.IsChecked.HasValue) ||
                (!radioBtnPets0_2.IsChecked.HasValue || !radioBtnPets3_plus.IsChecked.HasValue))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Does not proceed with submission if the validation fails
            }
            if (txbPhone.Text.Length < 3)
            {
                MessageBox.Show("Please enter at least the first 3 digits of your phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Does not proceed with submission if the phone number is less than 3 digits long
            }

            // Fetching the pet that's being adopted
            _selectedPet = PetDatabase.PetsInDatabase[_petIndex];

            // Getting the adoptee data from the user info
            string adopterData = GetAdopterData();

            // Saving the adoptee data in the adoptee information file
            SaveToFile(adopterData);

            // Getting the pet database
            string filePath = ".\\pet_information.txt";

            // Updating the pet adopted status from false to true in the file database
            int lineIndexToChange = _petIndex;
            string newTextForLine = $"{_selectedPet.Name},{_selectedPet.Age},true,{_selectedPet.Type},{_selectedPet.Description}";
            string[] lines = File.ReadAllLines(filePath);
            lines[lineIndexToChange] = newTextForLine;
            File.WriteAllLines(filePath, lines);

            // Display successful adoption
            SuccessfulAdoptionPopup popup = new SuccessfulAdoptionPopup();
            popup.Show();
            Close();
        }

        /// <summary>
        /// Returns to the main window. Closes currently open/object sender window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnClick_GoBackMain(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            Close();
        }

        /// <summary>
        /// Retrieves the data from the adoption form and turns it into a string.
        /// </summary>
        /// <returns>A string containing the formatted adopter data.</returns>
        private string GetAdopterData()
        {
            string home;
            string residents;
            string pets;
            if (radioBtnHouse.IsChecked == true)
                home = "House";
            else
                home = "Apartment";
            if (radioBtnResidents1_2.IsChecked == true)
                residents = "1-2";
            else
                residents = "3+";
            if (radioBtnPets0_2.IsChecked == true)
                pets = "0-2";
            else
                pets = "3+";
            string phoneNumber = txbPhone.Text.Replace("-", "");
            Adopter adopter = new Adopter(txbName.Text, txbAddress.Text, txbEmail.Text, phoneNumber, home, residents, pets);
            string adopterInfo = $"\n{adopter.ToString()},{selectedPetName.Text},{selectedPetType.Text}";
            return adopterInfo;
        }

        /// <summary>
        /// Saving the new adopter to the file
        /// </summary>
        /// <param name="content">the content to be saved to the file</param>
        private void SaveToFile(string content)
        {
            string filePath = ".\\adopter_information.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
