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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;


namespace w61922
{
    /// <summary>
    /// Logika interakcji dla klasy UsuńU.xaml
    /// </summary>
    public partial class UsuńU : Window
    {
       
        public UsuńU()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pesel = TbPe.Text;
            if (!Regex.IsMatch(TbPe.Text, @"^[0-9]{11,11}$"))
            {
                MessageBox.Show("Pesel jest w nieprawdiłowej formie.");
                return;
            }
            else
            {
                string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand sql = connection.CreateCommand();
                sql.CommandText = @"Delete from Klienci where pesel= @Pesel";
                sql.Parameters.AddWithValue("@Pesel", pesel);
                int reslt = sql.ExecuteNonQuery();
                if (reslt > 0)
                {
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nie ma Klienta o takich danych");
                    this.Close();
                }
            }
        }
    }
}
