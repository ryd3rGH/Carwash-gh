using CWLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for OrderWorkerControl.xaml
    /// </summary>
    public partial class OrderWorkerControl : UserControl
    {
        public Worker Worker { get; set; }

        public OrderWorkerControl(Worker worker)
        {
            Worker = worker;
            InitializeComponent();

            nameBox.Content = worker.Name;
            nameBox.IsChecked = false;
        }

        public Worker GetWorker()
        {
            return Worker;
        }
    }
}
