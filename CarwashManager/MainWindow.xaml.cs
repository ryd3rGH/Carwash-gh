using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CWLib;
using CarwashManager.Windows;
using CarwashManager.Classes;
using CarwashManager.Controls;
using System.Reflection;
using System.Data;
using System.ComponentModel;
using System.Threading;

namespace CarwashManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindow 
    {
        public int WorkerId { get; set; }
        private List<Box> Boxes { get; set; }
        private List<CarwashOrder> ActiveOrders { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetFontSize()
        {
            for (int i = 0; i < mainGrid.Children.Count; i++)
                ((Control)mainGrid.Children[i]).FontSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MainFontSize"]);
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("MainWindowName");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowTitle();
            SetWindowBackGround();

            UpdateBoxesPanel();
            RefreshOrdersPanel();
        }

        private void RefreshOrdersPanel()
        {
            loadBar.Visibility = Visibility.Visible;
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
            ActiveOrders = CarwashOrder.GetActiveOrders(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
        }

        private void OrdersWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ActiveOrders.Count > 0)
            {
                for (int i = 0; i < ActiveOrders.Count; i++)
                {
                    OrderLineControl order = new OrderLineControl(ActiveOrders[i]);
                    order.endOrderBtn.Click += EndOrderBtn_Click;
                    order.updateOrderBtn.Click += UpdateOrderBtn_Click;
                    order.delOrderBtn.Click += DelOrderBtn_Click;
                    ordersPanel.Children.Add(order);
                }
            }

            loadBar.Visibility = Visibility.Hidden;
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

        private void UpdateBoxesPanel()
        {
            loadBar.Visibility = Visibility.Visible;
            boxControlPanel.Children.Clear();

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
            DBWorker.BoxesSearch(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString(), out boxes);
            Boxes = boxes;
        }

        private void BoxesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Boxes != null && Boxes.Count > 0)
            {
                for (int i = 0; i < Boxes.Count; i++)
                {
                    BoxControl box = new BoxControl(Boxes[i]);
                    box.ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
                    box.deleteBtn.Click += DeleteBtn_Click;
                    box.changeStateBtn.Click += ChangeStateBtn_Click;
                    box.updateBtn.Click += UpdateBtn_Click;
                    boxControlPanel.Children.Add(box);
                }
            }

            loadBar.Visibility = Visibility.Hidden;
        }        

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateBoxesPanel();
        }

        private void ChangeStateBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateBoxesPanel();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateBoxesPanel();
        }

        private void addBoxBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.NewBoxWindow newBox = new Windows.NewBoxWindow();
            newBox.Owner = this;
            newBox.addBtn.Click += AddBtn_Click;
            newBox.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateBoxesPanel();
        }

        private void carDictBtn_Click(object sender, RoutedEventArgs e)
        {
            CarDictWindow carDict = new CarDictWindow();
            carDict.Owner = this;
            carDict.ShowDialog();
        }

        private void clientsDictBtn_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsDict = new ClientsWindow();
            clientsDict.Owner = this;
            clientsDict.ShowDialog();
        }

        private void workersDictBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkersDictWindow workers = new WorkersDictWindow();
            workers.Owner = this;
            workers.ShowDialog();
        }

        private void servicesBtn_Click(object sender, RoutedEventArgs e)
        {
            ServicesDictWindow services = new ServicesDictWindow();
            services.Owner = this;
            services.ShowDialog();
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

        private void cashboxBtn_Click(object sender, RoutedEventArgs e)
        {
            CashboxWindow cashbox = new CashboxWindow();
            cashbox.Owner = this;
            cashbox.ShowDialog();
        }
    }
}
