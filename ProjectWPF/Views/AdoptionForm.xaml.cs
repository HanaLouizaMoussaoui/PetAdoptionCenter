using ProjectWPF.Pets;
using ProjectWPF.Repos;
using System;
using ProjectWPF.Repos;
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
using ProjectWPF.Pets;
using ProjectWPF.Views;
using System.IO;
using System.Printing;
using ProjectWPF.Adopters;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for AdoptionForm.xaml
    /// </summary>
    /// 
    public partial class AdoptionForm : Window
    {
        static Pet _selectedPet;
        private int _petIndex;
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
        public void BtnClick_SubmittedAdoptionForm(object sender, RoutedEventArgs e)
        {
            // Fetching the pet that's being adopted
            _selectedPet = PetDatabase.PetsInDatabase[_petIndex];

            // Getting the adoptee data from the user info
            string adopterData = GetAdopterData();
            // Saving the adoptee data in the adoptee information file
            SaveToFile(adopterData);

            // Getting the pet database
            string filePath = ".\\PetDatabaseTextFile.txt";
          
            // Updating the pet adopted status from false to true in the file database
            int lineIndexToChange = _petIndex;
            string newTextForLine = $"{_selectedPet.Name},{_selectedPet.Age},true,{_selectedPet.Type},{_selectedPet.Description}";
            string[] lines = File.ReadAllLines(filePath);
            lines[lineIndexToChange] = newTextForLine;
            File.WriteAllLines(filePath, lines);

            // Updating the pet database
            //PetDatabase.PetsInDatabase;

            SuccessfulAdoptionPopup popup = new SuccessfulAdoptionPopup();
            popup.Show();
            Close();
        }
        public void BtnClick_GoBackMain(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            Close();
        }
        //add validation here?

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
