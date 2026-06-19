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
using System.Windows.Shapes;

namespace PRAKTIKA1
{
    public partial class Adminka : Window
    {
        public ObservableCollection<aregistr> aregistr { get; set; }
        private aregistr _currentUser;

        public Adminka(aregistr userHz)
        {
            InitializeComponent();

            var UserList = DbService.GetAllUsers();
            _currentUser = userHz;

            aregistr = new ObservableCollection<aregistr>();

            foreach (var user in UserList)
            {
                aregistr.Add(user);
            }

            DataContext = this;
        }

        private void Перелётики_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(null);
            main.Show();
            this.Close();
        }

        private void CBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        // ===========================
        //   ОБРАБОТЧИК УДАЛЕНИЯ
        // ===========================
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button?.Tag as aregistr;

            if (user == null)
                return;

            // Подтверждение удаления
            if (MessageBox.Show($"Удалить пользователя \"{user.name}\"?",
                                "Подтверждение",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            // ----------- ТУТ ТВОЙ КОД УДАЛЕНИЯ ИЗ БАЗЫ -----------
            // Например:
            DbService.DeleteUser(user.id);
            // ------------------------------------------------------

            // Удаляем из списка (UI обновится автоматически)
            aregistr.Remove(user);

            MessageBox.Show("Пользователь удалён.");
        }
    }
}
