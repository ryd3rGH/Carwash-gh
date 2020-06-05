using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
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
    /// Interaction logic for CarDictWindow.xaml
    /// </summary>
    public partial class CarDictWindow : Window, IWindow
    {
        private bool UsingCustomCategories { get; set; }
        private CarBrand SelectedBrand { get; set; }
        private List<CarBrand> Brands { get; set; }
        private List<CarCategory> Categories { get; set; }
        private List<CarModel> Models { get; set; }

        public CarDictWindow()
        {
            InitializeComponent();
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
            this.Title = rm.GetString("CarDictWindowName");
        }

        private void UseCustomCategories()
        {
            if (Convert.ToBoolean(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("Custom categories")) == true)
            {
                UsingCustomCategories = true;
                customCategoriesChBox.IsChecked = true;
            }

            else
            {
                UsingCustomCategories = false;
                customCategoriesChBox.IsChecked = false;
            }
        }

        private void ShowBrands()
        {
            brandsComboBox.IsEnabled = false;
            modelsComboBox.IsEnabled = false;

            using (BackgroundWorker brandsWorker = new BackgroundWorker())
            {
                brandsWorker.DoWork += BrandsWorker_DoWork;
                brandsWorker.RunWorkerCompleted += BrandsWorker_RunWorkerCompleted;
                brandsWorker.RunWorkerAsync();
            }
        }

        private void BrandsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Brands = XMLWorker.ReadBrands();
        }

        private void BrandsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            brandsComboBox.IsEnabled = true;
            modelsComboBox.IsEnabled = true;

            brandsComboBox.ItemsSource = null;
            brandsComboBox.Items.Clear();
            brandsComboBox.ItemsSource = Brands;
        }       

        private void ShowCategories()
        {
            btnsPanel.IsEnabled = false;

            using (BackgroundWorker catWorker = new BackgroundWorker())
            {
                catWorker.DoWork += CatWorker_DoWork;
                catWorker.RunWorkerCompleted += CatWorker_RunWorkerCompleted;
                catWorker.RunWorkerAsync();
            }
        }

        private void CatWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Categories = DBWorker.CategoriesSearch(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
        }

        private void CatWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            categoriesPanel.Children.Clear();

            if (Categories != null && Categories.Count > 0)
            {
                for (int i = 0; i < Categories.Count; i++)
                {
                    CategoryControl category = new CategoryControl(Categories[i]);
                    category.Categories = Categories;
                    category.ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
                    category.updateBtn.Click += UpdateBtn_Click;
                    category.delBtn.Click += DelBtn_Click;
                    categoriesPanel.Children.Add(category);
                }
            }

            btnsPanel.IsEnabled = true;
        }        

        private void ShowModels()
        {
            brandsComboBox.IsEnabled = false;
            modelsComboBox.IsEnabled = false;

            contentPanel.Visibility = Visibility.Hidden;
            modelsComboBox.ItemsSource = null;

            if (brandsComboBox.SelectedIndex != -1)
            {
                SelectedBrand = (CarBrand)brandsComboBox.SelectedItem;

                using (BackgroundWorker modelsWorker = new BackgroundWorker())
                {
                    modelsWorker.DoWork += ModelsWorker_DoWork;
                    modelsWorker.RunWorkerCompleted += ModelsWorker_RunWorkerCompleted;
                    modelsWorker.RunWorkerAsync();
                }
            }
        }

        private void ModelsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Models = XMLWorker.ReadAndSearchModels(SelectedBrand);
        }

        private void ModelsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            modelsComboBox.ItemsSource = null;
            modelsComboBox.Items.Clear();
            modelsComboBox.ItemsSource = Models;

            brandsComboBox.IsEnabled = true;
            modelsComboBox.IsEnabled = true;
        }        

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCategories();
        }

        private void ChangeCustomCategoriesState(bool value)
        {
            RegistryKey useCustomCategoriesKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\CWM");
            useCustomCategoriesKey.SetValue("Custom Categories", value);
            useCustomCategoriesKey.Close();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCategories();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowBackGround();
            SetWindowTitle();
            SetFontSize();

            ShowCategories();
            ShowBrands();
            UseCustomCategories();

            contentPanel.Visibility = Visibility.Hidden;            
        }

        private void addCatBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCategoryWindow newCat = new NewCategoryWindow();
            newCat.Owner = this;

            if (UsingCustomCategories)
                newCat.UsedCategories = Categories;

            newCat.addBtn.Click += AddBtn_Click;
            newCat.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCategories();
        }

        private void customCategoriesChBox_Checked(object sender, RoutedEventArgs e)
        {
            ChangeCustomCategoriesState(true);
        }

        private void customCategoriesChBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeCustomCategoriesState(false);
        }

        private void brandsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentPanel.Visibility = Visibility.Hidden;
            ShowModels();
        }

        private void modelsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentPanel.Visibility = Visibility.Visible;

            if (modelsComboBox.SelectedIndex != -1)
            {
                CarModel model = (CarModel)modelsComboBox.SelectedItem;
                descrTxt.Text = model.Description;
                classLbl.Content = model.Class.Name;
                CarCategory category = DBWorker.FindCategoryByClassId(model.Class.Id, System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
                if (category != null)
                    categoryLbl.Content = category.Name;
                else
                    categoryLbl.Content = "----------";
            }
            else
                contentPanel.Visibility = Visibility.Hidden;
        }
    }
}
