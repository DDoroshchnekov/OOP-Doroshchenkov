namespace MyProject.Domain;

public class User
{
    public Guid Id { get; }
    public string Name { get; }

    public User(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        Id = Guid.NewGuid();
        Name = name;
    }
}