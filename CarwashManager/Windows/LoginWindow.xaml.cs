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
using Microsoft.Win32;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IWindow
    {
        string ConnStr { get; set; }

        public LoginWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();
            SetFontSize();

            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
        }

        public void SetFontSize()
        {
            for (int i = 0; i < mainGrid.Children.Count; i++)
                ((Control)mainGrid.Children[i]).FontSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MainFontSize"]);
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("LoginWindowName");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> usersNames = DBWorker.FindUsers(ConnStr);
            if (usersNames.Count > 0)
            {
                for (int i = 0; i < usersNames.Count; i++)
                    loginsComboBox.Items.Add(usersNames[i]);
            }
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (loginsComboBox.Items.Count > 0 && loginsComboBox.SelectedIndex != -1)
                {
                    if (passTxt.Password.Length > 0 && !String.IsNullOrEmpty(passTxt.Password) && !String.IsNullOrWhiteSpace(passTxt.Password))
                    {
                        int? workerId;
                        if (DBWorker.Authentication(ConnStr, loginsComboBox.Text, passTxt.Password, out workerId, Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("SLT").ToString()))
                        {
                            MainWindow main = new MainWindow();
                            main.WorkerId = (int)workerId;
                            this.Close();
                            main.ShowDialog();                            
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не введен пароль или пароль состоит из пробелов");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
