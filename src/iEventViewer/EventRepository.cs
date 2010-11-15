using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace iEventViewer
{
    public class EventRepository
    {
        public IEnumerable<EventLogEntry> GetEvents(string computerName, string logName)
        {
            var eventLog = new EventLog(logName, computerName);
            foreach (EventLogEntry eventEntry in eventLog.Entries)
                yield return eventEntry;
        }

        public IEnumerable<string> GetLogNames(string computerName)
        {
            return EventLog.GetEventLogs(computerName).Select(x => x.LogDisplayName);
        }
    }
}
