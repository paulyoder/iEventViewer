using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Diagnostics;

namespace iEventViewer
{
    public partial class MainWindow : Window
    {
        public ViewModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Model = new ViewModel();
            DataContext = Model;

            GetLatestEvents();
        }

        protected void GetLatestEvents()
        {
            var repo = new EventRepository();
            Model.ImportEvents(repo.GetEvents("localhost", "Application"));
        }
    }
}
