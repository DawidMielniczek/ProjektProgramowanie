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
    /// Logika interakcji dla klasy MenuU.xaml
    /// </summary>
    /// 
  
    public partial class MenuU : Window
    {
        public string Haslo { get; set; }
        public MenuU()
        {
            InitializeComponent();
            
        }
        public MenuU(string haslo)
        {
            InitializeComponent();
            Haslo = haslo;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e) // Wylogowywanie 
        {
            DialogResult = false;
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Opinia
        {
            var Opinia = new Opinia(Haslo);
           

            if (Opinia.ShowDialog() == true)
            {
                MessageBox.Show("Dodano opinię! ");
            }



        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var AktualnieWyp = new AktualnieWyp(Haslo);
            AktualnieWyp.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var DostępneAuta = new DostępneAuta();
            DostępneAuta.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var Wypożycz = new Wypożycz(Haslo);
            if(Wypożycz.ShowDialog() == true)
            {
                MessageBox.Show("Wypożyczyłeś samochód!");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var Oddaj = new Oddaj(Haslo);
            if (Oddaj.ShowDialog() == true)
            {
                MessageBox.Show("Oddałeś samochód");
            }
        }
    }
}
