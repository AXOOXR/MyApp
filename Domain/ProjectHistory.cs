namespace Domain;

public class ProjectHistory
{
    public int Id { get; private set; }

    public int ProjectId { get; private set; }

    public string Action { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private ProjectHistory() { }

    public ProjectHistory(int projectId, string action)
    {
        ProjectId = projectId;
        Action = action;
        CreatedAt = DateTime.UtcNow;
    }
}