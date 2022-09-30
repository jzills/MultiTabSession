namespace Source.Session;

public abstract class SessionBase
{
    public long Id { get; set; }
    public Guid WindowName { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime? LastRequestedAt { get; set; }
    public long ExpiresIn { get; set; } = DateTime.Now.Ticks;
    public bool IsExpired() => ModifiedAt.Subtract(CreatedAt).Minutes > SessionConfiguration.SlidingExpirationInMinutes;
}