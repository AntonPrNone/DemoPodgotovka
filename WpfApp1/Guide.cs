using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    static partial class DB_Management
    {
        private static SqlConnection _connection;
        private const string _connectionString = "Data Source=DESKTOP-JVGFIMM;Initial Catalog=Test;Integrated Security=True;Encrypt=False";

        public static void OpenConnection() // Условия if можно убрать при единоразовом вызове метода
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        public static List<User> GetUsers() // Конкр. выборка: в конец запроса добавить WHERE и замена while на if
        {
            string query = "SELECT ID, Name, Age FROM Users";
            SqlCommand command = new SqlCommand(query, _connection);

            List<User> users = new List<User>();

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"].ToString());
                    string name = reader["Name"].ToString();
                    int age = Convert.ToInt32(reader["Age"].ToString());

                    users.Add(new User { Id = id, Name = name, Age = age });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            return users;
        }

        public static void AddUser(string name, int age)
        {
            string query = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";

            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", age);

            try
            {
                command.ExecuteNonQuery(); // Добавить return для обработки успешности запроса
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public static void DeleteUser(int userId)
        {
            string query = "DELETE FROM Users WHERE ID = @id";

            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", userId);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public static void UpdateUser(User user)
        {
            string query = "UPDATE Users SET Name = @NewName, Age = @NewAge WHERE ID = @Id";

            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@NewName", user.Name);
            command.Parameters.AddWithValue("@NewAge", user.Age);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
    class Guide
    {

    }
}
