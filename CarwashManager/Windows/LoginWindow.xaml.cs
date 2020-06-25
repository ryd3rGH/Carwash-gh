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
using CWLib;
using Microsoft.Win32;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IWindow
    {
        private string ConnStr { get; set; }
        private List<User> usersNames { get; set; }

        public LoginWindow()
        {
            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();

            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
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

        private void SetUserRegistryKey(User user)
        {
            RegistryKey sltKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\CWM");
            sltKey.SetValue("User", user.Id);
            sltKey.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainGrid.IsEnabled = false;
            using (BackgroundWorker uWorker = new BackgroundWorker())
            {
                uWorker.DoWork += UWorker_DoWork;
                uWorker.RunWorkerCompleted += UWorker_RunWorkerCompleted;
                uWorker.RunWorkerAsync();
            }
        }

        private void UWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            usersNames = DBWorker.FindUsers(ConnStr);
        }

        private void UWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (usersNames.Count > 0)            
                loginsComboBox.ItemsSource = usersNames;

            mainGrid.IsEnabled = true;
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
                        if (DBWorker.Authentication(ConnStr, ((User)loginsComboBox.SelectedItem).LoginName, passTxt.Password, out workerId, Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("SLT").ToString()))
                        {
                            SetUserRegistryKey((User)loginsComboBox.SelectedItem);

                            //MainWindow main = new MainWindow();
                            //main.WorkerId = (int)workerId;
                            //this.Close();
                            //main.ShowDialog();   

                            CWMainWindow main = new CWMainWindow();
                            main.PersonId = (int)workerId;
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
