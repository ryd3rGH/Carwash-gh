using CarwashManager.Windows;
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
    /// Interaction logic for OrderLineControl.xaml
    /// </summary>
    public partial class OrderLineControl : UserControl
    {
        private string ConnStr { get; set; }
        private CarwashOrder Order { get; set; }

        public OrderLineControl(CarwashOrder order)
        {
            Order = order;
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();            

            InitializeComponent();

            Show();
        }

        private void Show()
        {
            numberLbl.Content = $"Заказ №{Order.Id}";

            string services = string.Empty;

            if (Order.Services.Count > 0)
                for (int i = 0; i < Order.Services.Count; i++)
                    services += Order.Services[i].Name + ", ";

            servicesLbl.Content = services.Substring(0, services.Length - 2);
            estTimeLbl.Content = $"Завершение: {((DateTime)Order.EstimatedEndTime).ToShortTimeString()}, {((DateTime)Order.EstimatedEndTime).ToShortDateString()}";
        }

        private void endOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Order.EndOrder(ConnStr))
                MessageBox.Show($"Заказ №{Order.Id} успешно окончен");

            else
                MessageBox.Show("Во время операции окончания заказа возникла ошибка");
        }

        private void delOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Order.DeleteOrder(ConnStr))
                MessageBox.Show($"Заказ №{Order.Id} успешно удален");
            else
                MessageBox.Show("Во время операции удаления заказа возникла ошибка");
        }

        private void updateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCarwashOrderWindow updateOrder = new NewCarwashOrderWindow(Order);
            updateOrder.ShowDialog();
        }
    }
}
