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

namespace WpfOpgave8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<PlantSoort> soorten;
        public List<String> planten;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuincentrumDbManager();

                ComboboxSoort.DisplayMemberPath = "Soort";
                ComboboxSoort.SelectedValuePath = "SoortNr";

                ComboboxSoort.ItemsSource = manager.GetSoorten();


            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Combobox : " + ex.Message;
            }

        }

        private void ComboboxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var manager = new TuincentrumDbManager();
                Int32 soortnr = Convert.ToInt32(ComboboxSoort.SelectedValue);

                ListboxPlantenPerSoort.ItemsSource = manager.GetPlanten(soortnr);

            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Listbox : " + ex.Message;
            }
            


        }
    }
}
