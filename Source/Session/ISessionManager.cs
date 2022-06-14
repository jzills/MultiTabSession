using System;
using System.Collections.Generic;

namespace MultiTabSession.Session;

public interface ISessionManager<TSessionState> where TSessionState : SessionBase
{
    TSessionState? Current { get; }

    Guid AddSession(string sessionId, TSessionState value);

    Guid UpdateSession(string sessionId, TSessionState value);

    TSessionState? GetSession(string sessionId);

    IEnumerable<TSessionState>? GetSessions(bool ignoreCurrent = true);

    void RemoveSession(string sessionId);

    void SetCurrent(string sessionId);
}