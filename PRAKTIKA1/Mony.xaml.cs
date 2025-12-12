using PRAKTIKA1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRAKTIKA1
{
    /// <summary>
    /// Логика взаимодействия для Mony.xaml
    /// </summary>
    public partial class Mony : Page
    {
        public ObservableCollection<Models.Money> Monies { get; set; }
        public ObservableCollection<Country> Countrys { get; set; }
        public Mony()
        {
            InitializeComponent();
            var MonyList = DbService.GetMonies();

            Monies = new ObservableCollection<Models.Money> { };

            foreach (var mony in MonyList)
            {
                Monies.Add(mony);
            }

            DataContext = this;




            var CountryList = DbService.GetCountry();

            Countrys = new ObservableCollection<Country> { };

            foreach (var contry in CountryList)
            {
                Countrys.Add(contry);
            }

            DataContext = this;
        }

        private void denga_Click(object sender, RoutedEventArgs e)
        {

        }

        private void country_Click(object sender, RoutedEventArgs e)
        {

        }

        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            // Попробуем сначала через NavigationService
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
                return;
            }

            // Если страница размещена напрямую в Window (через Window.Content = new more())
            Window hostWindow = Window.GetWindow(this);
            if (hostWindow != null)
            {
                if (hostWindow.Content == this)
                {
                    hostWindow.Close();
                    return;
                }

                // Попробуем найти Frame с именем "M" в хост-окне и очистить его содержимое
                var frame = hostWindow.FindName("M") as Frame;
                if (frame != null)
                {
                    if (frame.Content == this)
                    {
                        frame.Content = null;
                        frame.Visibility = Visibility.Collapsed;
                        return;
                    }
                    try
                    {
                        frame.Navigate(null);
                        frame.Visibility = Visibility.Collapsed;
                        return;
                    }
                    catch { }
                }
            }

            // Если ничего не помогло — сообщим об этом пользователю
            MessageBox.Show("Не удалось закрыть страницу more.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
