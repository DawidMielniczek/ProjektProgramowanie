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
    /// Logika interakcji dla klasy Opinia.xaml
    /// </summary>
    public partial class Opinia : Window
    {
        public string Haslo { get; set; }
        public Opinia()
        {
      
            InitializeComponent();
        }
        public Opinia(string haslo)
        {
            InitializeComponent();
            Haslo = haslo;
        }

        private void CbOcena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (TbOpinia.MaxLength < 150)
            {
                string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sql = connection.CreateCommand();
                sql.CommandText = @"SELECT id_klienta from Klienci where pesel = @pesel";
                sql.Parameters.AddWithValue("@pesel", Haslo);
                SqlDataReader reader1 = sql.ExecuteReader();
                reader1.Read();
                
                    var Nr = reader1["id_klienta"].ToString();
                    reader1.Close();
                

                var Opinia = TbOpinia.Text;
                
                
                var gwiazdka = "";
                if (CbOcena.SelectedIndex == 0)
                {
                    gwiazdka = "1";
                }
                    else if (CbOcena.SelectedIndex == 1)
                    {
                        gwiazdka = "2";
                    }
                    else if (CbOcena.SelectedIndex == 2)
                    {
                        gwiazdka = "3";
                    }
                   else if (CbOcena.SelectedIndex == 3)
                    {
                        gwiazdka = "4";
                    }
                   else if (CbOcena.SelectedIndex == 4)
                    {
                        gwiazdka = "5";
                    }
                else
                {
                    MessageBox.Show("Nie Wybrano oceny! ", "Uwaga", MessageBoxButton.OK);
                    DialogResult = false;
                }
                if (gwiazdka != "")
                {

                    SqlCommand sql1 = connection.CreateCommand();
                    sql1.CommandText = @"INSERT INTO[dbo].[Wypożyczalnia]
                    ([id_klienta]
                      ,[Opinia],
                        [Ocena]
                       )
                    VALUES
                       (
                        @Id,
                        @Opinia,
                        @Ocena
                        )";
                    sql1.Parameters.AddWithValue("@Id", Nr);
                    sql1.Parameters.AddWithValue("@Opinia", Opinia);
                    sql1.Parameters.AddWithValue("@Ocena", gwiazdka);
                    if (sql1.ExecuteNonQuery() == 1)
                    {
                        DialogResult = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Twoja opinia była zbyt długa. ");
                DialogResult = false;
            }
        }
    }
}
