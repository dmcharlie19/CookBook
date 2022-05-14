namespace CookBook.Core.Domain
{
  public class User
  {
    public int Id { get; protected set; }
    public string Login { get; protected set; }
    public string Password { get; protected set; }
    public string Name { get; protected set; }

    public User(
        string login,
        string password,
        string name )
    {
      Login = login;
      Password = password;
      Name = name;
    }
  }
}
