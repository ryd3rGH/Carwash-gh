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
    /// Interaction logic for OrderServiceControl.xaml
    /// </summary>
    public partial class OrderServiceControl : UserControl
    {
        private CarwashService Service { get; set; }        

        public OrderServiceControl(CarwashService service, int classOrCatId)
        {            
            Service = service;
            Service.OrderServiceCount = 0;

            InitializeComponent();

            serviceNameBox.Content = Service.Name;

            if (service.CategoryPrices != null)
                Service.UsedPrice = (decimal)service.CategoryPrices.Find(x => x.Id == classOrCatId).Price;
            else
                Service.UsedPrice = (decimal)service.ClassPrices.Find(x => x.Id == classOrCatId).Price;

            numTxt.Text = Service.OrderServiceCount.ToString();
            priceLbl.Content = Service.UsedPrice + " руб.";

            ChangeControlsState(false, 0);
        }

        private void ChangeControlsState(bool IsEnabled, int countValue)
        {
            Service.OrderServiceCount = countValue;
            numTxt.Text = Service.OrderServiceCount.ToString();

            minBtn.IsEnabled = IsEnabled;
            numTxt.IsEnabled = IsEnabled;
            plusBtn.IsEnabled = IsEnabled;
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Service.OrderServiceCount > 0)
                Service.OrderServiceCount--;
            else
                serviceNameBox.IsChecked = false;

            numTxt.Text = Service.OrderServiceCount.ToString();
        }

        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Service.OrderServiceCount < 99)
                Service.OrderServiceCount++;

            numTxt.Text = Service.OrderServiceCount.ToString();
        }

        private void serviceNameBox_Checked(object sender, RoutedEventArgs e)
        {
            ChangeControlsState(true, (int)Service.OrderServiceCount != 0 ? (int)Service.OrderServiceCount : 1);
        }

        private void serviceNameBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeControlsState(false, (int)Service.OrderServiceCount);
        }

        public CarwashService GetService()
        {
            return Service;
        }

        public int GetCount()
        {
            return (int)Service.OrderServiceCount;
        }

        public decimal GetSinglePrice()
        {
            return (decimal)Service.UsedPrice;
        }

        public decimal GetCurrentPrice()
        {
            return (decimal)Service.UsedPrice * (int)Service.OrderServiceCount;
        }

        private void numTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
