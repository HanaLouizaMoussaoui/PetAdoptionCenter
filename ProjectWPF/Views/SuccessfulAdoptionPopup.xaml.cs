using ProjectWPF.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for SuccessfulAdoptionPopup.xaml
    /// </summary>
    public partial class SuccessfulAdoptionPopup : Window
    {
        readonly Pet _selectedPet;
        public SuccessfulAdoptionPopup()
        {
            InitializeComponent();
        }

        public SuccessfulAdoptionPopup(int petIndex) : this()
        {
            _selectedPet = PetDatabase.PetsInDatabase[petIndex];
            selectedPetName.Text = _selectedPet.Name;
        }
        public void BtnClick_ClosePopup(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            Close();
        }
    }
}
