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

        public int Toevoegen(string leverancier, string adres, string postcode, string plaats)
        {
            var TuincentrumDb = new TuincentrumDbManager();

            using (var TuincentrumDbConnect = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbCommand = TuincentrumDbConnect.CreateCommand())
                {

                    TuincentrumDbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    TuincentrumDbCommand.CommandText = "Toevoegen";

                    DbParameter TuincentrumDbParLeverancier = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParLeverancier.ParameterName = "@Leverancier";
                    TuincentrumDbParLeverancier.Value = leverancier;
                    TuincentrumDbParLeverancier.DbType = System.Data.DbType.String;
                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParLeverancier);

                    DbParameter TuincentrumDbParAdres = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParAdres.ParameterName = "@Adres";
                    TuincentrumDbParAdres.Value = adres;
                    TuincentrumDbParAdres.DbType = System.Data.DbType.String;
                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParAdres);

                    DbParameter TuincentrumDbParPostcode = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParPostcode.ParameterName = "@Postcode";
                    TuincentrumDbParPostcode.DbType = System.Data.DbType.String;
                    TuincentrumDbParPostcode.Value = postcode;

                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParPostcode);

                    DbParameter TuincentrumDbParPlaats = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParPlaats.ParameterName = "@Plaats";
                    TuincentrumDbParPlaats.Value = plaats;
                    TuincentrumDbParPlaats.DbType = System.Data.DbType.String;
                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParPlaats);

                    TuincentrumDbConnect.Open();
                    return TuincentrumDbCommand.ExecuteNonQuery();
                }
            }
        }

        public int EindejaarsKorting()
        {
            var TuincentrumDb = new TuincentrumDbManager();

            using (var TuincentrumDbConnect = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbCommand = TuincentrumDbConnect.CreateCommand())
                {
                    TuincentrumDbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    TuincentrumDbCommand.CommandText = "EindejaarsKorting";

                    TuincentrumDbConnect.Open();
                    return TuincentrumDbCommand.ExecuteNonQuery();
                }
            }
        }

    }
}
