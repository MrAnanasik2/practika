using PRAKTIKA1.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PRAKTIKA1
{
    public partial class Reg : Window
    {
        public ObservableCollection<Models.Money> Monies { get; set; }
        public Reg()
        {
            InitializeComponent();
            var MonyList = DbService.GetMonies();

            Monies = new ObservableCollection<Models.Money> { };

            foreach (var mony in MonyList)
            {
                Monies.Add(mony);
            }

            DataContext = this;
        }

        // Обработчик события нажатия на кнопку "Войти"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем логин из текстового поля и удаляем лишние пробелы
            string login = Логин.Text.Trim();
            // Получаем пароль из поля для пароля (PasswordBox скрывает ввод)
            string password = Пароль.Password;

            // Валидация ввода: проверяем, что оба поля заполнены
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            // Вызываем метод аутентификации из сервиса базы данных
            // DbService.AuthenticateUser проверяет логин/пароль в базе данных
            var user = DbService.AuthenticateUser(login, password);

            // Проверяем результат аутентификации
            if (user != null)
            {
                // Если пользователь найден (аутентификация успешна):

                // Открываем окно, соответствующее роли пользователя
                    OpenRoleWindow(user);
                // скрываем, но не уничтожаем
                // Окно невидимо, но продолжает существовать в памяти
                // Можно вернуться к нему позже (например, при разлогине)
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }


        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLoginPlaceholder();
            UpdatePasswordPlaceholder();
            UpdateEmailPlaceholder();
        }

        private void Перелётики_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(null);
            main.Show();
            this.Close();
        }

        private void Рег_Click(object sender, RoutedEventArgs e)
        
            {
                // Создаем экземпляр окна регистрации
                var registerWindow = new Reg();

                // Устанавливаем текущее окно как владельца окна регистрации
                // Это обеспечивает модальность окна (блокирует родительское окно)
                registerWindow.Owner = this;

                // Открываем окно регистрации в модальном режиме
                // ShowDialog() блокирует выполнение кода до закрытия окна
                registerWindow.ShowDialog();
            }
        

        // Метод для открытия окна в зависимости от роли пользователя
        private void OpenRoleWindow(autorize user)
        {
            Window roleWindow = null; // Переменная для хранения ссылки на окно

            // Определяем какое окно открыть на основе роли пользователя
            switch (user.Role.ToLower()) // Приводим роль к нижнему регистру для унификации
            {
                case "admin":
                    // Для администратора открываем AdminWindow
                    roleWindow = new Adminka(user);
                    Adminka adminka = new Adminka(user);
                    
                    adminka.Show();
                    this.Close();
                    break;

                case "editor":
                    // Для редактора открываем EditorWindow
                    roleWindow = new Adminka(user);

                    break;

                default: // user или любая другая роль
                    // Для обычного пользователя открываем UserWindow
                    roleWindow = new MainWindow(user);
                    break;
            }

            // Если окно успешно создано
            if (roleWindow != null)
            {
                // Отображаем окно (не модально, позволяет работать с другими окнами)
                roleWindow.Show();

                // Подписываемся на событие закрытия окна роли
                roleWindow.Closed += (s, args) => this.Close();
                // Когда окно роли закрывается, закрывается и главное окно (и приложение)
                // Лямбда-выражение создает обработчик события, который вызывает Close() для MainWindow
            }
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
           
        }

        private void tip_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tip_mony_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
        }
        
    }

}
