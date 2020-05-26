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
using System.Windows.Shapes;
using CWLib;
using CarwashManager.Classes;
using CarwashManager.Controls;
using System.Resources;
using System.Reflection;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for WorkersDictWindow.xaml
    /// </summary>
    public partial class WorkersDictWindow : Window, IWindow
    {
        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("WorkersDictWindowName");
        }

        public WorkersDictWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();

            groupList.ItemsSource = DBWorker.WorkersCategorySearch(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
        }

        private void RefreshWorkersList()
        {
            if (groupList.SelectedIndex != -1)
            {
                workersList.ItemsSource = null;
                workersList.Items.Clear();

                List<Worker> workers = Worker.GetWorkersByGroup(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString(),
                                                                    ((WorkersCategory)groupList.SelectedItem).GroupId);

                workersList.ItemsSource = workers;
            }
        }

        private void RefreshWorkerDetails()
        {
            if (workersList.SelectedIndex != -1)
            {
                contentPanel.Children.Clear();
                WorkerDetailsControl details = new WorkerDetailsControl((Worker)workersList.SelectedItem);
                details.updateWorkerBtn.Click += UpdateWorkerBtn_Click;
                details.deleteWorkerBtn.Click += DeleteWorkerBtn_Click;
                contentPanel.Children.Add(details);
            }
            else
                contentPanel.Children.Clear();
        }

        private void UpdateWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            var tempSelId = ((Worker)workersList.SelectedItem).Id;
            RefreshWorkersList();

            for (int i=0; i<workersList.Items.Count; i++)
            {
                if (((Worker)workersList.Items[i]).Id == tempSelId)
                    workersList.SelectedItem = workersList.Items[i];
            }

            RefreshWorkerDetails();
        }

        private void DeleteWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshWorkersList();
        }

        private void groupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshWorkersList();
        }

        private void addWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            NewWorkerWindow newWorker = new NewWorkerWindow();
            newWorker.Owner = this;
            newWorker.addBtn.Click += AddBtn_Click;
            newWorker.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshWorkersList();
        }

        private void workersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshWorkerDetails();
        }
    }
}
