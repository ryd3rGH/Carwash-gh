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
    /// Interaction logic for NewClientWindow.xaml
    /// </summary>
    public partial class NewClientWindow : Window, IWindow
    {
        public IndividualClient IndivClient { get; set; }
        public EntityClient EntityClient { get; set; }

        private bool stFlg;
        private string ConnStr { get; set; }

        public NewClientWindow()
        {
            InitializeComponent();

            SetWindowBackGround();

            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
        }

        private void AddControls()
        {
            NewPhysClientControl phys = new NewPhysClientControl();
            phys.Visibility = Visibility.Visible;

            if (IndivClient != null)
                phys.Client = IndivClient;

            contentGrid.Children.Add(phys);

            NewEntityClientControl entity = new NewEntityClientControl();
            entity.Visibility = Visibility.Hidden;
            if (EntityClient != null)
                entity.Client = EntityClient;

            contentGrid.Children.Add(entity);
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            if (IndivClient == null && EntityClient == null)
                this.Title = rm.GetString("NewClientWindowName");
            else
                this.Title = rm.GetString("UpdateClientWindowName");
        }

        private void phBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (stFlg)
            {
                for (int i = 0; i < contentGrid.Children.Count; i++)
                {
                    if (contentGrid.Children[i].GetType() == typeof(NewPhysClientControl))
                        contentGrid.Children[i].Visibility = Visibility.Visible;
                    else
                        contentGrid.Children[i].Visibility = Visibility.Hidden;
                }
            }

            stFlg = true;
        }

        private void phBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < contentGrid.Children.Count; i++)
            {
                if (contentGrid.Children[i].GetType() == typeof(NewPhysClientControl))
                    contentGrid.Children[i].Visibility = Visibility.Hidden;
                else
                    contentGrid.Children[i].Visibility = Visibility.Visible;
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            /*добавление частного лица (клиента) */
            if ((bool)phBtn.IsChecked)
            {
                for (int i = 0; i < contentGrid.Children.Count; i++)
                {
                    if (contentGrid.Children[i].GetType() == typeof(NewPhysClientControl))
                    {
                        NewPhysClientControl control = (NewPhysClientControl)contentGrid.Children[i];

                        if (control.groupList.Items.Count > 0)
                        {
                            if (control.nameTxt.Text != string.Empty && String.IsNullOrWhiteSpace(control.nameTxt.Text) != true)
                            {
                                if (control.carsList != null && control.carsList.Count > 0)
                                {
                                    if (IndivClient == null)
                                    {
                                        IndividualClient client = new IndividualClient()
                                        {
                                            Name = control.nameTxt.Text,
                                            Phone = control.phoneTxt.Text != string.Empty ? control.phoneTxt.Text : null,
                                            Email = control.mailTxt.Text != string.Empty ? control.mailTxt.Text : null,
                                            BirthDate = control.bDateTxt.SelectedDate != null ? (DateTime?)control.bDateTxt.SelectedDate : null,
                                            Group = control.groupList.SelectedIndex != -1 ? (ClientGroup)control.groupList.SelectedItem : (ClientGroup)control.groupList.Items[0],
                                            Cars = control.carsList,
                                            Comment = control.commentTxt.Text != string.Empty ? control.commentTxt.Text : null,
                                            DateAdd = DateTime.Now.Date
                                        };

                                        if (client.AddIndividualClient(ConnStr))
                                        {
                                            MessageBox.Show("Новый клиент успешно добавлен");
                                            this.Close();
                                        }
                                        else
                                            MessageBox.Show($"При добавлении клиента произошла ошибка"); 
                                    }
                                    else
                                    {
                                        if (IndivClient.UpdateIndividualClient(ConnStr, (ClientGroup)control.groupList.SelectedItem, control.carsList, 
                                                                 control.nameTxt.Text, control.phoneTxt.Text, control.mailTxt.Text, control.bDateTxt.SelectedDate))
                                        {
                                            MessageBox.Show("Информация о клиенте успешно обновлена");
                                            this.Close();
                                        }

                                        else
                                        {
                                            MessageBox.Show($"При обновлении информации о клиенте произошла ошибка");
                                        }
                                    }
                                }
                                else
                                    MessageBox.Show("Не выбран(ы) автомобиль(и) клиента");
                            }
                            else
                                MessageBox.Show("Не указано имя клиента");
                        }
                        else
                            MessageBox.Show("Не созданы группы клиентов.\nПерейдите во вкладку \"Группы клиентов\" и создайте хотя бы одну группу");
                    }
                }
            }
            /* добавление юр. лица */
            else if ((bool)entBtn.IsChecked)
            {
                for (int i = 0; i < contentGrid.Children.Count; i++)
                {
                    if (contentGrid.Children[i].GetType() == typeof(NewEntityClientControl))
                    {
                        NewEntityClientControl control = (NewEntityClientControl)contentGrid.Children[i];

                        if (control.nameTxt.Text != string.Empty && String.IsNullOrWhiteSpace(control.nameTxt.Text) != true)
                        {
                            if (control.carsList != null && control.carsList.Count > 0)
                            {
                                if (EntityClient == null)
                                {
                                    EntityClient client = new EntityClient()
                                    {
                                        Name = control.nameTxt.Text,
                                        Phone = control.phoneTxt.Text != string.Empty ? control.phoneTxt.Text : null,
                                        LegalAdress = control.laTxt.Text != string.Empty ? control.laTxt.Text : null,
                                        FactAdress = control.faTxt.Text != string.Empty ? control.faTxt.Text : null,
                                        Inn = control.innTxt.Text != string.Empty ? control.innTxt.Text : null,
                                        Ogrn = control.ogrnTxt.Text != string.Empty ? control.ogrnTxt.Text : null,
                                        PaymentAccount = control.rsTxt.Text != string.Empty ? control.rsTxt.Text : null,
                                        CorrAccount = control.ksTxt.Text != string.Empty ? control.ksTxt.Text : null,
                                        Bik = control.bikTxt.Text != string.Empty ? control.bikTxt.Text : null,
                                        GeneralManager = control.directorTxt.Text != string.Empty ? control.directorTxt.Text : null,
                                        ChiefAccountant = control.gbTxt.Text != string.Empty ? control.gbTxt.Text : null,
                                        ContactPerson = control.klTxt.Text != string.Empty ? control.klTxt.Text : null,
                                        ContactPersonPhone = control.phoneKlTxt.Text != string.Empty ? control.phoneKlTxt.Text : null,
                                        Cars = control.carsList,
                                        DateAdd = DateTime.Now.Date
                                    };

                                    string er = string.Empty;
                                    if (client.AddEntityClient(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString()))
                                    {
                                        MessageBox.Show("Новый клиент успешно добавлен");
                                        this.Close();
                                    }
                                    else
                                        MessageBox.Show($"При добавлении клиента произошла ошибка\n{er}"); 
                                }
                                else
                                {
                                    if (EntityClient.UpdateEntityClient(ConnStr, control.carsList, control.nameTxt.Text, control.phoneTxt.Text, control.faTxt.Text, 
                                                                    control.laTxt.Text, control.innTxt.Text, control.ogrnTxt.Text, control.rsTxt.Text, control.bikTxt.Text,
                                                                    control.gbTxt.Text, control.directorTxt.Text, control.klTxt.Text, control.phoneKlTxt.Text))
                                    {
                                        MessageBox.Show("Информация о клиенте успешно обновлена");
                                        this.Close();
                                    }

                                    else
                                    {
                                        MessageBox.Show($"При обновлении информации о клиенте произошла ошибка");
                                    }
                                }
                            }
                            else
                                MessageBox.Show("Не выбран(ы) автомобиль(и) клиента");
                        }
                        else
                            MessageBox.Show("Не указано название юр.лица");
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowTitle();
            AddControls();

            if (IndivClient != null || EntityClient != null)
            {
                topBtnsPanel.IsEnabled = false;
                addBtn.Content = "Сохранить";

                if (IndivClient != null)                
                    phBtn.IsChecked = true;

                if (EntityClient != null)
                    entBtn.IsChecked = true;
            }
        }
    }
}
