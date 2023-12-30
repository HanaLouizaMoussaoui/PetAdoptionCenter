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
            _selectedPet = PetDatabase.GetPetsInDatabase()[petIndex];
            selectedPetName.Text = _selectedPet.Name;
            selectedPetType.Text = _selectedPet.Type;
        }
        public void BtnClick_SubmittedAdoptionForm(object sender, RoutedEventArgs e)
        {
            string filePath = "..\\..\\..\\PetDatabaseInfo\\PetDatabaseTextFile.txt";
            _selectedPet = PetDatabase.GetPetsInDatabase()[_petIndex];
            int lineIndexToChange = _petIndex;
            string newTextForLine = $"{_selectedPet.Name},{_selectedPet.Age},true,{_selectedPet.Type},{_selectedPet.Description}";
            string[] lines = File.ReadAllLines(filePath);
            lines[lineIndexToChange] = newTextForLine;
            File.WriteAllLines(filePath, lines);
            _selectedPet.IsAdopted = true;
            string adopteeData = GetAdopteeData();
            SaveToFile(adopteeData);
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
        private string GetAdopteeData()
        {
            string adopteeInfo = $"Name: {txbName.Text} " +
                $"\nEmail: {txbEmail.Text}" +
                $"\nAddress: {txbAddress.Text}" +
                $"\nPhone Number: {txbPhone.Text}" +
                $"\nPhone Type: {txbPhoneType.Text}" +
                $"\nHome Type: TODO" +
                $"\nPermanent residents in home: TODO" +
                $"\nNumber of pets: TODO" +
                $"\nName of pet adopted: {selectedPetName.Text}" +
                $"\nType of pet adopted: {selectedPetType.Text} \n";
            return adopteeInfo;
        }
        private void SaveToFile(string content)
        {
            string filePath = "..\\..\\..\\AdopterInfo\\adoptee_information.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write(content);
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
