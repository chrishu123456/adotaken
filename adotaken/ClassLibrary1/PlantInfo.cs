using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakenGemeenschap
{
    public class PlantInfo
    {

        public bool changed { get; set; }
                                           //  private int plantnrValue;

        public int PlantNr { get; set; }

        /*
        public int PlantNr
        {
            get { return plantnrValue; }
            set { plantnrValue = value; }
        }
        */

        private String naamValue;

        public String Naam
        {
            get { return naamValue; }
            set { naamValue = value; }
        }

        private Int32 soortnrValue;

        public Int32 SoortNr
        {
            get { return soortnrValue; }
            set { soortnrValue = value; }
        }

        // private Int32 leveranciernrValue;

        public int LeverancierNr { get; set; }

        /*
        public Int32 LeverancierNr
        {
            get { return leveranciernrValue; }
            set { leveranciernrValue = value; }
        }

    */
        private String kleurValue;

        public String Kleur
        {
            get { return kleurValue; }
            set
            {
                kleurValue = value;
                changed = true;
            }
        }

        private Decimal verkoopprijsValue;

        public Decimal VerkoopPrijs
        {
            get { return verkoopprijsValue; }
            set
            {
                verkoopprijsValue = value;
                changed = true;
             }
        }


        public PlantInfo():this(0, null, 0, 0, null, 0m)
        {

        }

        public PlantInfo(int nummer, string naam, int soortnr, int leveranciernr, string kleur, decimal verkoopprijs)
        {
            this.PlantNr = nummer;
            this.Naam = naam;
            this.SoortNr = soortnr;
            this.LeverancierNr = leveranciernr;
            this.Kleur = kleur;
            this.VerkoopPrijs = verkoopprijs;
            this.changed = false;
        }
    }
}
