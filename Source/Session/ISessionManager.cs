namespace MultiTabSession.Session;

public interface ISessionManager<TSessionState> where TSessionState : SessionBase
{
    TSessionState? Current { get; }

    Guid Add(string sessionId, TSessionState value);

    Guid Update(string sessionId, TSessionState value);

    TSessionState? Get(string sessionId);

    IEnumerable<TSessionState>? Get(bool ignoreCurrent = true);

    void Remove(string sessionId);

    void SetCurrent(string sessionId);
}