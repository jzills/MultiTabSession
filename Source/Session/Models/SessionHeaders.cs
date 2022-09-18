namespace Source.Session;

public sealed class SessionHeader
{
    private readonly string _value;

    public static readonly SessionHeader InitializeSession      = new SessionHeader("x-init-session");
    public static readonly SessionHeader FromPreviousSession    = new SessionHeader("x-from-previous-session");
    public static readonly SessionHeader Session                = new SessionHeader("x-session");

    private SessionHeader(string value) => _value = value;
    
    public static implicit operator string(SessionHeader @enum) => @enum._value;
}