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
    /// Interaction logic for ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window, IWindow
    {
        bool stFlg;

        public ClientsWindow()
        {
            InitializeComponent();

            clientSearchTab.Visibility = Visibility.Hidden;
            filterTypeList.SelectedIndex = 0;

            SetWindowBackGround();
            SetWindowTitle();

            RefreshGroupsList();
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("ClientsDictWindowName");
        }

        private void RefreshGroupsList()
        {
            clientGroupsList.ItemsSource = null;
            clientGroupsList.Items.Clear();

            List<ClientGroup> groups = DBWorker.GroupsSearch(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
            clientGroupsList.ItemsSource = groups;
        }

        private void RefreshSearchClientsList()
        {
            searchClientsList.ItemsSource = null;
            searchClientsList.Items.Clear();

            bool withEntities = withEntitiesChBox.IsChecked == true ? true : false;
            if (filterTypeList.SelectedIndex == 0)
                searchClientsList.ItemsSource = DBWorker.ClientsSearchByName(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString(), withEntities);
            else if (filterTypeList.SelectedIndex == 1)
                searchClientsList.ItemsSource = DBWorker.ClientsSearchByPlate(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString(), withEntities);
        }

        private void addClientBtn_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow newClient = new NewClientWindow();
            newClient.Owner = this;
            newClient.ShowDialog();
        }

        private void addClientGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            NewClientsGroupWindow newGroup = new NewClientsGroupWindow();
            newGroup.Owner = this;
            newGroup.addBtn.Click += AddBtn_Click;
            newGroup.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshGroupsList();
        }

        private void clientGroupsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clientGroupsList.SelectedIndex != -1)
            {
                ClientGroupBigControl group = new ClientGroupBigControl((ClientGroup)clientGroupsList.SelectedItem);
                groupContentGrid.Children.Clear();
                groupContentGrid.Children.Add(group);
            }
            else
            {
                groupContentGrid.Children.Clear();
            }
        }

        private void refClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshSearchClientsList();
        }

        private void searchClientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchClientsList.SelectedIndex != -1)
            {
                int personId = 0;
                if (searchClientsList.SelectedItem.GetType() == typeof(ClientNameForSearchList))
                    personId = ((ClientNameForSearchList)searchClientsList.SelectedItem).Id;
                else
                    personId = ((ClientPlateForSearchList)searchClientsList.SelectedItem).Id;

                var res = DBWorker.EntityClientCheck(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString(), personId);
                if (!res)
                {
                    /* физ лицо */
                    clientDetailsGrid.Children.Clear();
                    ClientPhysDetailsControl client = new ClientPhysDetailsControl(personId);
                    client.deleteClientBtn.Click += DeleteClientBtn_Click;
                    client.updateClientBtn.Click += UpdateClientBtn_Click;
                    clientDetailsGrid.Children.Add(client);
                }
                else
                {
                    /* юр лицо */
                    clientDetailsGrid.Children.Clear();
                    ClientEntityDetailsControl client = new ClientEntityDetailsControl(personId);
                    client.deleteClientBtn.Click += DeleteClientBtn_Click;
                    client.updateClientBtn.Click += UpdateClientBtn_Click;
                    clientDetailsGrid.Children.Add(client);
                }              
            }
            else
                clientDetailsGrid.Children.Clear();
        }

        private void UpdateClientBtn_Click(object sender, RoutedEventArgs e)
        {
            var tempSelIndex = searchClientsList.SelectedIndex;

            RefreshSearchClientsList();

            searchClientsList.SelectedIndex = tempSelIndex;
        }

        private void DeleteClientBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshSearchClientsList();
        }

        private void filterTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stFlg)
            {
                if (filterTypeList.SelectedIndex != -1)
                {
                    RefreshSearchClientsList();
                }
                else
                    searchClientsList.Items.Clear();
            }

            stFlg = true;
        }
    }
}
