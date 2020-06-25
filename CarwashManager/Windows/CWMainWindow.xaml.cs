using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarwashManager.Classes;
using CarwashManager.Controls;
using CWLib;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for CWMainWindow.xaml
    /// </summary>
    public partial class CWMainWindow : Window, IWindow
    {
        public int PersonId { get; set; }
        private string ConnStr { get; set; }
        private List<Box> Boxes { get; set; }
        private List<CarwashOrder> ActiveOrders { get; set; }

        public CWMainWindow()
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();

            InitializeComponent();
        }

        public void SetWindowBackGround()
        {
            
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("MainWindowName");
        }

        private void UpdateBoxesPanel()
        {
            boxControlsPanel.Children.Clear();

            using (BackgroundWorker boxesWorker = new BackgroundWorker())
            {
                boxesWorker.DoWork += BoxesWorker_DoWork;
                boxesWorker.RunWorkerCompleted += BoxesWorker_RunWorkerCompleted;
                boxesWorker.RunWorkerAsync();
            }
        }

        private void BoxesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var boxes = new List<Box>();
            DBWorker.BoxesSearch(ConnStr, out boxes);
            Boxes = boxes;
        }

        private void BoxesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Boxes != null && Boxes.Count > 0)
            {
                for (int i = 0; i < Boxes.Count; i++)
                {
                    BoxModControl box = new BoxModControl(Boxes[i]);
                    box.ConnStr = ConnStr;
                    box.MouseDown += Box_MouseDown;
                    boxControlsPanel.Children.Add(box);
                }
            }

            BoxModControl addBox = new BoxModControl(true);
            addBox.ConnStr = ConnStr;
            addBox.MouseDown += Box_MouseDown;
            boxControlsPanel.Children.Add(addBox);
        }

        private void Box_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateBoxesPanel();
        }

        private void RefreshOrdersPanel()
        {
            ordersPanel.Children.Clear();

            using (BackgroundWorker ordersWorker = new BackgroundWorker())
            {
                ordersWorker.DoWork += OrdersWorker_DoWork;
                ordersWorker.RunWorkerCompleted += OrdersWorker_RunWorkerCompleted;
                ordersWorker.RunWorkerAsync();
            }
        }

        private void OrdersWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ActiveOrders = CarwashOrder.GetActiveOrders(ConnStr);
        }

        private void OrdersWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ActiveOrders.Count > 0)
            {
                titleGrid.Visibility = Visibility.Visible;

                for (int i = 0; i < ActiveOrders.Count; i++)
                {
                    NewOrderLineControl order = new NewOrderLineControl(ActiveOrders[i]);
                    order.endOrderBtn.Click += EndOrderBtn_Click;
                    order.updateOrderBtn.Click += UpdateOrderBtn_Click;
                    order.delOrderBtn.Click += DelOrderBtn_Click;
                    ordersPanel.Children.Add(order);
                }
            }
            else
                titleGrid.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userLbl.Content = User.FindNameByUserName(ConnStr, PersonId);

            UpdateBoxesPanel();
            RefreshOrdersPanel();
        }

        private void UpdateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshOrdersPanel();
            UpdateBoxesPanel();
        }

        private void DelOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshOrdersPanel();
        }

        private void EndOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshOrdersPanel();
            UpdateBoxesPanel();
        }

        private void cashboxBtn_Click(object sender, RoutedEventArgs e)
        {
            CashboxWindow cashbox = new CashboxWindow();
            cashbox.Owner = this;
            cashbox.ShowDialog();
        }

        private void clientsBtn_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsDict = new ClientsWindow();
            clientsDict.Owner = this;
            clientsDict.ShowDialog();
        }

        private void servicesBtn_Click(object sender, RoutedEventArgs e)
        {
            ServicesDictWindow services = new ServicesDictWindow();
            services.Owner = this;
            services.ShowDialog();
        }

        private void workersBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkersDictWindow workers = new WorkersDictWindow();
            workers.Owner = this;
            workers.ShowDialog();
        }

        private void carsBtn_Click(object sender, RoutedEventArgs e)
        {
            CarDictWindow carDict = new CarDictWindow();
            carDict.Owner = this;
            carDict.ShowDialog();
        }

        private void newCarwashOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCarwashOrderLicenceWindow licenceWindow = new NewCarwashOrderLicenceWindow();
            licenceWindow.Owner = this;
            licenceWindow.goToOrderBtn.Click += GoToOrderBtn_Click;
            licenceWindow.ShowDialog();
        }

        private void GoToOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshOrdersPanel();
            UpdateBoxesPanel();
        }
    }
}
