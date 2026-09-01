namespace Domain;

public class Project
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public bool IsActive { get; private set; }

    private Project() { }

    public Project(string name)
    {
        Name = name;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}