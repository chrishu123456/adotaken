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

        public void VervangLeverancier()
        {
            var TuincentrumDb = new TuincentrumDbManager();

            using (var TuincentrumDbConnection = TuincentrumDb.GetConnection())
            {
                TuincentrumDbConnection.Open();

                using (var TuincentrumDbTransaction = TuincentrumDbConnection.BeginTransaction())
                {
                    using (var TuincentrumDbVervangInPlantenTabelLev2DoorLev3Command = TuincentrumDbConnection.CreateCommand())
                    {
                        TuincentrumDbVervangInPlantenTabelLev2DoorLev3Command.CommandType = CommandType.StoredProcedure;
                        TuincentrumDbVervangInPlantenTabelLev2DoorLev3Command.CommandText = "Vervanglev2doorlev3";

                        TuincentrumDbVervangInPlantenTabelLev2DoorLev3Command.Transaction = TuincentrumDbTransaction;
                        if (TuincentrumDbVervangInPlantenTabelLev2DoorLev3Command.ExecuteNonQuery() == 0)
                        {
                            TuincentrumDbTransaction.Rollback();
                            throw new Exception("Vervangen lev2 door lev3 veroorzaakt problemen.");
                        }
                    }

                    using (var TuincentrumDbVerwijderLev2Command = TuincentrumDbConnection.CreateCommand())
                    {
                        TuincentrumDbVerwijderLev2Command.CommandType = CommandType.StoredProcedure;
                        TuincentrumDbVerwijderLev2Command.CommandText = "VerwijderLev2";

                        TuincentrumDbVerwijderLev2Command.Transaction = TuincentrumDbTransaction;

                        if (TuincentrumDbVerwijderLev2Command.ExecuteNonQuery() == 0)
                        {
                            TuincentrumDbTransaction.Rollback();
                            throw new Exception("Verwijderen lev2 veroorzaakt problemen.");
                        }

                    }

                    TuincentrumDbTransaction.Commit();

                }
            }
        }

        public decimal GemiddeldeKostPrijs(String soort)
        {

            var TuincentrumDb = new TuincentrumDbManager();



            using (var TuincentrumDbConnection = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbGemKostprijsCommand = TuincentrumDbConnection.CreateCommand())
                {
                    TuincentrumDbGemKostprijsCommand.CommandType = CommandType.StoredProcedure;
                    TuincentrumDbGemKostprijsCommand.CommandText = "GemKostprijs";

                    DbParameter ParTuincentrumDbSoort = TuincentrumDbGemKostprijsCommand.CreateParameter();
                    ParTuincentrumDbSoort.ParameterName = "@soort";
                    ParTuincentrumDbSoort.Value = soort;
                    ParTuincentrumDbSoort.DbType = DbType.String;

                    TuincentrumDbGemKostprijsCommand.Parameters.Add(ParTuincentrumDbSoort);

                    TuincentrumDbConnection.Open();
                    object GemKostprijs = TuincentrumDbGemKostprijsCommand.ExecuteScalar();

                    if (GemKostprijs == null)
                    {
                        throw new Exception("Iets misgegaan bij berekenen gemiddelde kostprijs. ");
                    }
                    else
                    {
                        return (decimal)GemKostprijs;
                    }
                }
            }
        }

        /*
        public PlantInfo PlantOpzoeken(String plantnummer)
        {
            var TuincentrumDb = new TuincentrumDbManager();

            int nummer;

            if (!int.TryParse(plantnummer, out nummer))
            { throw new Exception("Plantnummer is geen getal."); }

            using (var TuincentrumDbConnection = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbPlantOpzoekenCommand = TuincentrumDbConnection.CreateCommand())
                {
                    TuincentrumDbPlantOpzoekenCommand.CommandType = CommandType.StoredProcedure;
                    TuincentrumDbPlantOpzoekenCommand.CommandText = "PlantOpzoeken";

                    DbParameter ParTuincentrumNummer = TuincentrumDbPlantOpzoekenCommand.CreateParameter();
                    ParTuincentrumNummer.ParameterName = "@nummer";
                    ParTuincentrumNummer.Value = nummer;
                    ParTuincentrumNummer.DbType = DbType.Int32;

                    TuincentrumDbPlantOpzoekenCommand.Parameters.Add(ParTuincentrumNummer);

                    DbParameter ParTuincentrumNaam = TuincentrumDbPlantOpzoekenCommand.CreateParameter();
                    ParTuincentrumNaam.ParameterName = "@naam";
                    ParTuincentrumNaam.DbType = DbType.String;
                    ParTuincentrumNaam.Direction = ParameterDirection.Output;
                    ParTuincentrumNaam.Size = 50;

                    TuincentrumDbPlantOpzoekenCommand.Parameters.Add(ParTuincentrumNaam);

                    DbParameter ParTuincentrumSoort = TuincentrumDbPlantOpzoekenCommand.CreateParameter();
                    ParTuincentrumSoort.ParameterName = "@soort";
                    ParTuincentrumSoort.DbType = DbType.String;
                    ParTuincentrumSoort.Direction = ParameterDirection.Output;
                    ParTuincentrumSoort.Size = 50;

                    TuincentrumDbPlantOpzoekenCommand.Parameters.Add(ParTuincentrumSoort);

                    DbParameter ParTuincentrumLeverancier = TuincentrumDbPlantOpzoekenCommand.CreateParameter();
                    ParTuincentrumLeverancier.ParameterName = "@leverancier";
                    ParTuincentrumLeverancier.DbType = DbType.String;
                    ParTuincentrumLeverancier.Direction = ParameterDirection.Output;
                    ParTuincentrumLeverancier.Size = 50;

                    TuincentrumDbPlantOpzoekenCommand.Parameters.Add(ParTuincentrumLeverancier);

                    DbParameter ParTuincentrumKleur = TuincentrumDbPlantOpzoekenCommand.CreateParameter();
                    ParTuincentrumKleur.ParameterName = "@kleur";
                    ParTuincentrumKleur.DbType = DbType.String;
                    ParTuincentrumKleur.Direction = ParameterDirection.Output;
                    ParTuincentrumKleur.Size = 50;

                    TuincentrumDbPlantOpzoekenCommand.Parameters.Add(ParTuincentrumKleur);

                    DbParameter ParTuincentrumKostprijs = TuincentrumDbPlantOpzoekenCommand.CreateParameter();
                    ParTuincentrumKostprijs.ParameterName = "@kostprijs";
                    ParTuincentrumKostprijs.DbType = DbType.Currency;
                    ParTuincentrumKostprijs.Direction = ParameterDirection.Output;

                    TuincentrumDbPlantOpzoekenCommand.Parameters.Add(ParTuincentrumKostprijs);

                    TuincentrumDbConnection.Open();

                    if (TuincentrumDbPlantOpzoekenCommand.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("Iets misgelopen bij het opzoeken van de plant.");
                    }
                    else
                    { return new PlantInfo((String)ParTuincentrumNaam.Value, (String)ParTuincentrumSoort.Value, (String)ParTuincentrumLeverancier.Value, (String)ParTuincentrumKleur.Value, (Decimal)ParTuincentrumKostprijs.Value); }
                }
            }
        }
        */
        public Int64 RecordNummerNaToevoegen(string leverancier, string adres, string postcode, string plaats)
        {
            var TuincentrumDb = new TuincentrumDbManager();

            using (var TuincentrumDbConnect = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbCommand = TuincentrumDbConnect.CreateCommand())
                {

                    TuincentrumDbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    TuincentrumDbCommand.CommandText = "NummerToegevoegdeRecord";

                    DbParameter TuincentrumDbParLeverancier = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParLeverancier.ParameterName = "@leverancier";
                    TuincentrumDbParLeverancier.Value = leverancier;
                    TuincentrumDbParLeverancier.DbType = System.Data.DbType.String;
                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParLeverancier);

                    DbParameter TuincentrumDbParAdres = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParAdres.ParameterName = "@adres";
                    TuincentrumDbParAdres.Value = adres;
                    TuincentrumDbParAdres.DbType = System.Data.DbType.String;
                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParAdres);

                    DbParameter TuincentrumDbParPostcode = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParPostcode.ParameterName = "@postcode";
                    TuincentrumDbParPostcode.DbType = System.Data.DbType.String;
                    TuincentrumDbParPostcode.Value = postcode;

                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParPostcode);

                    DbParameter TuincentrumDbParPlaats = TuincentrumDbCommand.CreateParameter();
                    TuincentrumDbParPlaats.ParameterName = "@plaats";
                    TuincentrumDbParPlaats.Value = plaats;
                    TuincentrumDbParPlaats.DbType = System.Data.DbType.String;
                    TuincentrumDbCommand.Parameters.Add(TuincentrumDbParPlaats);

                    TuincentrumDbConnect.Open();
                    Int64 klantnr = Convert.ToInt64(TuincentrumDbCommand.ExecuteScalar());

                    return klantnr;
                }
            }
        }

        public List<PlantSoort> GetSoorten()
        {
            var TuincentrumDb = new TuincentrumDbManager();

            List<PlantSoort> soorten = new List<PlantSoort>();

            using (var TuincentrumDbConnection = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbCommand = TuincentrumDbConnection.CreateCommand())
                {
                    TuincentrumDbCommand.CommandType = CommandType.Text;
                    TuincentrumDbCommand.CommandText = "Select Soort, SoortNr from Soorten";
                    //  TuincentrumDbCommand.CommandText = "Select Planten.Naam from Planten inner join Soorten on Soorten.SoortNr = Planten.SoortNr where Soorten.Soort=@soort";

                    //       DbParameter ParTuincentrumSoort = TuincentrumDbCommand.CreateParameter();
                    //       ParTuincentrumSoort.ParameterName = "@soortnr";
                    //       ParTuincentrumSoort.Value = soort;
                    //       ParTuincentrumSoort.DbType = DbType.String;

                    //     TuincentrumDbCommand.Parameters.Add(ParTuincentrumSoort);

                    TuincentrumDbConnection.Open();

                    using (var reader = TuincentrumDbCommand.ExecuteReader())
                    {
                        //   Int32 PlantNrPos = reader.GetOrdinal("PlantNr");
                        Int32 SoortPos = reader.GetOrdinal("Soort");
                        Int32 SoortNrPos = reader.GetOrdinal("SoortNr");
                        //   Int32 SoortNrPos = reader.GetOrdinal("SoortNr");
                        //   Int32 LevNrPos = reader.GetOrdinal("Levnr");
                        //   Int32 KleurPos = reader.GetOrdinal("Kleur");
                        //   Int32 VerkoopprijsPos = reader.GetOrdinal("VerkoopPrijs");

                        while (reader.Read())
                        {
                            soorten.Add(new PlantSoort(reader.GetString(SoortPos), reader.GetInt32(SoortNrPos)));
                        }
                    }


                }
            }
            return soorten;
        }

        public List<String> GetPlanten(Int32 soortnr)

        {
            var TuincentrumDb = new TuincentrumDbManager();

            List<String> planten = new List<String>();

            using (var TuincentrumDbConnection = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbCommand = TuincentrumDbConnection.CreateCommand())
                {
                    TuincentrumDbCommand.CommandType = CommandType.Text;

                    TuincentrumDbCommand.CommandText = "Select Naam from Planten where SoortNr=@soortnr";

                    DbParameter ParTuincentrumSoort = TuincentrumDbCommand.CreateParameter();
                    ParTuincentrumSoort.ParameterName = "@soortnr";
                    ParTuincentrumSoort.Value = soortnr;
                    ParTuincentrumSoort.DbType = DbType.Int32;

                    TuincentrumDbCommand.Parameters.Add(ParTuincentrumSoort);

                    TuincentrumDbConnection.Open();
                    using (var reader = TuincentrumDbCommand.ExecuteReader())
                    {
                        Int32 PlantNaamPos = reader.GetOrdinal("Naam");

                        while (reader.Read())
                        {
                            planten.Add(reader.GetString(PlantNaamPos));
                        }
                    }


                }
            }
            return planten;
        }

        public List<PlantInfo> GetAllePlantInfo(Int32 soortnr)
        {
            var TuincentrumDb = new TuincentrumDbManager();

            List<PlantInfo> planten = new List<PlantInfo>();

            using (var TuincentrumDbConnection = TuincentrumDb.GetConnection())
            {
                using (var TuincentrumDbCommand = TuincentrumDbConnection.CreateCommand())
                {
                    TuincentrumDbCommand.CommandType = CommandType.Text;

                    TuincentrumDbCommand.CommandText = "Select PlantNr, Naam, SoortNr, LevNr, Kleur, VerkoopPrijs from Planten where SoortNr=@soortnr";

                    DbParameter ParTuincentrumSoort = TuincentrumDbCommand.CreateParameter();
                    ParTuincentrumSoort.ParameterName = "@soortnr";
                    ParTuincentrumSoort.Value = soortnr;
                    ParTuincentrumSoort.DbType = DbType.Int32;

                    TuincentrumDbCommand.Parameters.Add(ParTuincentrumSoort);

                    TuincentrumDbConnection.Open();

                    using (var reader = TuincentrumDbCommand.ExecuteReader())
                    {
                        Int32 PlantNrPos = reader.GetOrdinal("PlantNr");
                        Int32 PlantNaamPos = reader.GetOrdinal("Naam");
                        Int32 SoortNrPos = reader.GetOrdinal("SoortNr");
                        Int32 LevNrPos = reader.GetOrdinal("Levnr");
                        Int32 KleurPos = reader.GetOrdinal("Kleur");
                        Int32 VerkoopPrijsPos = reader.GetOrdinal("VerkoopPrijs");

                        while (reader.Read())
                        {
                            planten.Add(new PlantInfo(reader.GetInt32(PlantNrPos), reader.GetString(PlantNaamPos), reader.GetInt32(SoortNrPos), reader.GetInt32(LevNrPos), reader.GetString(KleurPos), reader.GetDecimal(VerkoopPrijsPos)));
                        }
                    }

                }

            }
            return planten;
        }
    }
}
