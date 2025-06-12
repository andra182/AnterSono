using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuestPDF.Infrastructure;
using QuestPDF;

namespace AnterSono
{
    internal static class Program
    {
        /// <summary> 
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LandingPage());
            Initializer initializer = new Initializer();
            initializer.CekDanInsertTarif();
        }

    }

    public class Initializer
    {
        public void CekDanInsertTarif()
        {
            string[] layanan = { "regular", "express", "sameday" };
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                foreach (var jenis in layanan)
                {
                    string cekQuery = "SELECT COUNT(*) FROM tarif WHERE jenis_layanan = @jenis";
                    using (SqlCommand cekCmd = new SqlCommand(cekQuery, conn))
                    {
                        cekCmd.Parameters.AddWithValue("@jenis", jenis);
                        int count = (int)cekCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            string insertQuery = "INSERT INTO tarif (jenis_layanan, berat_max, harga) VALUES (@jenis, @berat, @harga)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@jenis", jenis);
                                insertCmd.Parameters.AddWithValue("@berat", GetBeratMax(jenis));
                                insertCmd.Parameters.AddWithValue("@harga", GetHargaPerKm(jenis));
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private int GetBeratMax(string jenis)
        {
            switch (jenis)
            {
                case "regular":
                    return 20;
                case "express":
                    return 10;
                case "sameday":
                    return 5;
                default:
                    throw new ArgumentException("Jenis layanan tidak dikenal.");
            }
        }

        private int GetHargaPerKm(string jenis)
        {
            switch (jenis)
            {
                case "regular":
                    return 2000;
                case "express":
                    return 4000;
                case "sameday":
                    return 10000;
                default:
                    throw new ArgumentException("Jenis layanan tidak dikenal.");
            }
        }
    }
}
