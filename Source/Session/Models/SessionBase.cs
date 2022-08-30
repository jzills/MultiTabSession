namespace MultiTabSession.Session;

public abstract class SessionBase
{
    public long Id { get; set; }
    public Guid? WindowName { get; set; } 
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? LastRequestedAt { get; set; }
}