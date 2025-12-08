using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PRAKTIKA1
{
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLoginPlaceholder();
            UpdatePasswordPlaceholder();
            UpdateEmailPlaceholder();
        }

        private void Перелётики_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Рег_Click(object sender, RoutedEventArgs e)
        {
            string login = Логин.Text.Trim().ToLower();
            string password = Пароль.Password.Trim();

            // Пароль обязателен
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Существующие аккаунты
            string[] exists = { "admin", "pilot", "stuard", "gruzchik" };

            if (exists.Contains(login))
            {
                MessageBox.Show("Аккаунт уже существует!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Логин_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLoginPlaceholder();
        }

        private void Пароль_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholder();
        }

        private void E_mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateEmailPlaceholder();
        }

        private void PlaceholderLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Логин.Focus();
            Логин.CaretIndex = Логин.Text.Length;
        }

        private void PlaceholderPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Пароль.Focus();
        }

        private void PlaceholderEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            E_mail.Focus();
            E_mail.CaretIndex = E_mail.Text.Length;
        }

        private void UpdateLoginPlaceholder()
        {
            PlaceholderLogin.Visibility = string.IsNullOrEmpty(Логин.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void UpdatePasswordPlaceholder()
        {
            PlaceholderPassword.Visibility = string.IsNullOrEmpty(Пароль.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void UpdateEmailPlaceholder()
        {
            PlaceholderEmail.Visibility = string.IsNullOrEmpty(E_mail.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void Уже_есть_Click(object sender, RoutedEventArgs e)
        {
            string login = Логин.Text.Trim().ToLower();
            string password = Пароль.Password.Trim();

            // Логины, для которых нужен пароль
            string[] allowedUsers = { "admin", "pilot", "stuard", "gruzchik" };

            if (allowedUsers.Contains(login))
            {
                // Если пароль не введён
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка пароля
                if (password == "123")
                {
                    Adminka adm = new Adminka();
                    adm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // ВСЕ остальные логины → без проверки пароля
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
    
}
