using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewCarwashOrderWindow.xaml
    /// </summary>
    public partial class NewCarwashOrderWindow : Window, IWindow, IOrder
    {
        public CarwashOrder OrderToUpdate { get; set; }
        private decimal ServicesSum { get; set; }
        private string ConnStr { get; set; }
        private List<Worker> Workers {get;set;}
        private ClientCar Car { get; set; }
        private List<CarwashService> SelectedServices { get; set; }
        private CarCategory UsedCategory { get; set; }

        private ClientCar SelectedCar { get; set; }

        private int? orderId;

        public NewCarwashOrderWindow(ClientCar car)
        {
            Car = car;
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            ServicesSum = 0;

            InitializeComponent();

            SetWindowTitle();
            SetWindowBackGround();
        }

        public NewCarwashOrderWindow(CarwashOrder order)
        {
            OrderToUpdate = order;
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            ServicesSum = 0;

            InitializeComponent();

            SetWindowTitle();
            SetWindowBackGround();            
        }

        public void GetCarBrands()
        {
            brandsList.ItemsSource = null;
            brandsList.Items.Clear();

            brandsList.ItemsSource = XMLWorker.ReadBrands();           
        }

        public void GetClientGroups()
        {
            groupsList.ItemsSource = null;
            groupsList.Items.Clear();

            groupsList.ItemsSource = DBWorker.GroupsSearch(ConnStr);

            if (groupsList.Items.Count > 0)
                groupsList.SelectedIndex = 0;
        }        

        public void GetClientInfo()
        {
            if (OrderToUpdate == null)
            {
                licencePlateTxt.Text = Car.LicencePlate;

                /* Существующий клиент */
                if (Car != null && Car.PersonId != null)
                {
                    stPanel.IsEnabled = false;

                    dynamic client = null;
                    SelectedCar = new ClientCar();

                    if (DBWorker.EntityClientCheck(ConnStr, (int)Car.PersonId))
                    {
                        entityClientBtn.IsChecked = true;

                        client = new EntityClient() { Id = Car.PersonId };
                        client.GetClientInfoById(ConnStr);
                    }
                    else
                    {
                        physClientBtn.IsChecked = true;

                        client = new IndividualClient() { Id = Car.PersonId };
                        client.GetClientInfoById(ConnStr);
                    }

                    nameTxt.Text = client.Name;
                    phoneTxt.Text = client.Phone;
                    mailTxt.Text = client.Email;

                    if (client.GetType() == typeof(EntityClient))
                        SelectedCar = ((EntityClient)client).Cars.Find(x => x.Car.Id == this.Car.Car.Id);
                    else
                        SelectedCar = ((IndividualClient)client).Cars.Find(x => x.Car.Id == this.Car.Car.Id);

                    for (int i = 0; i < brandsList.Items.Count; i++)
                    {
                        if (((CarBrand)brandsList.Items[i]).Id == SelectedCar.Car.Brand.Id)
                            brandsList.SelectedIndex = i;
                    }

                    if (modelsList.Items.Count > 0)
                    {
                        for (int i = 0; i < modelsList.Items.Count; i++)
                        {
                            if (((CarModel)modelsList.Items[i]).Id == SelectedCar.Car.Id)
                                modelsList.SelectedIndex = i;
                        }
                    }
                }

                /* Новый клиент */
                else
                {
                    physClientBtn.IsChecked = true;
                }                 
            }
            else
            {
                /* клиент */

                dynamic client = null;
                bool isEntity = DBWorker.EntityClientCheck(ConnStr, (int)OrderToUpdate.PersonId);

                entityClientBtn.IsEnabled = false;
                physClientBtn.IsEnabled = false;

                if (isEntity)
                {
                    client = new EntityClient() { Id = (int)OrderToUpdate.PersonId };
                    ((EntityClient)client).GetClientInfoById(ConnStr);

                    SelectedCar = ClientCar.GetCarByIdClientCar(ConnStr, OrderToUpdate.IdClientsCar);
                    entityClientBtn.IsChecked = true;

                    nameTxt.Text = client.Name;
                    licencePlateTxt.Text = ClientCar.GetCarByIdClientCar(ConnStr, OrderToUpdate.IdClientsCar).LicencePlate;

                    if (brandsList.Items.Count > 0)
                    {
                        for (int i = 0; i < brandsList.Items.Count; i++)
                        {
                            if (((CarBrand)brandsList.Items[i]).Id == SelectedCar.Car.Brand.Id)
                                brandsList.SelectedItem = brandsList.Items[i];
                        }
                    }
                }

                else
                {
                    client = new IndividualClient() { Id = (int)OrderToUpdate.PersonId };
                    ((IndividualClient)client).GetClientInfoById(ConnStr);

                    SelectedCar = ClientCar.GetCarByIdClientCar(ConnStr, OrderToUpdate.IdClientsCar);
                    physClientBtn.IsChecked = true;

                    nameTxt.Text = ((IndividualClient)client).Name;
                    phoneTxt.Text = ((IndividualClient)client).Phone;
                    mailTxt.Text = ((IndividualClient)client).Email;

                    licencePlateTxt.Text = ClientCar.GetCarByIdClientCar(ConnStr, OrderToUpdate.IdClientsCar).LicencePlate;

                    if (brandsList.Items.Count > 0)
                    {
                        for (int i = 0; i < brandsList.Items.Count; i++)
                        {
                            if (((CarBrand)brandsList.Items[i]).Id == SelectedCar.Car.Brand.Id)
                                brandsList.SelectedItem = brandsList.Items[i];
                        }
                    }
                }               
            }
        }

        public void GetServices()
        {
            serviceSumLbl.Content = "0,00 руб.";

            SelectedServices = new List<CarwashService>();

            serviceTypesList.ItemsSource = null;
            serviceTypesList.Items.Clear();

            serviceTypesList.ItemsSource = ServiceType.GetCWTypes(ConnStr);

            if (serviceTypesList.Items.Count > 0)
                serviceTypesList.SelectedIndex = 0;

            if (OrderToUpdate != null)
            {
                /* услуги */
                SelectedServices = OrderToUpdate.Services;
                ServicesSum = CalculateSum(SelectedServices);
                serviceTypesList_SelectionChanged(null, null);
            }
        }

        public void GetWorkers()
        {
            Workers = new List<Worker>();
            workersNumLbl.Content = Workers.Count;
            List<Worker> workers = new List<Worker>();

            if (OrderToUpdate == null)            
                workers = Worker.GetWorkersByGroupNotBusy(ConnStr, 3);
            else
                workers = Worker.GetWorkersByGroup(ConnStr, 3);

            if (workers.Count > 0)
            {
                for (int i = 0; i < workers.Count; i++)
                {
                    OrderWorkerControl worker = new OrderWorkerControl(workers[i]);
                    worker.nameBox.Checked += NameBox_Checked;
                    worker.nameBox.Unchecked += NameBox_Unchecked;
                    workersPanel.Children.Add(worker);
                }
            }

            if (OrderToUpdate != null)
            {
                /* работники */
                if (OrderToUpdate.Workers.Count > 0)
                {
                    for (int i = 0; i < workersPanel.Children.Count; i++)
                    {
                        if (workersPanel.Children[i].GetType() == typeof(OrderWorkerControl))
                        {
                            OrderWorkerControl worker = (OrderWorkerControl)workersPanel.Children[i];
                            for (int j = 0; j < OrderToUpdate.Workers.Count; j++)
                            {
                                if (worker.Worker.Id == OrderToUpdate.Workers[j].Id)
                                    worker.nameBox.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }

        private void SelectCarInExistOrder()
        {
            if (OrderToUpdate != null)
            {
                if (modelsList.Items.Count > 0)
                {
                    for (int i = 0; i < modelsList.Items.Count; i++)
                    {
                        if (((CarModel)modelsList.Items[i]).Id == SelectedCar.Car.Id)
                            modelsList.SelectedItem = modelsList.Items[i];
                    }
                }
            }
        }

        private void NameBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Workers.Count > 0)
            {
                Workers.Remove(((OrderWorkerControl)((Grid)((Control)sender).Parent).Parent).GetWorker());
            }

            workersNumLbl.Content = Workers.Count;
        }

        private void NameBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Workers != null)
            {
                Worker newWorker = Workers.SingleOrDefault(x => x.Id == ((OrderWorkerControl)((Grid)((Control)sender).Parent).Parent).GetWorker().Id);
                if (newWorker == null)
                    Workers.Add(((OrderWorkerControl)((Grid)((Control)sender).Parent).Parent).GetWorker());
            }

            workersNumLbl.Content = Workers.Count;
        }

        public void GetAdditionalInfo()
        {
            /* Загрузка боксов */
            boxList.ItemsSource = null;
            boxList.Items.Clear();

            List<Box> boxesList = new List<Box>();
            DBWorker.BoxesSearch(ConnStr, out boxesList);

            boxList.ItemsSource = boxesList;

            if (OrderToUpdate != null)
            {
                for (int i = 0; i < boxList.Items.Count; i++)
                {
                    if (((Box)boxList.Items[i]).Id == OrderToUpdate.Box.Id)
                        boxList.SelectedItem = boxList.Items[i];
                }
            }

            /* Загрузка типов оплаты */
            moneyList.ItemsSource = null;
            moneyList.Items.Clear();

            moneyList.ItemsSource = MoneyType.GetMoneyTypes(ConnStr);

            if (moneyList.Items.Count > 0)
                moneyList.SelectedIndex = 0;
        }

        public void IsEntityClientOrder(bool isEntity)
        {
            if (isEntity)
            {
                nameLbl.Content = "Наименование:";
                groupLbl.Visibility = Visibility.Hidden;
                groupsList.Visibility = Visibility.Hidden;
            }
            else
            {
                nameLbl.Content = "Ф.И.О.:";
                groupLbl.Visibility = Visibility.Visible;
                groupsList.Visibility = Visibility.Visible;
            }
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            if (OrderToUpdate == null)
                this.Title = rm.GetString("NewCarwashOrderWindowName");
            else
                this.Title = rm.GetString("UpdateCarwashOrderWindowName") + $" №{OrderToUpdate.Id}";
        }

        private void physClientBtn_Checked(object sender, RoutedEventArgs e)
        {
            IsEntityClientOrder(false);
        }

        private void entityClientBtn_Checked(object sender, RoutedEventArgs e)
        {
            IsEntityClientOrder(true);
        }

        private void brandsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brandsList.SelectedIndex != -1)
            {
                servicesPanel.Children.Clear();

                modelsList.ItemsSource = null;
                modelsList.Items.Clear();

                modelsList.ItemsSource = XMLWorker.ReadAndSearchModels((CarBrand)brandsList.SelectedItem);
            }
        }

        private void modelsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelsList.SelectedIndex != -1)
            {
                SelectedCar = new ClientCar();
                SelectedCar.LicencePlate = String.IsNullOrWhiteSpace(licencePlateTxt.Text) != true ? licencePlateTxt.Text : "#########";
                SelectedCar.Car = XMLWorker.SearchModelById((int)((CarModel)modelsList.SelectedItem).Id);
                SelectedCar.Car.Category = DBWorker.FindCategoryByClassId(SelectedCar.Car.Class.Id, ConnStr);
                serviceTypesList_SelectionChanged(null, null);
            }
        }

        private void serviceTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelsList.SelectedIndex != -1)
            {
                if (serviceTypesList.SelectedIndex != -1)
                {
                    servicesPanel.Children.Clear();
                    List<CarwashService> services = new List<CarwashService>();
                    services = CarwashService.GetServices(ConnStr,
                                                        ((ServiceType)serviceTypesList.SelectedItem).Id,
                                                        Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom Categories")));

                    if (services.Count > 0)
                    {
                        for (int i = 0; i < services.Count; i++)
                        {
                            OrderServiceControl service = null;
                            if (Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom Categories")))
                                service = new OrderServiceControl(services[i], (int)SelectedCar.Car.Category.Id);
                            else
                                service = new OrderServiceControl(services[i], SelectedCar.Car.Class.Id);

                            service.serviceNameBox.Checked += ServiceNameBox_Checked;
                            service.serviceNameBox.Unchecked += ServiceNameBox_Unchecked;
                            service.minBtn.Click += MinBtn_Click;
                            service.plusBtn.Click += PlusBtn_Click;
                            servicesPanel.Children.Add(service);
                        }
                    }

                    /* При переключении типов услуг, если до этого были выбраны какие-либо услуги, 
                     * выбрать их в списке и установить нужные значения */
                    if (SelectedServices != null && SelectedServices.Count > 0)
                    {
                        for (int i=0; i<servicesPanel.Children.Count; i++)
                        {
                            for (int j=0; j<SelectedServices.Count; j++)
                            {
                                if (((OrderServiceControl)servicesPanel.Children[i]).GetService().Id == SelectedServices[j].Id)
                                {
                                    OrderServiceControl service = (OrderServiceControl)servicesPanel.Children[i];
                                    service.serviceNameBox.IsChecked = true;
                                    service.GetService().OrderServiceCount = SelectedServices[j].OrderServiceCount;
                                    service.numTxt.Text = SelectedServices[j].OrderServiceCount.ToString();                                    
                                }
                            }
                        }

                        serviceSumLbl.Content = ServicesSum + " руб.";
                        wholeSumLbl.Content = ServicesSum + " руб.";
                    }
                }
                else
                    servicesPanel.Children.Clear();
            }
            else
                servicesPanel.Children.Clear();
        }

        private void ServiceNameBox_Checked(object sender, RoutedEventArgs e)
        {
            CarwashService foundedService = SelectedServices.SingleOrDefault(x => x.Id == ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetService().Id);
            if (foundedService == null)
            {
                SelectedServices.Add(((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetService());
                ServicesSum += ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetCurrentPrice();
                serviceSumLbl.Content = ServicesSum + " руб.";
                wholeSumLbl.Content = ServicesSum + " руб.";
            }
        }

        private void ServiceNameBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SelectedServices.Count > 0)
            {
                CarwashService serviceToRemove = ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetService();
                var foundedServiceInList = SelectedServices.Find(x => x.Id == serviceToRemove.Id);
                SelectedServices.Remove(foundedServiceInList);
            }

            ServicesSum -= ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetCurrentPrice();
            serviceSumLbl.Content = ServicesSum + " руб.";
            wholeSumLbl.Content = ServicesSum + " руб.";
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetCount() >= 0 &&
                ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetCurrentPrice() > 0)
            {
                ServicesSum -= ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetSinglePrice();
                serviceSumLbl.Content = Math.Round(ServicesSum, 2).ToString("n2").Replace('.', ',') + " руб.";
                wholeSumLbl.Content = Math.Round(ServicesSum, 2).ToString("n2").Replace('.', ',') + " руб.";
            }
            else
            {
                if (ServicesSum > 0)
                {
                    ServicesSum -= ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetSinglePrice();
                    serviceSumLbl.Content = Math.Round(ServicesSum, 2).ToString("n2").Replace('.', ',') + " руб.";
                    wholeSumLbl.Content = Math.Round(ServicesSum, 2).ToString("n2").Replace('.', ',') + " руб.";
                }
            }               
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetCount() < 100)
            {
                ServicesSum += ((OrderServiceControl)((Grid)((Control)sender).Parent).Parent).GetSinglePrice();
                serviceSumLbl.Content = Math.Round(ServicesSum, 2).ToString("n2").Replace('.', ',') + " руб.";
                wholeSumLbl.Content = Math.Round(ServicesSum, 2).ToString("n2").Replace('.', ',') + " руб.";
            }            
        }

        private void discountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            discountValuelbl.Content = (int)discountSlider.Value + "%";
            wholeSumLbl.Content = (ServicesSum - Math.Round((ServicesSum / 100) * (decimal)discountSlider.Value, 2)).ToString().Replace('.', ',') + " руб.";
        }

        private decimal CalculateSum(List<CarwashService> services)
        {
            decimal calculatedSum = 0;

            for (int i=0; i<services.Count; i++)
            {
                if (services[i].UsedPrice != null && services[i].UsedPrice != 0)
                    calculatedSum += (decimal)services[i].UsedPrice * (int)services[i].OrderServiceCount;
            }

            return calculatedSum;
        }

        private void addOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OrderToUpdate == null)
            {
                if (nameTxt.Text != string.Empty && !String.IsNullOrWhiteSpace(nameTxt.Text))
                {
                    dynamic client = null;
                    bool addClientResult = false;

                    if (Car.PersonId == null)
                    {
                        if ((bool)physClientBtn.IsChecked)
                        {
                            client = new IndividualClient()
                            {
                                Name = nameTxt.Text,
                                BirthDate = null,
                                Phone = String.IsNullOrWhiteSpace(phoneTxt.Text) != true ? phoneTxt.Text : null,
                                Email = String.IsNullOrWhiteSpace(mailTxt.Text) != true ? mailTxt.Text : null,
                                Group = groupsList.SelectedIndex != -1 ? (ClientGroup)groupsList.SelectedItem : (ClientGroup)groupsList.Items[0],
                                Cars = new List<ClientCar>() { SelectedCar },
                                DateAdd = DateTime.Now.Date
                            };

                            addClientResult = ((IndividualClient)client).AddIndividualClient(ConnStr);
                        }
                        else if ((bool)entityClientBtn.IsChecked)
                        {
                            client = new EntityClient()
                            {
                                Name = nameTxt.Text,
                                Phone = String.IsNullOrWhiteSpace(phoneTxt.Text) ? phoneTxt.Text : null,
                                Cars = new List<ClientCar>() { SelectedCar },
                                DateAdd = DateTime.Now.Date
                            };

                            addClientResult = ((EntityClient)client).AddEntityClient(ConnStr);
                        }
                    }

                    else
                    {
                        if ((bool)physClientBtn.IsChecked)
                            client = new IndividualClient() { Id = Car.PersonId };

                        else if ((bool)entityClientBtn.IsChecked)
                            client = new EntityClient() { Id = Car.PersonId };

                        addClientResult = true;
                    }

                    /* Установить каждой выбранной услуге необходимый класс или категорию авто */
                    for (int i = 0; i < SelectedServices.Count; i++)
                    {
                        if (Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom Categories")))
                            SelectedServices[i].UsedCarCategory = (int)SelectedCar.Car.Category.Id;
                        else
                            SelectedServices[i].UsedCarClass = (int)SelectedCar.Car.Class.Id;
                    }

                    if (addClientResult)
                    {
                        if (SelectedCar != null)
                        {
                            if (boxList.SelectedIndex != -1)
                            {
                                if (Workers.Count > 0)
                                {
                                    if (SelectedServices.Count > 0)
                                    {
                                        if (moneyList.SelectedIndex != -1)
                                        {
                                            /* Client ++ */
                                            /* Car ++ */
                                            /* Workers ++ */
                                            /* Services ++ */
                                            /* Создание заказа */

                                            CarwashOrder order = new CarwashOrder()
                                            {
                                                Box = (Box)boxList.SelectedItem,
                                                PersonId = (int)((PersonBase)client).Id,
                                                MoneyType = (MoneyType)moneyList.SelectedItem,
                                                IdClientsCar = (bool)physClientBtn.IsChecked
                                                            ? ((IndividualClient)client).GetOrderClientsCar(ConnStr, licencePlateTxt.Text, (int)((CarModel)modelsList.SelectedItem).Id)
                                                            : ((EntityClient)client).GetOrderClientsCar(ConnStr, licencePlateTxt.Text, (int)((CarModel)modelsList.SelectedItem).Id),
                                                Comment = commentTxt.Text != string.Empty ? commentTxt.Text : null,
                                                Workers = Workers,
                                                Services = SelectedServices,
                                                Sum = CalculateSum(SelectedServices),
                                                UsingCustomCategories = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom Categories"))
                                            };

                                            string err = string.Empty;

                                            if (order.AddOrder(ConnStr, out orderId, out err))
                                            {
                                                for (int i = 0; i < Workers.Count; i++)
                                                    Workers[i].SetWorkerBusyState(ConnStr, true);

                                                order.Box.SetBoxBusyState(ConnStr, false);

                                                MessageBox.Show($"Заказ №{orderId} успешно добавлен");
                                                this.Close();
                                            }
                                            else
                                                MessageBox.Show($"При добавлении заказа возникла ошибка\n{err}");
                                        }
                                        else
                                            MessageBox.Show("Не выбран тип оплаты");
                                    }
                                    else
                                        MessageBox.Show("Не выбрано ни одной услуги");
                                }
                                else
                                    MessageBox.Show("Не выбраны работники");
                            }
                            else
                                MessageBox.Show("Не выбран бокс");
                        }
                        else
                            MessageBox.Show("Не выбран автомобиль клиента");
                    }
                    else
                        MessageBox.Show("Возникла ошибка при добавлении клиента");
                }
                else
                {
                    if ((bool)physClientBtn.IsChecked)
                        MessageBox.Show("Не указано имя клиента");
                    else
                        MessageBox.Show("Не указано наименование клиента");
                }
            }
            
            else
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetCarBrands();
            GetClientGroups();
            GetClientInfo();
            SelectCarInExistOrder();
            GetWorkers();
            GetServices();
            GetAdditionalInfo();

            if (OrderToUpdate != null)
            {
                addOrderBtn.Content = "Сохранить";              
            }
        }
    }
}
