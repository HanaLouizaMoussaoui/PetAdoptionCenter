﻿using ProjectWPF.Pets;
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

namespace ProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for ShowAdoptedPets.xaml
    /// </summary>
    public partial class ShowAdoptedPets : Window
    {
        private Pet[] AdoptedPets { get; set; }
        public ShowAdoptedPets()
        {
            InitializeComponent();
            DataContext = this;
            // Assign the list of adopted pets to the AdoptedPets property. We need to do this to display an updated list of all adopted pets
             AdoptedPets = PetDatabase.PetsInDatabase.Where(pet => pet.IsAdopted).ToArray();
        }
    }
}
