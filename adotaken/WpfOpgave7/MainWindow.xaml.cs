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

namespace WpfOpgave7
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

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Lev = new Leverancier();
                var manager = new TuincentrumDbManager();
                Lev.Naam = TextBoxNaam.Text;
                Lev.Adres = TextBoxAdres.Text;
                Lev.PostNr = TextBoxPostcode.Text;
                Lev.Woonplaats = TextBoxPlaats.Text;


                if (manager.Toevoegen(Lev.Naam, Lev.Adres, Lev.PostNr, Lev.Woonplaats) != 0)
                {
                    LabelMeldingen.Content = "Leverancier record bijgevoegd.";
                }
                else
                {
                    LabelMeldingen.Content = "Geen Leverancier record bijgevoegd.";
                }
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Leverancier toevoegen : " + ex.Message;
            }
        }

        private void EindejaarskortingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuincentrumDbManager();


                int aantalAangepasteRecords;

                if ((aantalAangepasteRecords = manager.EindejaarsKorting()) != 0)
                {
                    LabelMeldingen.Content = "aantal aangepaste records : " + aantalAangepasteRecords;
                }
                else
                {
                    LabelMeldingen.Content = "Geen aangepaste records";
                }
            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "EindejaarsKorting : " + ex.Message;
            }

        }

        private void ButtonToevoegenRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuincentrumDbManager();

                var Lev = new Leverancier();

                Lev.Naam = TextBoxNaam.Text;
                Lev.Adres = TextBoxAdres.Text;
                Lev.PostNr = TextBoxPostcode.Text;
                Lev.Woonplaats = TextBoxPlaats.Text;
                Int64 resultaat = manager.RecordNummerNaToevoegen(Lev.Naam, Lev.Adres, Lev.PostNr,Lev.Woonplaats);

                if (resultaat != 0)
                {
                    LabelMeldingen.Content = "Leverancier record nummer " + resultaat.ToString() + " is toegevoegd.";
                }
                else
                {
                    LabelMeldingen.Content = "Probleem met het toevoegen van de record.";
                }

            }
            catch (Exception ex)
            {
                LabelMeldingen.Content = "Button RecordNummer Na Toevoegen : " + ex.Message;
            }
        }
    }
}
