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
using System.Resources;
using System.Reflection;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewCarwashOrderLicenceWindow.xaml
    /// </summary>
    public partial class NewCarwashOrderLicenceWindow : Window, IWindow
    {
        public NewCarwashOrderLicenceWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();

            RefreshLicencePlatesList();
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("NewOrderLicenceWindowName");
        }

        private void RefreshLicencePlatesList()
        {
            licencePlatesList.ItemsSource = null;
            licencePlatesList.Items.Clear();

            licencePlatesList.ItemsSource = ClientCar.GetCars(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
        }

        private void goToOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (licencePlatesList.Text != string.Empty && String.IsNullOrWhiteSpace(licencePlatesList.Text) != true)
            {
                ClientCar car = new ClientCar();
                if (licencePlatesList.SelectedIndex != -1)
                {
                    for (int i = 0; i < licencePlatesList.Items.Count; i++)
                    {
                        if (licencePlatesList.Text == ((ClientCar)licencePlatesList.SelectedItem).ToString())
                            car = (ClientCar)licencePlatesList.SelectedItem;
                    }
                }
                else
                    car.LicencePlate = licencePlatesList.Text;                

                NewCarwashOrderWindow order = new NewCarwashOrderWindow(car);
                order.Owner = this.Owner;
                order.addOrderBtn.Click += AddOrderBtn_Click;

                this.Close();
                order.ShowDialog();
            }
            else
                MessageBox.Show("Не указан или не выбран гос.номер");
        }

        public void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
