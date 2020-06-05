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
using CarwashManager.Windows;
using CWLib;
using Microsoft.Win32;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for LaunchWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window, IWindow
    {
        private string ConnStr { get; set; }
        private string ReportString { get; set; }

        public LaunchWindow()
        {
            InitializeComponent();
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
            this.Title = rm.GetString("LaunchWindowName");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            SetWindowTitle();
            SetFontSize();

            /* TODO: сделать все нижеописанные операции в потоках, чтоб не фризило интерфейс */
            /* TODO: облагородить интерфейс */

            using (BackgroundWorker launchWorker = new BackgroundWorker())
            {
                launchWorker.WorkerReportsProgress = true;
                launchWorker.WorkerSupportsCancellation = true;

                launchWorker.ProgressChanged += LaunchWorker_ProgressChanged;
                launchWorker.DoWork += LaunchWorker_DoWork;
                launchWorker.RunWorkerCompleted += LaunchWorker_RunWorkerCompleted;
                launchWorker.RunWorkerAsync();
            }         
        }

        private void LaunchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            /* Проверка наличия ресурсов*/
            ((BackgroundWorker)sender).ReportProgress(0, "Проверка ресурсов...");
            if (System.IO.File.Exists("CWLib.dll"))
            {
                /* Проверка БД */
                ((BackgroundWorker)sender).ReportProgress(0, "Проверка наличия БД...");
                if (!DBWorker.CheckDBCreation(ConnStr))
                {
                    /* Создание БД */
                    ((BackgroundWorker)sender).ReportProgress(0, "Создание БД...");
                    DBWorker.CreateDB(ConnStr);

                    /* Создание таблиц */
                    ((BackgroundWorker)sender).ReportProgress(0, "Создание таблиц...");
                    DBWorker.CreateDBTables(ConnStr);

                    try
                    {
                        /* Security */
                        byte[] outBytes = null;
                        Protector protector = new Protector(null, out outBytes);

                        RegistryKey sltKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\CWM");
                        sltKey.SetValue("SLT", Encoding.Default.GetString(outBytes));
                        sltKey.Close();

                        /* Внесение базовой информации о группах работников */
                        ((BackgroundWorker)sender).ReportProgress(0, "Внесение базовой информации...");
                        DBWorker.AddInitialWorkersInfo(ConnStr);

                        /* Внесение информации о типах услуг */
                        DBWorker.AddInitialServicesInfo(ConnStr);

                        /* Внесение информации о типах оплаты */
                        DBWorker.AddPaymentCategories(ConnStr);

                        /* Внесение групп клиентов по ум. */
                        DBWorker.AddDefaultClientGroups(ConnStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }

                /* Проверка пользователей в БД */
                try
                {
                    if (!DBWorker.CheckWorkersAndUsers(ConnStr))
                    {
                        /* Если пользователей в программе не найдено - 
                        задать основную информацию и записать ее в БД */
                        DBWorker.AddUsersInitialInfo(ConnStr, Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CWM").GetValue("SLT").ToString());
                    }
                }
                catch (Exception ex)
                {
                    ((BackgroundWorker)sender).CancelAsync();
                    MessageBox.Show(ex.ToString());
                    throw;
                }

                /* Запуск логин-формы */               
            }
            else
                ((BackgroundWorker)sender).ReportProgress(0, "Файл ресурсов не найден!");
        }

        private void LaunchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.ShowDialog();
        }

        private void LaunchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            loadingLbl.Content = e.UserState.ToString();
        }               
    }
}
