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

namespace ProjectWPF.Views
{

    public partial class PetDetails : Window
    {
        private Pet _selectedPet;
        private int currentPetIndex;
        private int[] currentAlreadyGeneratedPets;
        public PetDetails()
        {
            InitializeComponent();
        }
        public PetDetails(int petIndex, int[] alreadyGeneratedPets) : this()
        {
            currentAlreadyGeneratedPets = alreadyGeneratedPets;
            currentPetIndex = petIndex; // Saving the pet index in a global variable so we can pass it later
            _selectedPet = PetDatabase.GetPetsInDatabase()[petIndex];
            selectedPetPhoto.Source = new BitmapImage(new Uri($"/Images/{_selectedPet.Name}.png", UriKind.Relative));
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
            MainWindow newMain = new MainWindow(currentAlreadyGeneratedPets);
            newMain.Show();
            this.Close();
            
        }
        
        public void BtnClick__GoToAdoptPetPage(object sender, RoutedEventArgs e)
        {
            AdoptionForm newAdoption = new AdoptionForm(currentPetIndex);
            newAdoption.Show();
            this.Close();
        }

    }
}
