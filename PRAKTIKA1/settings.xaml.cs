using PRAKTIKA1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PRAKTIKA1
{

    public partial class settings : Window
    {
        public ObservableCollection<Models.Money> Monies { get; set; }
        public ObservableCollection<Country> Countrys { get; set; }
        public ObservableCollection<autorize> obnova { get; set; }

        private autorize _user; // ← добавил текущего пользователя

        public settings()
        {
            InitializeComponent();

            var UserList = DbService.UpdateUser;
            obnova = new ObservableCollection<autorize> { new autorize() };




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

        private void Перелётики_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(null);
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
