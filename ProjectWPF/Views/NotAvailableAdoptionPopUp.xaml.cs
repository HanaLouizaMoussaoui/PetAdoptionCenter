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

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for NotAvailableAdoptionPopUp.xaml. Appears when a user was unable to adopt the pet they selected.
    /// </summary>
    public partial class NotAvailableAdoptionPopUp : Window
    {
        public NotAvailableAdoptionPopUp()
        {
            InitializeComponent();
        }
        public void BtnClick_ClosePopup(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            Close();
        }
    }
}
