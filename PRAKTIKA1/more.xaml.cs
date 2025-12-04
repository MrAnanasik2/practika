using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PRAKTIKA1
{
    public partial class more : Page
    {
        public more()
        {
            InitializeComponent();
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

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();

            // Закрываем главное окно (а значит и страницу more)
            Window host = Window.GetWindow(this);
            if (host != null)
                host.Close();

        }
    }
}
