using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace DemoExamApp
{
    /// <summary>
    /// Слой доступа к данным MySQL.
    /// </summary>
    public class Database
    {
        private readonly string _connectionString;

        public Database()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["DemoExamConnection"]
                ?.ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("Строка подключения не найдена в App.config.");
        }

        /// <summary>
        /// Авторизация пользователя. Возвращает пользователя или null.
        /// </summary>
        public User AuthUser(string login, string password)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(
                "SELECT Id, Login, IsAdmin, IsBlocked FROM Users WHERE Login = @login AND Password = @password", conn))
            {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            IsAdmin = reader.GetBoolean(2),
                            IsBlocked = reader.GetBoolean(3)
                        };
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Блокирует пользователя после неудачных попыток капчи.
        /// </summary>
        public void BlockUser(int userId)
        {
            ExecuteNonQuery("UPDATE Users SET IsBlocked = 1 WHERE Id = @id",
                new MySqlParameter("@id", userId));
        }

        /// <summary>
        /// Возвращает список пользовательских таблиц базы данных.
        /// </summary>
        public List<string> GetTables()
        {
            var tables = new List<string>();
            const string query = @"SELECT TABLE_NAME 
                                   FROM INFORMATION_SCHEMA.TABLES 
                                   WHERE TABLE_TYPE = 'BASE TABLE' 
                                   AND TABLE_SCHEMA = DATABASE()
                                   ORDER BY TABLE_NAME";

            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
            }

            return tables;
        }

        /// <summary>
        /// Загружает данные таблицы в DataTable.
        /// </summary>
        public DataTable GetTableData(string tableName)
        {
            var table = new DataTable();
            string query = $"SELECT * FROM `{tableName}`";

            using (var conn = new MySqlConnection(_connectionString))
            using (var adapter = new MySqlDataAdapter(query, conn))
            {
                adapter.Fill(table);
            }

            return table;
        }

        /// <summary>
        /// Сохраняет изменения DataTable в базе данных.
        /// </summary>
        public int SaveTableData(string tableName, DataTable dataTable)
        {
            string query = $"SELECT * FROM `{tableName}`";

            using (var conn = new MySqlConnection(_connectionString))
            using (var adapter = new MySqlDataAdapter(query, conn))
            using (var builder = new MySqlCommandBuilder(adapter))
            {
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();

                return adapter.Update(dataTable);
            }
        }

        /// <summary>
        /// Выполняет SQL-запрос без возврата данных.
        /// </summary>
        public void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
