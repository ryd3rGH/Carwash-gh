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
using Microsoft.Win32;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for ServicesDictWindow.xaml
    /// </summary>
    public partial class ServicesDictWindow : Window, IWindow
    {
        public ServicesDictWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();

            RefreshServiceTypesList();
        }

        public void SetFontSize()
        {
            
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("ServicesDictWindowName");
        }

        private void ShowServiceDetails(CarwashService service)
        {
            if (servicesList.SelectedIndex != -1)
            {
                nameTxt.Text = service.Name;
                durationTxt.Text = service.Duration.Minutes + " мин.";
                categoryTxt.Text = service.Type.Name;
                descrTxt.Text = service.Description != null && service.Description != string.Empty ? service.Description : "------";

                pricesPanel.Children.Clear();

                if (service.ClassPrices != null && service.ClassPrices.Count > 0)
                {
                    for (int i=0; i<service.ClassPrices.Count; i++)
                    {
                        Controls.ClassPriceControl price = new Controls.ClassPriceControl() { carClass = service.ClassPrices[i] };
                        price.priceTxt.IsEnabled = false;

                        pricesPanel.Children.Add(price);
                    }
                }

                if (service.CategoryPrices != null && service.CategoryPrices.Count > 0)
                {
                    for (int i=0; i<service.CategoryPrices.Count; i++)
                    {
                        Controls.ClassPriceControl price = new Controls.ClassPriceControl() { carCategory = service.CategoryPrices[i] };
                        price.priceTxt.IsEnabled = false;

                        pricesPanel.Children.Add(price);
                    }
                }

                contentPanel.Visibility = Visibility.Visible;
            }
        }

        private void RefreshServiceTypesList()
        {
            typesList.ItemsSource = null;
            typesList.Items.Clear();

            typesList.ItemsSource = ServiceType.GetCWTypes(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
            contentPanel.Visibility = Visibility.Hidden;
        }

        private void RefreshServicesList()
        {
            if (typesList.SelectedIndex != -1)
            {
                servicesList.ItemsSource = null;
                servicesList.Items.Clear();

                servicesList.ItemsSource = CarwashService.GetServices(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString(),
                                                                        ((ServiceType)typesList.SelectedItem).Id,
                                                                        Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom Categories")));

                contentPanel.Visibility = Visibility.Hidden;
            }

            else
            {
                servicesList.ItemsSource = null;
                servicesList.Items.Clear();

                contentPanel.Visibility = Visibility.Hidden;
            }
        }

        private void typesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshServicesList();
        }

        private void addTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            NewServiceCategoryWindow type = new NewServiceCategoryWindow();
            type.Owner = this;
            type.addBtn.Click += AddBtn_Click;
            type.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshServiceTypesList();
        }

        private void addServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCarwashServiceWindow service = new NewCarwashServiceWindow();
            service.Owner = this;
            service.addBtn.Click += AddBtn_Click1;
            service.ShowDialog();
        }

        private void AddBtn_Click1(object sender, RoutedEventArgs e)
        {
            RefreshServicesList();
        }

        private void servicesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowServiceDetails((CarwashService)servicesList.SelectedItem);
        }

        private void deleteServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (servicesList.SelectedIndex != -1)
            {
                if (((CarwashService)servicesList.SelectedItem).DeleteService(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString()))
                {
                    MessageBox.Show("Услуга успешно удалена");
                    RefreshServicesList();
                }
                else
                    MessageBox.Show($"При удалении услуги возникла ошибка\n");
            }
        }

        private void updateServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCarwashServiceWindow serviceUpdate = new NewCarwashServiceWindow();
            serviceUpdate.ServiceToUpdate = (CarwashService)servicesList.SelectedItem;
            serviceUpdate.Owner = this;
            serviceUpdate.addBtn.Click += AddBtn_Click1;
            serviceUpdate.ShowDialog();
        }
    }
}
