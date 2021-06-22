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
    /// Logika interakcji dla klasy DostępneAuta.xaml
    /// </summary>
    /// 
    class Wypożyczenia
    {
        public int Id_samochodu { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Poj_silnika { get; set; }
        public string Rok_prod { get; set; }

        public Wypożyczenia(string marka, string model, string Poj_silnika, string Rok_prod)
        {
            this.Marka = marka;
            this.Model = model;
            this.Poj_silnika = Poj_silnika;
            this.Rok_prod = Rok_prod;
        }
        public Wypożyczenia()
        {


        }
    }
        public partial class DostępneAuta : Window
    {
          List <Wypożyczenia> list { get; set; }

        public DostępneAuta()
        {
            list = new List<Wypożyczenia>();
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();
            sql.CommandText = @"Select Marka,Model,Poj_silnika, Rok_prod From Samochody where Dostępność = 'tak'";
            SqlDataReader reader = sql.ExecuteReader();
           
               
            while (reader.Read())
            {
                list.Add(new Wypożyczenia()     
                {
                   Marka = Convert.ToString( reader["Marka"]),
                   Model = Convert.ToString(reader["Model"]),
                   Poj_silnika = Convert.ToString(reader["Poj_silnika"]),
                   Rok_prod = Convert.ToString(reader["Rok_prod"])
                });
            }
            reader.Close();
            InitializeComponent();

            
            DgWyp.Columns.Add(new DataGridTextColumn() { Header = "Marka", Binding = new Binding("Marka") });
            DgWyp.Columns.Add(new DataGridTextColumn() { Header = "Model", Binding = new Binding("Model") });
            DgWyp.Columns.Add(new DataGridTextColumn() { Header = "Poj_silnika", Binding = new Binding("Poj_silnika") });
            DgWyp.Columns.Add(new DataGridTextColumn() { Header = "Rok_prod", Binding = new Binding("Rok_prod") });

            DgWyp.AutoGenerateColumns = false;
            DgWyp.ItemsSource = list;
        }




        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
    
}
