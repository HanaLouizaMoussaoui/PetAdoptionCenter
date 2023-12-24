﻿using ProjectWPF.Repos;
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
        public PetDetails()
        {
            InitializeComponent();
        }
        public PetDetails(int petIndex) : this()
        {
            _selectedPet = PetDatabase.GetPetsInDatabase()[petIndex];
            selectedPetPhoto.Source = new BitmapImage(new Uri($"/Images/{_selectedPet.Name}.jpg", UriKind.Relative));
            selectedPetName.Text = _selectedPet.Name;
            selectedPetAge.Text = $"{_selectedPet.Age} years old";
            selectedPetType.Text = _selectedPet.Type;
            selectedPetDescription.Text = _selectedPet.Description;

        }
        public void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            this.Close();
            
        }


    }
}