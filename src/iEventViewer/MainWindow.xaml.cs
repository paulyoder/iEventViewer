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

            DataContext = new ViewModel();
        }
    }
}
