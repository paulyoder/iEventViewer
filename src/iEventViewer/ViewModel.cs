using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading;

namespace iEventViewer
{
    public class ViewModel : INotifyPropertyChanged
    {
        private EventRepository _repo;
        private IEnumerable<EventLogEntry> _allEvents;
        private List<EventLogEntry> _nextPageEvents;
        private int _currentPage;

        public ObservableCollection<EventLogEntry> DisplayedEvents { get; set; }
        public ObservableCollection<string> LogNames { get; set; }
        #region ComputerName (INotifyPropertyChanged Property)
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
                    OnComputerNameChanged();
                }
            }
        }
        #endregion
        #region SelectedLogName (INotifyPropertyChanged Property)
        private string _selectedLogName;
        public string SelectedLogName
        {
            get { return _selectedLogName; }
            set
            {
                if (_selectedLogName != value)
                {
                    _selectedLogName = value;
                    OnPropertyChanged("SelectedLogName");
                    if (_selectedLogName != null)
                        OnSelectedLogNameChanged();
                }
            }
        }
        #endregion
        #region EventsCount (INotifyPropertyChanged Property)
        private int _eventsCount;
        public int EventsCount
        {
            get { return _eventsCount; }
            set
            {
                if (_eventsCount != value)
                {
                    _eventsCount = value;
                    OnPropertyChanged("EventsCount");
                }
            }
        }
        #endregion
        #region DisplayedEventsFrom (INotifyPropertyChanged Property)
        private int _displayedEventsFrom;
        public int DisplayedEventsFrom
        {
            get { return _displayedEventsFrom; }
            set
            {
                if (_displayedEventsFrom != value)
                {
                    _displayedEventsFrom = value;
                    OnPropertyChanged("DisplayedEventsFrom");
                }
            }
        }
        #endregion
        #region DisplayedEventsTo (INotifyPropertyChanged Property)
        private int _displayedEventsTo;
        public int DisplayedEventsTo
        {
            get { return _displayedEventsTo; }
            set
            {
                if (_displayedEventsTo != value)
                {
                    _displayedEventsTo = value;
                    OnPropertyChanged("DisplayedEventsTo");
                }
            }
        }
        #endregion

        public ICommand NextPageCommand { get; private set; }

        public ViewModel()
        {
            DisplayedEvents = new ObservableCollection<EventLogEntry>();
            LogNames = new ObservableCollection<string>();
            _repo = new EventRepository();
            _nextPageEvents = new List<EventLogEntry>();
            NextPageCommand = new RelayCommand(x => DisplayNextPageEvents());
        }


        private void OnComputerNameChanged()
        {
            DisplayedEvents.Clear();
            LogNames.Clear();
            SelectedLogName = null;
            GetComputerLogNames();
        }

        private void GetComputerLogNames()
        {
            LogNames.Clear();
            _selectedLogName = null;
            LogNames.AddRange(_repo.GetLogNames(ComputerName));
        }

        private void OnSelectedLogNameChanged()
        {
            ImportEvents();
        }

        private void ImportEvents()
        {
            _allEvents = _repo.GetEvents(ComputerName, SelectedLogName).Reverse();
            _nextPageEvents.Clear();
            EventsCount = _allEvents.Count();
            _currentPage = 0;
            DisplayedEventsFrom = 1;
            DisplayedEventsTo = 20;
            DisplayNextPageEvents();
        }

        private void DisplayNextPageEvents()
        {
            if (_nextPageEvents.Count == 0)
                PrePopulateNextPage();
            _currentPage++;
            DisplayedEvents.Clear();
            DisplayedEvents.AddRange(_nextPageEvents);
            DisplayedEventsFrom = 20 * (_currentPage - 1) + 1;
            DisplayedEventsTo = DisplayedEventsFrom + 19;

            ThreadStart start = () => PrePopulateNextPage();
            new Thread(start).Start();
        }

        private void PrePopulateNextPage()
        {
            _nextPageEvents.Clear();
            _nextPageEvents.AddRange(_allEvents.Skip((_currentPage) * 20).Take(20));
        }

        #region INotifyPropertyChanged values

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
