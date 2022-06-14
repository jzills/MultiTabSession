using System.Collections.Generic;
using System.Threading.Tasks;
using MultiTabSession.Session;

namespace MultiTabSession.Hubs;

public interface ISessionHub
{
    Task Notify(IEnumerable<SessionTab>? sessionTabs);
}