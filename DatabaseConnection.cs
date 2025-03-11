using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Windows;

namespace userDataBase_ADO
{
    public class DatabaseConnection
    {
        
        private string m_connectionString;
        private int m_userId;
        public int UserId
        {
            get { return m_userId; }
        }

        public DatabaseConnection()
        {
            m_connectionString = "Server=localhost; Database=Users; Integrated Security=True; TrustServerCertificate=True;";
        }
        public DatabaseConnection(in string dataBase) {
            m_connectionString = $"Server=localhost; Database={dataBase}; Integrated Security=True; TrustServerCertificate=True;";
        }

        public void Connect()
        {
            using(var connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Подключение к базе данных успешно установлено.");

                    connection.Close();
                } catch(Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                }
            }
        }


        //2.получите список всех таблиц в базе данных с помощью INFORMATION_SCHEMA.TABLES
        //3.выведите названия таблиц в консоль
        public void GetTables()
        {
            using(var connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    connection.Open();                    
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                //Console.ForegroundColor = ConsoleColor.Green;
                                //Console.WriteLine(reader.GetString(0));
                                MessageBox.Show(reader.GetString(0));
                            }
                            //Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }

                } catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }


        }

        //проверка логина и пароля
        public bool CheckData(in string login, in string pass)
        {
            

            using(var connection = new SqlConnection(m_connectionString))
            {
                bool isFound = false;
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM Datas";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                              
                                //проверяю, есть ли в базе данных такой логин и пароль (обрезаю пробелы)
                                if(reader.GetString(1).Trim() == login && reader.GetString(2).Trim() == pass)
                                {
                                    isFound = true;
                                    m_userId = reader.GetInt32(0);
                                    break;
                                }
                            }
                            
                        }
                    }
                } catch(Exception ex)
                {

                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                    
                }
                return isFound;
            }
        }

        //получить количество полей в таблице (нужна для правильной вставки)
        private int GetColumnCount(in string tableName)
        {
            using(var connection = new SqlConnection(m_connectionString))
            {
                int columnCount;
                try
                {
                    connection.Open();
                    string query = $"SELECT COUNT(COLUMN_NAME) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        columnCount = (int)command.ExecuteScalar();
                        //Console.WriteLine($"Количество полей в таблице {tableName}: {columnCount}");
                    }
                    return columnCount;
                } catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                return -1;
            }
        }
        
        //вставить строку в таблицу
        public void InsertRow(in string login, in string pass)
        {
            using(var connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    
                    string query = "INSERT INTO Datas (login, password) VALUES(@login, @password)";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", pass);
                        command.ExecuteNonQuery();
                    }
                    
                    connection.Close();
                    
                } catch(Exception ex)
                {
                    
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                    
                }
            }
        }

        // функция для записи изображения в базу данных
        public void SaveImageToDatabase(in string filePath, in int userId)
        {
            
            if(!File.Exists(filePath))
            {
                Console.WriteLine("Ошибка: файл не найден.");
                return;
            }

            try
            {
                byte[] imageData = File.ReadAllBytes(filePath);  // считываем файл в массив байтов

                using(var connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();

                    // запрос на добавление данных в таблицу 

                    string query = "UPDATE Datas SET profilePicture = (@profilePicture) WHERE (id = @userID)";

                    using(var command = new SqlCommand(query, connection))
                    {
                        // параметры для запроса
                        //command.Parameters.AddWithValue("@FilePath", filePath);  // путь к файлу
                        command.Parameters.AddWithValue("@userID", userId);
                        command.Parameters.AddWithValue("@profilePicture", imageData);  // двоичные данные изображения

                        // выполнение запроса
                        command.ExecuteNonQuery();
                        MessageBox.Show("Изображение успешно сохранено в базу данных.");
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изображения в базу: " + ex.Message);
            }
        }

        // функция для чтения изображения из базы данных
        public string ReadImageFromDatabase(in int imageId)
        {
            // путь, куда сохранить извлечённое изображение
            string filePath = $".\\image{imageId}.png";

            try
            {
                using(var connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();

                    // запрос для получения изображения по ID
                    string query = "SELECT profilePicture FROM Datas WHERE id = @ImageId";

                    using(var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageId", imageId);

                        using(var reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                byte[] imageData = (byte[])reader["profilePicture"];

                                // сохраняем файл обратно в файловую систему
                                File.WriteAllBytes(filePath, imageData);
                                MessageBox.Show($"Изображение извлечено и сохранено по пути: {filePath}");
                            } else
                            {
                                Console.WriteLine("Изображение с таким ID не найдено.");
                            }
                        }
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine("Ошибка при извлечении изображения из базы: " + ex.Message);
            }

            return filePath;
        }

        //служебная фунция для получения названия поля по индексу
        private string GetColumnNameByIndex(in string tableName, int index)
        {
            using(var connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM {tableName}";
                    
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(index >= 0 && index < reader.FieldCount)
                            {
                                return reader.GetName(index);
                            } else
                            {
                                throw new IndexOutOfRangeException("Индекс выходит за пределы количества полей в таблице.");
                            }
                        }
                    }
                } catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return null;
                }
            }
        }

        //обновить строку по id
        public void UpdateRowById(in string tableName)
        {
            
            using(var connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    
                    string query = $"UPDATE {tableName} SET ";
                    
                    Console.Write("Введите id строки: ");
                    
                    string id = Console.ReadLine();
                    
                    for(int i = 1; i < GetColumnCount(tableName); ++i)
                    {
                        Console.Write($"Введите новое значение {i}-го поля: ");
                        string value = Console.ReadLine();

                        query += $"{tableName}.{GetColumnNameByIndex(tableName, i)} = '{value}'";
                        
                        if(i < GetColumnCount(tableName) - 1)
                        {
                            query += ", ";
                        }
                    }
                    query += $" WHERE {tableName}.id = {id}";
                    
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                } catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }

        

    }
}
