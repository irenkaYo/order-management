namespace OrderManagementSystem;

public class User
{
    public int UserId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public User(int userId, string userName, string email)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
    }
}