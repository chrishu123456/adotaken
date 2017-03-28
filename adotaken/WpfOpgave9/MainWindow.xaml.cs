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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TakenGemeenschap;

namespace WpfOpgave9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PlantInfo> Planten = new List<PlantInfo>();

        private string GeselecteerdeSoortNaam;
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

                GeselecteerdeSoortNaam = ((PlantSoort)ComboboxSoort.SelectedItem).Soort;
                Planten = manager.GetAllePlantInfo(soortnr);
                ListboxPlantenPerSoort.ItemsSource = Planten;

                ListboxPlantenPerSoort.DisplayMemberPath = "Naam";
                
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Listbox : " + ex.Message;
            }
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            List<PlantInfo> gewijzigdePlanten = new List<PlantInfo>();

            foreach (PlantInfo p in Planten)
            {
                if (p.changed == true)
                {
                    gewijzigdePlanten.Add(p);
                    p.changed = false;
                }


            }

            if ( (gewijzigdePlanten.Count >0)  && (MessageBox.Show("Gewijzigde planten van soort '" + GeselecteerdeSoortNaam + " 'opslaan ? ","Opslaan" , MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes))
                {
                var manager = new TuincentrumDbManager();
                try
                {
                    manager.GewijzigdePlantenOpslaan(gewijzigdePlanten);
                    LabelMeldingen.Content = "Button Opslaan : " + "Planten Opgeslagen";
                }
                catch (Exception ex)
                {
                    LabelMeldingen.Content = "Button Opslaan : " + ex.Message;
                }

            }
        }
    }
}
