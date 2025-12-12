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
        public ObservableCollection<Country> Countrys { get; set; }
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




            var CountryList = DbService.GetCountry();

            Countrys = new ObservableCollection<Country> { };

            foreach (var contry in CountryList)
            {
                Countrys.Add(contry);
            }

            DataContext = this;
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
            // Получаем логин из текстового поля и удаляем лишние пробелы
            string login = Логин.Text.Trim();
            // Получаем пароль из поля для пароля (PasswordBox скрывает ввод)
            string password = Пароль.Password;
            string email = Email.Text.Trim(null);
            var selectedMoney = tip_mony.SelectedItem as Models.Money;
            var selectedCountry = tip_country.SelectedItem as Country;



            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Введите все данные");
                return;
            }
            else {
                autorize newUser = new autorize
                {
                    Login = login,
                    Password = password,
                    Email = email,
                    Money_id = selectedMoney.Mony_id,
                    Country_id = selectedCountry.Country_id,

                };
            bool success = DbService.RegisterUser(newUser);
                MainWindow main = new MainWindow(newUser);
                main.Show();
                this.Close();
            }
        }

         private void Уже_есть_Click(object sender, RoutedEventArgs e)
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

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
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
            Email.Focus();
            Email.CaretIndex = Email.Text.Length;
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
            PlaceholderEmail.Visibility = string.IsNullOrEmpty(Email.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

       

        private void tip_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tip_mony_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
        }

        private void tip_country_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
