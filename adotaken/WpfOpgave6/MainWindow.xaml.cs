﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TakenGemeenschap;

namespace WpfOpgave6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpzoeken_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var manager = new TuincentrumDbManager();

               
                PlantInfo plant = manager.PlantOpzoeken(TextBoxPlantnummer.Text);
                LabeLNaam.Content = "Naam : " + plant.Naam;
                LabelSoort.Content = "Soort : " + plant.Soort;
                LabelLeverancier.Content = "Leverancier : " + plant.Leverancier;
                LabelKleur.Content = "Kleur : " + plant.Kleur;
                LabelKostprijs.Content = "Kostprijs : " + plant.Kostprijs.ToString();
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Button Opzoeken : " + ex.Message;
            }


            
        }
    }
}