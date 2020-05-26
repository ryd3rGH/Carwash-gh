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
using CarwashManager.Classes;
using System.Resources;
using System.Reflection;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewServiceCategoryWindow.xaml
    /// </summary>
    public partial class NewServiceCategoryWindow : Window, IWindow
    {
        public NewServiceCategoryWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();
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
            this.Title = rm.GetString("NewServiceCategoryWindowName");
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTxt.Text != string.Empty && !String.IsNullOrWhiteSpace(nameTxt.Text))
            {
                ServiceType type = new ServiceType()
                {
                    IsCarwashService = true,
                    Name = nameTxt.Text
                };
                if (type.AddCategory(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString())) 
                {
                    MessageBox.Show("Новая категория услгу успешно добавлена");
                    this.Close();
                }                    
                else
                    MessageBox.Show("При добавлении новой\nкатегории услуг произошла ошибка");
            }
            else
                MessageBox.Show("Не указано имя новой категории");
        }
    }
}
