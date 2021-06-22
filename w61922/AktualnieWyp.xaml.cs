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
    /// Logika interakcji dla klasy AktualnieWyp.xaml
    /// </summary>
    public partial class AktualnieWyp : Window
    {
        Wypożyczenia Wypozyczenie = new Wypożyczenia();
        List<Wypożyczenia> lista = new List<Wypożyczenia>();
        public AktualnieWyp(string Haslo)
        {
            lista = new List<Wypożyczenia>();
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();
           
            sql.CommandText = @"Select Marka,Model, Poj_silnika,Rok_prod From Samochody,Wypożyczenia,Klienci where Klienci.id_klienta = Wypożyczenia.id_użytkownika AND Samochody.id_samochodu = Wypożyczenia.id_samochodu AND Klienci.pesel = @pesel";
            sql.Parameters.AddWithValue("@pesel", Haslo);
            SqlDataReader reader = sql.ExecuteReader();


            while (reader.Read())
            {
                lista.Add(new Wypożyczenia()
                {
                    Marka = Convert.ToString(reader["Marka"]),
                    Model = Convert.ToString(reader["Model"]),
                    Poj_silnika = Convert.ToString(reader["Poj_silnika"]),
                    Rok_prod = Convert.ToString(reader["Rok_prod"])
                });
            }
            reader.Close();

            InitializeComponent();

            GridGl.Columns.Add(new DataGridTextColumn() { Header = "Marka", Binding = new Binding("Marka") });
            GridGl.Columns.Add(new DataGridTextColumn() { Header = "Model", Binding = new Binding("Model") });
            GridGl.Columns.Add(new DataGridTextColumn() { Header = "Poj_silnika", Binding = new Binding("Poj_silnika") });
            GridGl.Columns.Add(new DataGridTextColumn() { Header = "Rok_prod", Binding = new Binding("Rok_prod") });

            GridGl.AutoGenerateColumns = false;
            GridGl.ItemsSource = lista;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
