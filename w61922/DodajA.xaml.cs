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
    /// Logika interakcji dla klasy DodajA.xaml
    /// </summary>
    public partial class DodajA : Window
    {
        public DodajA()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TbSil_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var marka = TbM.Text;
            var model = TbMod.Text;
            var poj = TbSil.Text;
            var rok = TbRok.Text;

            if (!Regex.IsMatch(TbM.Text, @"^\p{Lu}\p{Ll}{1,12}$") ||
                !Regex.IsMatch(TbMod.Text, @"^\p{Lu}\p{Ll}{1,12}$") ||
                !Regex.IsMatch(TbSil.Text, @"^[0-9]{4,12}$") ||
                !Regex.IsMatch(TbRok.Text, @"^[0-9]{4,4}$"))
            {
                MessageBox.Show("Podano błedne dane.");
                return;
            }
            else
            {

                string connectionString = @"Data source= DESKTOP-57VIT9O;database=Wypożyczalnia_samochodów;Trusted_Connection=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand sql = connection.CreateCommand();

                sql.CommandText = @"INSERT INTO[dbo].[Samochody]
                ([Marka]
                  ,[Model]
                  ,[Poj_silnika],
                    [Rok_prod],
                    [Dostępność]
                   )
                VALUES
                   (
                    @marka,
                    @model,
                    @Poj,
                    @Rok,
                    @Dos
                    )";

                sql.Parameters.AddWithValue("@marka", marka);
                sql.Parameters.AddWithValue("@model", model);
                sql.Parameters.AddWithValue("@poj", poj);
                sql.Parameters.AddWithValue("@rok", rok);
                sql.Parameters.AddWithValue("@Dos", "tak");
                sql.ExecuteNonQuery();
                DialogResult = true;
            }
        }
    }
}
