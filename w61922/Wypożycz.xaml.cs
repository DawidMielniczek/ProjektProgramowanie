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
using System.Runtime;


namespace w61922
{
    /// <summary>
    /// Logika interakcji dla klasy Wypożycz.xaml
    /// </summary>
    public partial class Wypożycz : Window
    {
        public class Wypożyczenie
        {
            public int Id_samochodu { get; set; }
            public string Marka { get; set; }
            public string Model { get; set; }
            public string Poj_silnika { get; set; }
            public string Rok_prod { get; set; }
        }
        List<Wypożyczenie> list { get; set; }
        public Wypożyczenie WypożyczS = new Wypożyczenie();
        public string Haslo { get; set; }
        public Wypożycz(string haslo)
        {
            Haslo = haslo;
            list = new List<Wypożyczenie>();
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();
            sql.CommandText = @"Select id_samochodu,Marka,Model,Poj_silnika, Rok_prod From Samochody where Dostępność = 'tak'";
            SqlDataReader reader = sql.ExecuteReader();


            while (reader.Read())
            {
                list.Add(new Wypożyczenie()
                {
                    Id_samochodu = (int)reader["id_samochodu"],
                    Marka = Convert.ToString(reader["Marka"]),
                    Model = Convert.ToString(reader["Model"]),
                    Poj_silnika = Convert.ToString(reader["Poj_silnika"]),
                    Rok_prod = Convert.ToString(reader["Rok_prod"])
                });
            }
            reader.Close();
            InitializeComponent();

            DgWypożycz.Columns.Add(new DataGridTextColumn() { Header = "Id_samochodu", Binding = new Binding("Id_samochodu") });
            DgWypożycz.Columns.Add(new DataGridTextColumn() { Header = "Marka", Binding = new Binding("Marka") });
            DgWypożycz.Columns.Add(new DataGridTextColumn() { Header = "Model", Binding = new Binding("Model") });
            DgWypożycz.Columns.Add(new DataGridTextColumn() { Header = "Poj_silnika", Binding = new Binding("Poj_silnika") });
            DgWypożycz.Columns.Add(new DataGridTextColumn() { Header = "Rok_prod", Binding = new Binding("Rok_prod") });

            DgWypożycz.AutoGenerateColumns = false;
            DgWypożycz.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand Numer = connection.CreateCommand();
            Numer.CommandText = @"Select id_klienta from Klienci where pesel = @pesel";
            Numer.Parameters.AddWithValue("@pesel", Haslo);
            SqlDataReader reader = Numer.ExecuteReader();
            reader.Read();
            var IdU = (int)reader["id_klienta"];
             reader.Close();

            
            var nr = TbWyb.Text;
            var marka = TbWybM.Text;
            var Data = DateTime.Now;


            if (!Regex.IsMatch(TbWyb.Text, @"^[0-9]{1,4}$") ||
                !Regex.IsMatch(TbWybM.Text, @"^\p{Lu}\p{Ll}{1,12}$"))
            {
                MessageBox.Show("Nie prawidłowe dane o samochdozie!");
                return;
            }
            else
            {

                SqlCommand Sql = connection.CreateCommand();
                Sql.CommandText = @"Update Samochody set Dostępność = 'nie' where id_samochodu = @Nr AND Marka = @marka";
                Sql.Parameters.AddWithValue("@Nr", nr);
                Sql.Parameters.AddWithValue("@marka", marka);
                Sql.ExecuteNonQuery();

                SqlCommand sql1 = connection.CreateCommand();
                Sql.CommandText = @"INSERT INTO[dbo].[Wypożyczenia]
                ([id_użytkownika]
                  ,[id_samochodu]
                  ,[data_wyp]
                   )
                VALUES
                   (
                    @IdU,
                    @IdS,
                    @Data_wyp
                    )";
                Sql.Parameters.AddWithValue("@IdU", IdU);
                Sql.Parameters.AddWithValue("@IdS", nr);
                Sql.Parameters.AddWithValue("@Data_wyp",Data);
                Sql.ExecuteNonQuery();
                DialogResult = true;
            }
        }

        private void TbWyb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DgWypożycz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TbWybM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

