using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakenGemeenschap
{
    public class PlantInfo
    {
        private String naamValue;

        public String Naam
        {
            get { return naamValue; }
            set { naamValue = value; }
        }

        private String soortValue;

        public String Soort
        {
            get { return soortValue; }
            set { soortValue = value; }
        }

        private String leverancierValue;

        public String Leverancier
        {
            get { return leverancierValue; }
            set { leverancierValue = value; }
        }

        private String kleurValue;

        public String Kleur
        {
            get { return kleurValue; }
            set { kleurValue = value; }
        }

        private Decimal kostprijsValue;

        public Decimal Kostprijs
        {
            get { return kostprijsValue; }
            set { kostprijsValue = value; }
        }

        public PlantInfo():this(null, null, null, null, 0m)
        {

        }

        public PlantInfo(string naam, string soort, string leverancier, string kleur, decimal kostprijs)
        {
            this.Naam = naam;
            this.Soort = soort;
            this.Leverancier = leverancier;
            this.Kleur = kleur;
            this.Kostprijs = kostprijs;
        }
    }
}
