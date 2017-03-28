using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakenGemeenschap
{
    public class PlantSoort
    {
        private String soortValue;

        public String Soort
        {
            get { return soortValue; }
            set { soortValue = value; }
        }

        private int soortnrValue;

        public int SoortNr
        {
            get { return soortnrValue; }
            set { soortnrValue = value; }
        }


        public PlantSoort():this(null,0)
        {

        }

        public PlantSoort(string soort, int soortnr)
        {
            this.Soort = soort;
            this.SoortNr = soortnr;
        }
    }

}
