using MySql.Data.MySqlClient;
using PRAKTIKA1.Models;
using System.Windows;
namespace PRAKTIKA1
{
    internal class DbService
    {
        // Строка подключения к базе данных MySQL
        // Содержит все параметры: сервер (у вас такой же), БД (tompsons_studN - N номер по журналу - 01, 04, 13...), логин (как имя БД), пароль (ваш) и кодировку
        public static string connectionString = "server=tompsons.beget.tech;user=tompsons_stud01;database=tompsons_stud01;password=1339Roma;CharSet=utf8mb4;";

        // Метод для создания и открытия подключения к БД
        public static MySqlConnection GetConnection()
        {
            // Новый объект подключения с использованием строки подключения
            MySqlConnection connection = new MySqlConnection(connectionString);
            // Открываем соединение с базой данных
            connection.Open();
            // Возвращаем готовое подключение
            return connection;
        }
        // Универсальный метод для выполнения SQL-запросов, которые НЕ возвращают данные
        // (INSERT, UPDATE, DELETE) с использованием параметров для безопасности
        public static int ExecuteNonQueryWithParameters(string query, Dictionary<string, object> parameters)
        {
            // Количество затронутых строк
            int rowsAffected = 0;
            // using обеспечивает автоматическое закрытие соединения и команды
            using (MySqlConnection connection = GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Добавляем параметры в команду для защиты от SQL-инъекций
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                // Выполняем запрос и получаем количество обработанных строк
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
        // Универсальный метод для получения данных с возможностью преобразования
        public static List<T> GetData<T>(string query, Func<MySqlDataReader, T> mapFunction)
        {
            // Список для хранения результата
            List<T> data = new List<T>();
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Читаем построчно, пока есть данные
                    while (reader.Read())
                    {
                        // Преобразуем текущую строку в объект типа T с помощью переданной функции
                        data.Add(mapFunction(reader));
                    }
                }
            }
            return data;
        }
        // Метод для выполнения скалярных запросов (возвращающих одно значение)
        // Например: SELECT COUNT(*) FROM table
        public static object ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            object result = null;
            using (MySqlConnection connection = GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                result = command.ExecuteScalar();
            }
            return result;
        }
        // Метод для аутентификации пользователя по логину и паролю
        public static aregistr AuthenticateUser(string name, string password)
        {
            try
            {
                string query = "SELECT * FROM aregistr WHERE name = @name AND password = @password";

                using (MySqlConnection connection = GetConnection())
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new aregistr
                            {
                                id = reader.GetInt32("id"),
                                name = reader.GetString("name"),
                                phone = reader.GetInt32("phone"),
                                email = reader.GetString("email"),
                                password = reader.GetString("password"),
                                Role = reader.GetString("Role")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка аутентификации: {ex.Message}");
            }

            return null;
        }
        // Метод для регистрации нового пользователя
        public static bool RegisterUser(aregistr newUser)
        {
            try
            {
                // Регистрация нового пользователя
                string insertQuery = @"INSERT INTO aregistr (name, secondname, phone, Mail, password, Role) 
                                VALUES (@name, @secondname, @phone, @email, @password, @Role)";

                var parameters = new Dictionary<string, object>
                {
                    ["@name"] = newUser.name,
                    ["@secondname"] = newUser.secondname,
                    ["@phone"] = newUser.phone,
                    ["@email"] = newUser.email,
                    ["@password"] = newUser.password,
                    ["@Role"] = "user", // Все новые пользователи получают роль 'user'
                };

                ExecuteNonQueryWithParameters(insertQuery, parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
                return false;
            }
        }
        // Метод для получения всех пользователей из БД
        public static List<aregistr> GetAllUsers()
        {
            string query = "SELECT * FROM aregistr ORDER BY id";

            // Используем универсальный метод GetData с лямбда-выражением для создания объектов User
            return GetData(query, reader => new aregistr
            {
                id = reader.GetInt32("id"),
                name = reader.GetString("name"),
                secondname = reader.GetString("secondname"),
                email = reader.GetString("email"),
                password = reader.GetString("password"),
                Role = reader.GetString("Role")
            });
        }
        
        //Метод для обновления данных пользователя
        public static bool UpdateUser(aregistr user)
        {
            try
            {
                string query = @"UPDATE aregistr 
                        SET name = @name, 
                            secondname = @secondname,
                            email = @email, 
                            password = @password,
                            Role = @Role 
                        WHERE Id = @Id";

                var parameters = new Dictionary<string, object>
                {
                    ["@id"] = user.id,
                    ["@name"] = user.name,
                    ["@secondname"] = user.secondname,
                    ["@email"] = user.email,
                    ["@password"] = user.password,
                    ["@Role"] = user.Role,
                };

                ExecuteNonQueryWithParameters(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления пользователя: {ex.Message}");
                return false;
            }
        }
        // Метод для удаления пользователя по ID
        public static bool DeleteUser(int userId)
        {
            try
            {
                string query = "DELETE FROM aregistr WHERE id = @id";
                var parameters = new Dictionary<string, object>
                {
                    ["@Id"] = userId
                };
                ExecuteNonQueryWithParameters(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления пользователя: {ex.Message}");
                return false;
            }
        }
    }
}
