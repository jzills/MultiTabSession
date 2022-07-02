namespace MultiTabSession.Session;

public abstract class SessionBase
{
    public int Id { get; set; }
    public Guid? WindowName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? LastRequestedAt { get; set; }

    public void Initialize(string sessionId) => 
        (Id, WindowName, CreatedAt) = (
            DateTime.Now.Millisecond, 
            Guid.Parse(sessionId), 
            DateTime.Now
        );
}