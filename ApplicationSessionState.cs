namespace MultiTabSession;

public class ApplicationSessionState
{
    public int Id { get; set; }
    public Guid WindowName { get; set; }
    public Dictionary<string, string> ApplicationState { get; set; } = new();
}