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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace w61922
{
    /// <summary>
    /// Logika interakcji dla klasy Rejestracja.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var imie = TbIm.Text;
            var nazwisko = TbNaz.Text;
            var pesel = TbPes.Text;
            var miejscowosc = TbMsc.Text;
            var NrDomu = TbNr.Text;
            var Kontakt = TbKon.Text;
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            bool Poprawnie = true;
            SqlCommand sql1 = connection.CreateCommand();
            sql1.CommandText = @"SELECT Pesel From Klienci";
            SqlDataReader reader = sql1.ExecuteReader();
            while (reader.Read())
            {

                if (reader["Pesel"].ToString() == pesel || pesel.Length != 11)
                {

                    MessageBox.Show("Podany Pesel jest już przypisany do danego konta lub jest za krótki.", "Uwaga!", MessageBoxButton.OK);
                    this.Close();
                    Poprawnie = false;
                    break;
                }

            }
            reader.Close();
            if (Poprawnie)
            {
                SqlCommand sql = connection.CreateCommand();
                sql.CommandText = @"INSERT INTO [dbo].[Klienci]
                   ([imie],
                    [nazwisko],
                    [pesel],
                    [miejscowosć],
                    [nr_domu],
                    [kontakt]
                   )
                VALUES
                   (
                    @imie,
                    @nazwisko,
                    @pesel,
                    @msc,
                    @NrD,
                    @Kontakt
                    )";
                sql.Parameters.AddWithValue("imie", imie);
                sql.Parameters.AddWithValue("nazwisko", nazwisko);
                sql.Parameters.AddWithValue("pesel", pesel);
                sql.Parameters.AddWithValue("msc", miejscowosc);
                sql.Parameters.AddWithValue("NrD", NrDomu);
                sql.Parameters.AddWithValue("Kontakt", Kontakt);

                sql.ExecuteNonQuery();

                DialogResult = true;
                this.Close();
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
    }
