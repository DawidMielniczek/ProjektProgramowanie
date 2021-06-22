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
    /// Logika interakcji dla klasy Logowanie.xaml
    /// </summary>
    
    
   

    
    public partial class Logowanie : Window
    {
       
        public Logowanie()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

          
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            var login = TBL.Text;
            var Haslo = TbH.Text;

            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();
            sql.CommandText = @"Select Imie, pesel From Klienci";

            SqlDataReader reader = sql.ExecuteReader();

            bool Poprawna = false;

            while (reader.Read())
            {

                if (reader["imie"].ToString().Trim() == login && reader["Pesel"].ToString().Trim() == Haslo)
                {
                    reader.Close();
                    Poprawna = true;
                    DialogResult = true;
                    var MenuU = new MenuU(Haslo);
                    MenuU.ShowDialog();
                    break;
                }

            }
            if (!Poprawna)
            {
                MessageBox.Show("Login lub hasło jest niepoprawne. ");
                reader.Close();
                DialogResult = false;
             }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = TBL.Text;
            var Haslo = TbH.Text;

            string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sql = connection.CreateCommand();
            sql.CommandText = @"Select Imie, pesel From Pracownicy";

            SqlDataReader reader = sql.ExecuteReader();

            bool Poprawna = false;

            while (reader.Read())
            {

                if (reader["imie"].ToString().Trim() == login && reader["Pesel"].ToString().Trim() == Haslo)
                {
                    reader.Close();
                    Poprawna = true;
                    DialogResult = true;
                    var MenuA = new MenuA(Haslo);
                    MenuA.ShowDialog();
                    break;
                }

            }
            if (!Poprawna)
            {
                MessageBox.Show("Login lub hasło jest niepoprawne. ");
                reader.Close();
                DialogResult = false;
            }

        }
    }
}
