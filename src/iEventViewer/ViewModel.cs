using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace iEventViewer
{
    public class ViewModel
    {
        private IEnumerable<EventLogEntry> _allEvents;
        public ObservableCollection<EventLogEntry> DisplayedEvents { get; set; }

        public ViewModel()
        {
            DisplayedEvents = new ObservableCollection<EventLogEntry>();
        }

        public void ImportEvents(IEnumerable<EventLogEntry> events)
        {
            _allEvents = events;
            DisplayedEvents.Clear();
            DisplayedEvents.AddRange(_allEvents.Reverse().Take(20));
        }
    }
}
