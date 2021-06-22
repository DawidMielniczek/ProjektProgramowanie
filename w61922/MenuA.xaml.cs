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

namespace w61922
{
    /// <summary>
    /// Logika interakcji dla klasy MenuA.xaml
    /// </summary>
    public partial class MenuA : Window
    {
        public string Haslo { get; set; }
        public MenuA(string haslo)
        {
            InitializeComponent();
            Haslo = haslo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var DodajA = new DodajA();
            if (DodajA.ShowDialog() == true)
            {
                MessageBox.Show("Dodano Samochód!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var UsuńS = new UsuńS();
            if (UsuńS.ShowDialog() == true)
            {
                MessageBox.Show("Usunłąeś samochód z wypożyczalni!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //usuwanie
            var UsuńU = new UsuńU();
            if(UsuńU.ShowDialog()== true)
            {
                MessageBox.Show("Usunąłeś danego Klienta.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var OpinieA = new OpinieA();
            OpinieA.Show();
        }
    }
}
