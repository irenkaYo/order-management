namespace OrderManagementSystem;
using Npgsql;
public class UsersRepository
{
    string connectionString =
        "Host=localhost;" +
        "Port=5432;" +
        "Database=order_management;" +
        "Username=postgres;" +
        "Password=nzkhf906Fjl;";
    
    public List<User> Users { get; private set; }

    public UsersRepository()
    {
        Users = new List<User>();
    }
    
    public List<User> GetAllUsers()
    {
        using var connection = OpenConnection();

        var sql = "SELECT id, name, email FROM users";

        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var email = reader.GetString(2);
            User user = new User(id, name, email);
            Users.Add(user);
        }   
        return Users;
    }

    public User GetUserById(int userId)
    {
        using var connection = OpenConnection();
        User user = null;
        var sql = "SELECT id, name, email FROM users WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("id", userId);
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var email = reader.GetString(2);

            user = new User(id, name, email);
        }
        return user;
    }

    public void AddUser(string name, string email)
    {
        using var connection = OpenConnection();
        var sql = "INSERT INTO users (name, email) VALUES (@name, @email)";
        using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("name", name);
        command.Parameters.AddWithValue("email", email);
        
        command.ExecuteNonQuery();
    }
    
    public NpgsqlConnection OpenConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    public void DeleteUser(int userId)
    {
        using var connection = OpenConnection();
        var sql = "DELETE FROM users WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", userId);
        command.ExecuteNonQuery();
    }
    
}