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
    /// Interaction logic for ClientPhysDetailsControl.xaml
    /// </summary>
    public partial class ClientPhysDetailsControl : UserControl
    {
        private IndividualClient Client { get; set; }

        private void ShowClientDetails(IndividualClient client)
        {
            clientNameTxt.Text = client.Name;
            clientPhoneTxt.Text = client.Phone != null ? client.Phone : "------";
            clientEMailTxt.Text = client.Email != null ? client.Email : "------";
            birthDateTxt.Text = client.BirthDate != null ? ((DateTime)client.BirthDate).ToShortDateString() : "------";
            clientgroupTxt.Text = client.Group.Name;
            clientCommentTxt.Text = client.Comment != null ? client.Comment : "------";

            string carsStr = string.Empty;
            for (int i = 0; i < client.Cars.Count; i++)
            {
                if (i != client.Cars.Count - 1)
                    carsStr += $"{client.Cars[i].LicencePlate} ({client.Cars[i].Car.Brand.Name} {client.Cars[i].Car.Name}), ";
                else
                    carsStr += $"{client.Cars[i].LicencePlate} ({client.Cars[i].Car.Brand.Name} {client.Cars[i].Car.Name})";
            }

            clientCarsTxt.Text = carsStr;
        }

        public ClientPhysDetailsControl(int personId)
        {
            InitializeComponent();

            Client = new IndividualClient();
            Client.Id = personId;
            Client.GetClientInfoById(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());

            ShowClientDetails(Client);
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
            updateClient.IndivClient = Client;
            updateClient.ShowDialog();
        }
    }
}
