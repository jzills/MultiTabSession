namespace Source.Session;

public class SessionTab : SessionBase
{
    public Dictionary<string, string> ApplicationState { get; set; } = new();
}