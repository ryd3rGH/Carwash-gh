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
    /// Interaction logic for NewBoxModWindow.xaml
    /// </summary>
    public partial class NewBoxModWindow : Window, IWindow
    {
        public Box BoxToUpdate { get; set; }
        private string ConnStr { get; set; }

        public NewBoxModWindow()
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("NewBoxWindowName");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (BoxToUpdate != null)
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                addBtn.Style = this.FindResource("SaveButton") as Style;
                ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
                winNameLbl.Content = rm.GetString("UpdateBoxLabelText");

                rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
                this.Title = rm.GetString("UpdateBoxWindowName");
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                boxNameTxt.Text = BoxToUpdate.Name;

                busyBtn.IsChecked = (bool)BoxToUpdate.State ? false : true;
                notbusyBtn.IsChecked = (bool)BoxToUpdate.State ? true : false;

                IsWorkingBtn.IsChecked = (bool)BoxToUpdate.TechState ? true : false;
                IsNotWorkingBtn.IsChecked = (bool)BoxToUpdate.TechState ? false : true;

                delBtn.Visibility = Visibility.Visible;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
                winNameLbl.Content = rm.GetString("NewBoxLabelText");                

                busyBtn.IsEnabled = false;
                notbusyBtn.IsEnabled = false;
            }

            SetWindowBackGround();
            SetWindowTitle();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = boxNameTxt.Text == string.Empty ? "Бокс" : boxNameTxt.Text;
            bool techState = (bool)IsWorkingBtn.IsChecked ? true : false;
            bool addingResult = false;

            if (BoxToUpdate == null)
            {
                Box box = new Box();
                box.AddBox(name, techState, ConnStr, out addingResult);

                if (addingResult && box != null)
                {
                    MessageBox.Show("Новый бокс успешно добавлен");
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"При добавлении нового бокса\nпроизошла ошибка");
                }
            }

            else if (BoxToUpdate != null)
            {
                if (BoxToUpdate.UpdateBox(ConnStr, name, techState))
                {
                    MessageBox.Show("Информация о боксе обновлена");
                    this.Close();
                }
                else
                    MessageBox.Show("При обновлении информации о\nбоксе произошла ошибка");
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!BoxToUpdate.DeleteBox(ConnStr))
                MessageBox.Show("Ошибка при удалении бокса");
            else
            {
                MessageBox.Show("Бокс удален");
                this.Close();
            }                
        }
    }
}
