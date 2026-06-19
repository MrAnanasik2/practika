using PRAKTIKA1.Models;
using System.Windows;

namespace PRAKTIKA1
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string secondname = SecondNameBox.Text.Trim();
            int phone = PhoneBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(secondname) ||
                int.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                MsgText.Text = "Заполните все поля";
                return;
            }

            aregistr user = new aregistr
            {
                name = name,
                secondname = secondname,
                phone = phone,
                email = email,
                password = password,
                Role = "user"
            };

            bool success = DbService.RegisterUser(user);

            if (success)
            {
                MsgText.Text = "Успешная регистрация";

                MainWindow main = new MainWindow(user);
                main.Show();
                this.Close();
            }
            else
            {
                MsgText.Text = "Ошибка регистрации";
            }
        }

        private void GoLogin_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}