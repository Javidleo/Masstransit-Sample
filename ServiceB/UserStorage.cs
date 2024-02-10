namespace ServiceB;

public class UserStorage
{
    public static List<User> users = new();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public List<User> ReadAll()
    {
        return users;
    }
}

public record User(string Name, string Family);