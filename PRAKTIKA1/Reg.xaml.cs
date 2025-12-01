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
    }
}
