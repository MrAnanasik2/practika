using System;
using System.Windows;
using System.Windows.Controls;

namespace PRAKTIKA1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


    private void Button_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map();
            map.Show();
            this.Close();
        }

        private void Откуда_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void Профиль_Click(object sender, RoutedEventArgs e)
        {
            P.Visibility = Visibility.Visible;
            P.Navigate(new more());
        }

        private void Mony_Click(object sender, RoutedEventArgs e)
        {
            M.Visibility = Visibility.Visible;
            M.Navigate(new Mony());
        }

        private void Перелётики_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            this.Close();
        }

        private void MoreHot_Click(object sender, RoutedEventArgs e)
        {
            hot h = new hot();
            h.Show();
            this.Close();
        }

        private void Поддержка_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://t.me/@Kepkochka", // <-- сюда вставь ссылку на поддержку
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть Telegram: " + ex.Message);
            }
        }
    }


}
