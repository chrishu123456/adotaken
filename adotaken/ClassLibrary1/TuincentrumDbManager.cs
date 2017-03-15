using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace TakenGemeenschap
{
    public class TuincentrumDbManager
    {
        private static ConnectionStringSettings conTuincentrumString = ConfigurationManager.ConnectionStrings["Tuincentrum"];

        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conTuincentrumString.ProviderName);

        public DbConnection GetConnection()
        {

            var conTuincentrum = factory.CreateConnection();

            conTuincentrum.ConnectionString = conTuincentrumString.ConnectionString;
            return conTuincentrum;

        }
    }
}
