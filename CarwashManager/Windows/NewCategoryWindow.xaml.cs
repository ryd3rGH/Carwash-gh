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
using System.Xml;
using CarwashManager.Classes;
using CWLib;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewCategoryWindow.xaml
    /// </summary>
    public partial class NewCategoryWindow : Window, IWindow
    {        
        public CarCategory CategoryToUpdate { get; set; }
        private string ConnStr { get; set; }
        public List<CarCategory> UsedCategories { get; set; }

        public NewCategoryWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();
            SetFontSize();
            ShowClasses(XMLWorker.ReadClasses());

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
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
            this.Title = rm.GetString("NewCategoryWindowName");
        } 
        
        private void BlockUsedClasses(List<CarCategory> categories)
        {
            List<int> usedClasses = new List<int>();

            if (categories != null)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    if (categories[i].Classes.Count > 0)
                    {
                        for (int j = 0; j < categories[i].Classes.Count; j++)
                        {
                            usedClasses.Add(categories[i].Classes[j]);
                        }
                    }
                }
            }

            if (classesPanel.Children.Count > 0)
            {
                for (int i = 0; i < classesPanel.Children.Count; i++)
                {
                    if (classesPanel.Children[i].GetType() == typeof(CheckBox))
                    {
                        CheckBox cls = (CheckBox)classesPanel.Children[i];

                        for (int j = 0; j < usedClasses.Count; j++)
                        {
                            if (cls.Tag.ToString() == usedClasses[j].ToString())
                                cls.IsEnabled = false;
                        }
                    }                    
                }
            }
        }

        private void BlockUsedClasses(List<CarCategory> categories, CarCategory categoryToUpdate)
        {
            BlockUsedClasses(categories);

            if (categoryToUpdate != null)
            {
                for (int i = 0; i < classesPanel.Children.Count; i++)
                {
                    if (classesPanel.Children[i].GetType() == typeof(CheckBox))
                    {
                        CheckBox cls = (CheckBox)classesPanel.Children[i];

                        for (int j = 0; j < categoryToUpdate.Classes.Count; j++)
                        {
                            if (cls.Tag.ToString() == categoryToUpdate.Classes[j].ToString())
                                cls.IsEnabled = true;
                        }
                    }
                }
            }
        }

        private void ShowClasses(List<CarClass> clss)
        {
            if (clss.Count > 0)
            {
                classesPanel.Children.Clear();

                for (int i=0; i<clss.Count; i++)
                {
                    CheckBox chBox = new CheckBox();
                    chBox.Content = clss[i].Name;
                    chBox.Tag = clss[i].Id;
                    chBox.IsChecked = false;
                    classesPanel.Children.Add(chBox);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CategoryToUpdate != null)
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                addBtn.Content = "Сохранить";

                ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
                this.Title = rm.GetString("UpdateCategoryWindowName");

                nameTxt.Text = CategoryToUpdate.Name;

                for (int i=0; i < CategoryToUpdate.Classes.Count; i++)
                {
                    for (int j=0; j<classesPanel.Children.Count; j++)
                    {
                        if (CategoryToUpdate.Classes[i] == Convert.ToInt32(((CheckBox)classesPanel.Children[j]).Tag))
                        {
                            CheckBox chBox = (CheckBox)classesPanel.Children[j];
                            chBox.IsChecked = true;
                        }
                    }
                }

                BlockUsedClasses(UsedCategories, CategoryToUpdate);
            }

            else
            {
                BlockUsedClasses(UsedCategories);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTxt.Text != string.Empty ? nameTxt.Text : "Категория undefined";

            List<int> classes = new List<int>();
            for(int i=0; i<classesPanel.Children.Count; i++)
            {
                if (classesPanel.Children[i].GetType() == typeof(CheckBox))
                {
                    CheckBox chb = (CheckBox)classesPanel.Children[i];
                    if ((bool)chb.IsChecked)
                        classes.Add(Convert.ToInt32(chb.Tag));
                }
            }

            if (CategoryToUpdate == null)
            {
                string er = string.Empty;
                CarCategory cat = new CarCategory() { Name = name, Classes = classes };
                if (cat.AddCategory(ConnStr))
                {
                    MessageBox.Show("Категория успешно добавлена");
                    this.Close();
                }
                else
                    MessageBox.Show($"При добавлении новой категории\nпроизошла ошибка"); 
            }  
            else
            {
                if (CategoryToUpdate.UpdateCategory(ConnStr, nameTxt.Text, classes))
                {
                    MessageBox.Show("Изменения категории успешно сохранены");
                    this.Close();
                }
                else
                    MessageBox.Show($"При изменении категории\nпроизошла ошибка");
            }
        }
    }
}
