using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;

namespace iEventViewer
{
    public partial class MainWindow : Window
    {
        public ViewModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Model = new ViewModel();
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Model_PropertyChanged);
            DataContext = Model;
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ComputerName")
            {
                var repo = new EventRepository();
                Model.ImportEvents(repo.GetEvents(Model.ComputerName, "Application"));
            }
        }
    }
}
