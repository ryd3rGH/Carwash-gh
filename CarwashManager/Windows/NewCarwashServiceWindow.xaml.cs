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
using CarwashManager.Controls;
using CarwashManager.Classes;
using System.Resources;
using System.Reflection;
using Microsoft.Win32;
using System.Linq;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewCarwashServiceWindow.xaml
    /// </summary>
    public partial class NewCarwashServiceWindow : Window, IWindow
    {
        public CarwashService ServiceToUpdate { get; set; }
        private string ConnStr { get; set; }
        private bool IsCustomCategoriesUsed { get; set; }
        private List<CarClass> OldCPrices { get; set; }     /* часть костыля, зато работает */
        private List<CarCategory> OldPrices { get; set; }   /* часть костыля, зато работает */

        public NewCarwashServiceWindow()
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            IsCustomCategoriesUsed = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom Categories"));

            InitializeComponent();

            SetWindowBackGround();            
            LoadTypes();
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            if (ServiceToUpdate == null)
                this.Title = rm.GetString("NewCarwashServiceWindowName");
            else
                this.Title = rm.GetString("UpdateCarwashServiceWindowName");
        }

        private void LockAddButton()
        {
            if (ServiceToUpdate == null)
                if (classesPanel.Children.Count < 1)            
                    addBtn.IsEnabled = false;                
        }

        private void LoadTypes()
        {
            typeList.ItemsSource = null;
            typeList.Items.Clear();

            typeList.ItemsSource = ServiceType.GetCWTypes(ConnStr);
        }

        private void AddClasses()
        {
            if (ServiceToUpdate == null)
            {
                ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WindowsResources", Assembly.GetExecutingAssembly());

                if (!IsCustomCategoriesUsed)
                {
                    sectionNameLbl.Content = rm.GetString("DefaultClassesUsed");

                    List<CarClass> classes = XMLWorker.ReadClasses();
                    for (int i = 0; i < classes.Count; i++)
                    {
                        classesPanel.Children.Add(
                            new ClassPriceControl() { carClass = classes[i] });
                    }
                }

                else
                {
                    sectionNameLbl.Content = rm.GetString("CustomCategoriesUsed");

                    List<CarCategory> categories = DBWorker.CategoriesSearch(ConnStr);
                    for (int i = 0; i < categories.Count; i++)
                    {
                        classesPanel.Children.Add(
                            new ClassPriceControl() { carCategory = categories[i] });
                    }
                } 
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTxt.Text != string.Empty && !String.IsNullOrWhiteSpace(nameTxt.Text))
            {
                if (durationTxt.Text != string.Empty && !String.IsNullOrWhiteSpace(durationTxt.Text))
                {
                    if (typeList.SelectedIndex != -1)
                    {
                        dynamic prices = null;

                        if (!IsCustomCategoriesUsed)
                        {
                            prices = new List<CarClass>();
                            for (int i = 0; i < classesPanel.Children.Count; i++)
                            {
                                if (classesPanel.Children[i].GetType() == typeof(ClassPriceControl))
                                {
                                    ((ClassPriceControl)classesPanel.Children[i]).SetPrice();
                                    prices.Add(((ClassPriceControl)classesPanel.Children[i]).carClass);
                                }
                            }
                        }
                        else
                        {
                            prices = new List<CarCategory>();
                            for (int i = 0; i < classesPanel.Children.Count; i++)
                            {
                                if (classesPanel.Children[i].GetType() == typeof(ClassPriceControl))
                                {
                                    ((ClassPriceControl)classesPanel.Children[i]).SetPrice();
                                    prices.Add(((ClassPriceControl)classesPanel.Children[i]).carCategory);
                                }
                            }
                        }

                        if (ServiceToUpdate == null)
                        {
                            CarwashService service = new CarwashService(null, nameTxt.Text, descrTxt.Text, TimeSpan.FromMinutes(Convert.ToDouble(durationTxt.Text)), true);
                            service.Type = (ServiceType)typeList.SelectedItem;
                            if (!IsCustomCategoriesUsed)                            
                                service.ClassPrices = prices;
                                
                            else                            
                                service.CategoryPrices = prices;
                                

                            string err = string.Empty;
                            if (service.AddNewService(ConnStr, IsCustomCategoriesUsed, out err))
                            {
                                MessageBox.Show("Новая услуга успешно добавлена");
                                this.Close();
                            }
                            else
                                MessageBox.Show($"При добавлении новой услуги произошла ошибка\n{err}"); 
                        }

                        else
                        {
                            bool workRes = false;

                            if (IsCustomCategoriesUsed)
                            {
                                 workRes = ServiceToUpdate.UpdateService(ConnStr, IsCustomCategoriesUsed, nameTxt.Text, descrTxt.Text,
                                                              new TimeSpan(0, Convert.ToInt32(durationTxt.Text), 0),
                                                              (ServiceType)typeList.SelectedItem, OldPrices, null);
                            }
                            else
                            {
                                workRes = ServiceToUpdate.UpdateService(ConnStr, IsCustomCategoriesUsed, nameTxt.Text, descrTxt.Text,
                                                              new TimeSpan(0, Convert.ToInt32(durationTxt.Text), 0),
                                                              (ServiceType)typeList.SelectedItem, null, OldCPrices);
                            }

                            if (workRes)
                            {
                                MessageBox.Show("Изменения успешно сохранены");
                                this.Close();
                            }

                            else
                                MessageBox.Show($"При сохранении изменений услуги произошла ошибка");
                        }
                    }
                    else
                        MessageBox.Show("Не выбрана категория для новой услуги");
                }
                else
                    MessageBox.Show("Не указана длительность новой услуги");
            }
            else
                MessageBox.Show("Не указано название новой услуги");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowTitle();
            AddClasses();            

            if (ServiceToUpdate != null)
            {
                addBtn.Content = "Сохранить";

                nameTxt.Text = ServiceToUpdate.Name;
                durationTxt.Text = ServiceToUpdate.Duration.Minutes.ToString();
                descrTxt.Text = ServiceToUpdate.Description;                

                for (int i = 0; i < typeList.Items.Count; i++)
                {
                    if (((ServiceType)typeList.Items[i]).Id == ServiceToUpdate.Type.Id)
                        typeList.SelectedItem = typeList.Items[i];
                }               

                if (ServiceToUpdate.ClassPrices != null && ServiceToUpdate.ClassPrices.Count > 0)
                {
                    for (int i = 0; i < ServiceToUpdate.ClassPrices.Count; i++)
                    {
                        ClassPriceControl price = new ClassPriceControl() { carClass = ServiceToUpdate.ClassPrices[i] };
                        classesPanel.Children.Add(price);
                    }
                }

                if (ServiceToUpdate.CategoryPrices != null && ServiceToUpdate.CategoryPrices.Count > 0)
                {
                    for (int i = 0; i < ServiceToUpdate.CategoryPrices.Count; i++)
                    {
                        ClassPriceControl price = new ClassPriceControl() { carCategory = ServiceToUpdate.CategoryPrices[i] };
                        classesPanel.Children.Add(price);
                    }
                }

                try
                {
                    OldCPrices = ServiceToUpdate.ClassPrices != null ? (List<CarClass>)Extesions.Clone(ServiceToUpdate.ClassPrices) : null;
                    OldPrices = ServiceToUpdate.CategoryPrices != null ? (List<CarCategory>)Extesions.Clone(ServiceToUpdate.CategoryPrices) : null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            }
        }
    }
}
