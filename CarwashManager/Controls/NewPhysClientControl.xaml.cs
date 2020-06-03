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
using CarwashManager.Windows;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for NewPhysClientControl.xaml
    /// </summary>
    public partial class NewPhysClientControl : UserControl
    {
        public IndividualClient Client { get; set; }

        public List<ClientCar> carsList;

        private string ConnStr { get; set; }

        public NewPhysClientControl()
        {
            InitializeComponent();

            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<ClientGroup> groups = DBWorker.GroupsSearch(ConnStr);
            groupList.ItemsSource = groups;

            if (Client != null)
            {
                nameTxt.Text = Client.Name;
                phoneTxt.Text = Client.Phone;
                mailTxt.Text = Client.Email;
                bDateTxt.SelectedDate = Client.BirthDate;

                carsList = (List<ClientCar>)Extesions.Clone(Client.Cars);

                for (int i=0; i<groupList.Items.Count; i++)
                {
                    if (((ClientGroup)groupList.Items[i]).Id == Client.Group.Id)
                        groupList.SelectedItem = groupList.Items[i];
                }

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

        private void Cars_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void phoneTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
