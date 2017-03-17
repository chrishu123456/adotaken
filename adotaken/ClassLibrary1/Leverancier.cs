using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakenGemeenschap
{
    public class Leverancier
    {
        private string naamValue;

        public string Naam
        {
            get { return naamValue; }
            set { naamValue = value; }
        }

        private string adresValue;

        public string Adres
        {
            get { return adresValue; }
            set { adresValue = value; }
        }

        private string postnrValue;

        public string PostNr
        {
            get { return postnrValue; }
            set { postnrValue = value; }
        }

        private string woonplaatsValue;

        public string Woonplaats
        {
            get { return woonplaatsValue; }
            set { woonplaatsValue = value; }
        }

    }
}
