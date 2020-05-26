using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewClientCarsWindow.xaml
    /// </summary>
    public partial class NewClientCarsWindow : Window, IWindow
    {
        public List<ClientCar> cars;

        public NewClientCarsWindow()
        {
            InitializeComponent();

            brandsList.Items.Clear();
            brandsList.ItemsSource = XMLWorker.ReadBrands();

            modelsList.IsEnabled = false;            
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
            this.Title = rm.GetString("NewClientCarsWindowName");
        }

        private void RefreshCarsList()
        {
            carsPanel.Children.Clear();

            for (int i=0; i<cars.Count; i++)
            {
                ClientCarControl car = new ClientCarControl(cars[i]);
                car.removeBtn.Click += RemoveBtn_Click;
                carsPanel.Children.Add(car);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cars != null && cars.Count > 0)
            {
                RefreshCarsList();
            }
            else
                cars = new List<ClientCar>();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            ClientCarControl car = (ClientCarControl)((Grid)((Button)sender).Parent).Parent;

            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Car.Id == car.car.Car.Id)
                {
                    cars.Remove(car.car);
                    RefreshCarsList();
                } 
            }

            
        }

        private void brandsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brandsList.SelectedIndex != -1)
            {
                modelsList.IsEnabled = true;
                modelsList.ItemsSource = XMLWorker.ReadAndSearchModels((CarBrand)brandsList.SelectedItem);
            }
            else
            {
                modelsList.IsEnabled = false;
                modelsList.ItemsSource = null;
                modelsList.Items.Clear();

                plateTxt.Text = string.Empty;
            }                
        }

        private void addCarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (brandsList.SelectedIndex != -1)
            {
                if (modelsList.SelectedIndex != -1)
                {
                    if (plateTxt.Text != string.Empty && String.IsNullOrWhiteSpace(plateTxt.Text) != true)
                    {
                        CarModel model = new CarModel()
                        {
                            Brand = (CarBrand)brandsList.SelectedItem,
                            Id = ((CarModel)modelsList.SelectedItem).Id,
                            Name = ((CarModel)modelsList.SelectedItem).Name,
                            Description = ((CarModel)modelsList.SelectedItem).Description
                        };

                        ClientCar clientCar = new ClientCar()
                        {
                            Car = model,
                            LicencePlate = plateTxt.Text != string.Empty ? plateTxt.Text : "#########",
                            CarwashNumber = 0,
                            TyreserviceNumber = 0
                        };

                        cars.Add(clientCar);

                        RefreshCarsList();

                        brandsList.SelectedIndex = -1; 
                    }  
                }
            }
        }        
    }
}
