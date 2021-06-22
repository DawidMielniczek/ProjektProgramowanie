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
    /// Logika interakcji dla klasy UsuńS.xaml
    /// </summary>
    public partial class UsuńS : Window
    {
       
        public UsuńS()
        {
            InitializeComponent();
        }

        private void TbId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Id = TbId.Text;
            if(!Regex.IsMatch(TbId.Text, @"^[0-9]{1,10}$"))
            {
                MessageBox.Show("Podano błedny Id");
                return;
            }
            else
            { 

                string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand sql = connection.CreateCommand();
                sql.CommandText = @"Delete from Samochody where id_samochodu= @Id";
                sql.Parameters.AddWithValue("@Id", Id);
                int reslt = sql.ExecuteNonQuery();
                if (reslt > 0)
                {
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nie ma Samochodu o podanym Identyfikatorze");
                    this.Close();
                }
            }
        }
    }
}
