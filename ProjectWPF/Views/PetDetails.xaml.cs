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
using System.Diagnostics;

namespace ProjectWPF.Views
{

    public partial class PetDetails : Window
    {
        private Pet _selectedPet;
        private int currentPetIndex;
        private int[] currentlyGeneratedPets;
        public PetDetails()
        {
            InitializeComponent();
        }
        public PetDetails(int petIndex, int[] alreadyGeneratedPets) : this()
        {
            currentlyGeneratedPets = alreadyGeneratedPets;
            currentPetIndex = petIndex; // Saving the pet index in a global variable so we can pass it later
            _selectedPet = PetDatabase.PetsInDatabase[petIndex];
            selectedPetPhoto.Source = new BitmapImage(_selectedPet.PhotoSource);
            selectedPetName.Text = _selectedPet.Name;
            selectedPetAge.Text = $"{_selectedPet.Age} years old";
            selectedPetType.Text = _selectedPet.Type;
            selectedPetDescription.Text = _selectedPet.Description;
            if (_selectedPet.IsAdopted)
            {
                selectedPetIsAdopted.Text = "Not Available - Already Adopted";
            }
            else selectedPetIsAdopted.Text = "Available - Up for Adoption";
        }
        public void BtnClick_GoBackMain(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow(currentlyGeneratedPets);
            newMain.Show();
            this.Close();
            
        }
        
        public void BtnClick_GoToAdoptPetPage(object sender, RoutedEventArgs e)
        {
            if (PetDatabase.PetsInDatabase[currentPetIndex].IsAdopted)
            {
                NotAvailableAdoptionPopUp notAdoptable = new NotAvailableAdoptionPopUp();
                notAdoptable.Show();
                this.Close();
            }
            else {
                AdoptionForm newAdoption = new AdoptionForm(currentPetIndex);
                newAdoption.Show();
                this.Close();
            }
        }

    }
}
