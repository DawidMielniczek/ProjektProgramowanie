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
using System.Text.RegularExpressions;

namespace w61922
{
    /// <summary>
    /// Logika interakcji dla klasy Oddaj.xaml
    /// </summary>
    public partial class Oddaj : Window
    {
        public string Haslo { get; set; }
        Wypożyczenia Wypozyczenie = new Wypożyczenia();
        List<Wypożyczenia> lista = new List<Wypożyczenia>();
        public Oddaj(string haslo)
        {
            Haslo = haslo;
            lista = new List<Wypożyczenia>();
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();

            sql.CommandText = @"Select * From Samochody,Wypożyczenia,Klienci where Klienci.id_klienta = Wypożyczenia.id_użytkownika AND Samochody.id_samochodu = Wypożyczenia.id_samochodu AND Klienci.pesel = @pesel";
            sql.Parameters.AddWithValue("@pesel", Haslo);
            SqlDataReader reader = sql.ExecuteReader();


            while (reader.Read())
            {
                lista.Add(new Wypożyczenia()
                {
                    Id_samochodu = (int)reader["id_samochodu"],
                    Marka = Convert.ToString(reader["Marka"]),
                    Model = Convert.ToString(reader["Model"]),
                    Poj_silnika = Convert.ToString(reader["Poj_silnika"]),
                    Rok_prod = Convert.ToString(reader["Rok_prod"])
                }) ;
            }
            reader.Close();

            InitializeComponent();
            DgOddaj.Columns.Add(new DataGridTextColumn() { Header = "Id_samochodu", Binding = new Binding("Id_samochodu") });
            DgOddaj.Columns.Add(new DataGridTextColumn() { Header = "Marka", Binding = new Binding("Marka") });
            DgOddaj.Columns.Add(new DataGridTextColumn() { Header = "Model", Binding = new Binding("Model") });
            DgOddaj.Columns.Add(new DataGridTextColumn() { Header = "Poj_silnika", Binding = new Binding("Poj_silnika") });
            DgOddaj.Columns.Add(new DataGridTextColumn() { Header = "Rok_prod", Binding = new Binding("Rok_prod") });

            DgOddaj.AutoGenerateColumns = false;
            DgOddaj.ItemsSource = lista;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool Poprawnie = false;
            var id = TbId.Text;
            var marka = TbMar.Text;
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand dane = connection.CreateCommand();

            dane.CommandText = @"Select Samochody.id_samochodu, Marka From Samochody,Wypożyczenia,Klienci where Klienci.id_klienta = Wypożyczenia.id_użytkownika AND Samochody.id_samochodu = Wypożyczenia.id_samochodu AND Klienci.pesel = @pesel";
            dane.Parameters.AddWithValue("@pesel", Haslo);
            SqlDataReader reader = dane.ExecuteReader();

            while (reader.Read())
            {
                if (reader["id_samochodu"].ToString() == id && reader["Marka"].ToString().Trim() == marka)
                {
                    Poprawnie = true;
                    break;
                }
            }
            reader.Close();

            if (!Poprawnie)
            {
                MessageBox.Show("Nie prawidłowe dane o samochdozie!");
                return;
            }
            else
            {   
                SqlCommand sql = connection.CreateCommand();

                sql.CommandText = @"Update Samochody set Dostępność = 'tak' where id_samochodu = @id";
                sql.Parameters.AddWithValue("@id", id);
                sql.ExecuteNonQuery();

                SqlCommand sql1 = connection.CreateCommand();
                sql1.CommandText = @"Delete from Wypożyczenia where id_samochodu = @id";
                sql1.Parameters.AddWithValue("@id", id);
                sql1.ExecuteNonQuery();
                DialogResult = true;
            }
        }
    }
}
