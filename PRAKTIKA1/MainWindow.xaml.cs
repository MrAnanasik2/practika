using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;

namespace PRAKTIKA1
{
    public partial class MainWindow : Window
    {
        private List<string> cities = new List<string>() { "Москва", "Санкт-Петербург", "Сочи" };

        public MainWindow()
        {
            InitializeComponent();

            Откуда.ItemsSource = cities;
            Пассажир.ItemsSource = Enumerable.Range(1, 5).ToList();
            Качество.ItemsSource = new List<string>() { "Эконом", "Комфорт", "Бизнес", "Первый класс" };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Откуда_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Откуда.SelectedItem != null)
            {
                Куда.ItemsSource = cities.Where(c => c != Откуда.SelectedItem.ToString()).ToList();
            }
        }

        private void Куда_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Куда_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Пассажир_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Качество_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
