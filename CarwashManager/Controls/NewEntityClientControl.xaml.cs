using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CarwashManager.Windows;
using CWLib;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for NewEntityClientControl.xaml
    /// </summary>
    public partial class NewEntityClientControl : UserControl
    {
        public EntityClient Client { get; set; }
        public List<ClientCar> carsList;
        private string ConnStr { get; set; }

        public NewEntityClientControl()
        {
            InitializeComponent();

            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Client != null)
            {
                nameTxt.Text = Client.Name;
                phoneTxt.Text = Client.Phone;
                laTxt.Text = Client.LegalAdress;
                faTxt.Text = Client.FactAdress;
                innTxt.Text = Client.Inn;
                ogrnTxt.Text = Client.Ogrn;
                rsTxt.Text = Client.PaymentAccount;
                ksTxt.Text = Client.CorrAccount;
                bikTxt.Text = Client.Bik;
                directorTxt.Text = Client.GeneralManager;
                gbTxt.Text = Client.ChiefAccountant;
                klTxt.Text = Client.ContactPerson;
                phoneKlTxt.Text = Client.ContactPersonPhone;

                carsList = (List<ClientCar>)Extesions.Clone(Client.Cars);

                if (Client.Cars != null && Client.Cars.Count > 0)
                {
                    string resCarsStr = string.Empty;
                    for (int i = 0; i < Client.Cars.Count; i++)
                    {
                        if (i < Client.Cars.Count - 1)
                            resCarsStr += Client.Cars[i].LicencePlate + ", ";
                        else
                            resCarsStr += Client.Cars[i].LicencePlate;
                    }

                    carsLbl.Content = resCarsStr;
                }
            }
        }

        private void addCarBtn_Click(object sender, RoutedEventArgs e)
        {
            NewClientCarsWindow carsWindow = new NewClientCarsWindow();

            if (carsList != null && carsList.Count > 0)
                carsWindow.cars = carsList;

            carsWindow.Closing += Cars_Closing;
            carsWindow.ShowDialog();
        }

        private void Cars_Closing(object sender, CancelEventArgs e)
        {
            carsList = ((NewClientCarsWindow)sender).cars;
            string resCarsStr = string.Empty;

            for (int i = 0; i < carsList.Count; i++)
            {
                if (i < carsList.Count - 1)
                    resCarsStr += carsList[i].LicencePlate + ", ";
                else
                    resCarsStr += carsList[i].LicencePlate;
            }

            carsLbl.Content = resCarsStr;
        }       
    }
}
