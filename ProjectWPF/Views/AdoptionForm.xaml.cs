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

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for AdoptionForm.xaml
    /// </summary>
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
            _selectedPet = PetDatabase.GetPetsInDatabase()[petIndex];
            selectedPetName.Text = _selectedPet.Name;
            selectedPetType.Text = _selectedPet.Type;
        }
        public void BtnClick_SubmittedAdoptionForm(object sender, RoutedEventArgs e)
        {
            _selectedPet = PetDatabase.GetPetsInDatabase()[_petIndex];
            if (_selectedPet.IsAdopted == false) 
            {
                _selectedPet.IsAdopted = true;
            }
            SaveToFile();
            SuccessfulAdoptionPopup popup = new SuccessfulAdoptionPopup();
            popup.Show();
            Close();
        }
        public void BtnClick_GoBackMain(object sender, RoutedEventArgs e) //repeated method, could make this more modular
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            Close();
        }
        //add validation here?
        private string AdopteeData()
        {
            string adopteeInfo = $"Name: {txbName.Text} " +
                $"\nEmail: {txbEmail.Text}" +
                $"Address: {txbAddress.Text}" +
                $"Phone Number: {txbPhone.Text}" +
                $"Phone Type: {txbPhoneType.Text}" +
                $"Home Type: {}" +
                $"Permanent residents in home: {}" +
                $"Number of pets: {}" +
                $"Name of pet adopted: {selectedPetName.Text}" +
                $"Type of pet adopted: {selectedPetType.Text}";
            return adopteeInfo;
        }
        private void SaveToFile()
        {
            string filePath = "..\\..\\..\\AdopterInfo\\adoptee_information.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("");
                    MessageBox.Show("Information saved properly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
