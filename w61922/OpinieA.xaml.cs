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
    /// Logika interakcji dla klasy OpinieA.xaml
    /// </summary>
    /// 
    public class OpiniaA
    {
        public int id_opini { get; set; }
        public int id_klienta { get; set; }
        public string Opinia { get; set; }
        public string Ocena { get; set; }
    }
    public partial class OpinieA : Window
    {
        List<OpiniaA> list = new List<OpiniaA>();
        public OpinieA()
        {
            list = new List<OpiniaA>();
            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();
            sql.CommandText = @"Select * From Wypożyczalnia";
            SqlDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new OpiniaA()
                {
                    id_opini = (int)reader["id_opini"],
                    id_klienta = (int)reader["id_klienta"],
                    Opinia = reader["Opinia"].ToString(),
                    Ocena = reader["Ocena"].ToString()
                }); 
            }
            reader.Close();

            InitializeComponent();

            DgOpinie.Columns.Add(new DataGridTextColumn() { Header = "id_opini", Binding = new Binding("id_opini") });
            DgOpinie.Columns.Add(new DataGridTextColumn() { Header = "id_klienta", Binding = new Binding("id_klienta") });
            DgOpinie.Columns.Add(new DataGridTextColumn() { Header = "Opinia", Binding = new Binding("Opinia") });
            DgOpinie.Columns.Add(new DataGridTextColumn() { Header = "Ocena", Binding = new Binding("Ocena") });

            DgOpinie.AutoGenerateColumns = false;
            DgOpinie.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
