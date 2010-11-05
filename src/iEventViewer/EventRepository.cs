using System.Collections.Generic;
using System.Diagnostics;

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
    }
}
