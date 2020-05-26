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
using CWLib;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for ClientCarControl.xaml
    /// </summary>
    public partial class ClientCarControl : UserControl
    {
        public ClientCar car;

        public ClientCarControl(ClientCar car)
        {
            InitializeComponent();

            this.car = car;

            plateLbl.Content = this.car.LicencePlate;
            brandLbl.Content = this.car.Car.Brand.Name;
            modelLbl.Content = this.car.Car.Name;
        }
    }
}
