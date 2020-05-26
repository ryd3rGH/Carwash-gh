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
using CarwashManager.Windows;
using CWLib;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for ClientEntityDetailsControl.xaml
    /// </summary>
    public partial class ClientEntityDetailsControl : UserControl
    {
        private EntityClient Client { get; set; }

        private void ShowClientDetails()
        {
            clientNameTxt.Text = Client.Name;
            clientPhoneTxt.Text = Client.Phone;
            clientFATxt.Text = Client.FactAdress;
            clientLATxt.Text = Client.LegalAdress;
            clientBikTxt.Text = Client.Bik;
            clientDirTxt.Text = Client.GeneralManager;
            clientGbTxt.Text = Client.ChiefAccountant;
            clientInnTxt.Text = Client.Inn;
            clientKLPhoneTxt.Text = Client.ContactPersonPhone;
            clientKLTxt.Text = Client.ContactPerson;
            clientKSTxt.Text = Client.CorrAccount;
            clientOgrnTxt.Text = Client.Ogrn;
            clientRSTxt.Text = Client.PaymentAccount;

            string carsStr = string.Empty;
            for (int i = 0; i < Client.Cars.Count; i++)
            {
                if (i != Client.Cars.Count - 1)
                    carsStr += $"{Client.Cars[i].LicencePlate} ({Client.Cars[i].Car.Brand.Name} {Client.Cars[i].Car.Name}), ";
                else
                    carsStr += $"{Client.Cars[i].LicencePlate} ({Client.Cars[i].Car.Brand.Name} {Client.Cars[i].Car.Name})";
            }

            clientCarsTxt.Text = carsStr;
        }

        public ClientEntityDetailsControl(int personId)
        {
            InitializeComponent();

            Client = new EntityClient();
            Client.Id = personId;
            Client.GetClientInfoById(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());

            ShowClientDetails();
        }

        private void deleteClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Client != null && Client.DeleteClient(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString()))
                MessageBox.Show("Клиент успешно удален");
            else
                MessageBox.Show("При удалении клиента возникла ошибка");
        }

        private void updateClientBtn_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow updateClient = new NewClientWindow();
            updateClient.EntityClient = Client;
            updateClient.ShowDialog();
        }
    }
}
