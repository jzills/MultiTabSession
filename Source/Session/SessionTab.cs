using System.Collections.Generic;

namespace MultiTabSession.Session;

public class SessionTab : SessionBase
{
    public Dictionary<string, string> ApplicationState { get; set; } = new();
}