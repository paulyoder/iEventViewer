using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace iEventViewer
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IEnumerable<EventLogEntry> _allEvents;

        private string _computerName;
        public string ComputerName
        {
            get { return _computerName; }
            set
            {
                if (_computerName != value)
                {
                    _computerName = value;
                    OnPropertyChanged("ComputerName");
                }
            }
        }

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



        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
