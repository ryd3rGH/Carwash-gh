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
using CWLib;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewClientsGroupWindow.xaml
    /// </summary>
    public partial class NewClientsGroupWindow : Window, IWindow
    {
        public NewClientsGroupWindow()
        {
            InitializeComponent();

            SetFontSize();
            SetWindowBackGround();
            SetWindowTitle();
        }

        public void SetFontSize()
        {
            //for (int i = 0; i < mainGrid.Children.Count; i++)
            //    ((Control)mainGrid.Children[i]).FontSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MainFontSize"]);
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("NewClientGroupWindowName");
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = groupNameTxt.Text != string.Empty ? groupNameTxt.Text : "Группа по умолчанию";
            bool res = DBWorker.AddClientGroup(name, (int)discSlider.Value, System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
            if (res)
            {
                MessageBox.Show("Новая группа клиентов успешно добавлена");
                this.Close();
            }
            else
                MessageBox.Show($"При добавлении новой группы клиентов\nпроизошла ошибка");
        }
    }
}
