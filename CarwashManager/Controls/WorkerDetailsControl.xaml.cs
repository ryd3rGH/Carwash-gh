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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarwashManager.Windows;
using CWLib;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for WorkerDetailsControl.xaml
    /// </summary>
    public partial class WorkerDetailsControl : UserControl
    {
        private Worker Worker { get; set; }

        public WorkerDetailsControl(Worker worker)
        {
            InitializeComponent();

            this.Worker = worker;            
        }

        private void RefreshInfo()
        {
            Worker.GetWorkerInfoById(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());

            nameTxt.Text = Worker.Name != null ? Worker.Name : "------";
            phoneTxt.Text = Worker.Phone != null ? Worker.Phone : "------";
            emailTxt.Text = Worker.Email != null ? Worker.Email : "------";
            bDateTxt.Text = Worker.BirthDate != null ? ((DateTime)Worker.BirthDate).ToShortDateString() : "------";
            psTxt.Text = Worker.PassportSeries != null ? Worker.PassportSeries : "------";
            pnTxt.Text = Worker.PassportNumber != null ? Worker.PassportNumber : "------";
            pDateTxt.Text = Worker.PassportDate != null ? ((DateTime)Worker.PassportDate).ToShortDateString() : "------";
            raTxt.Text = Worker.RegistrationAdress != null ? Worker.RegistrationAdress : "------";
            faTxt.Text = Worker.FactAdress != null ? Worker.FactAdress : "------";
            groupTxt.Text = Worker.WorkerCategory.GroupName != null ? Worker.WorkerCategory.GroupName : "------";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshInfo();
        }

        private void deleteWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.DeleteWokrer(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString()))
                MessageBox.Show("Работник успешно удален");
            else
                MessageBox.Show("При удалении работника произошла ошибка");
        }

        private void updateWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            NewWorkerWindow updateWorker = new NewWorkerWindow();
            updateWorker.WorkerToUpdate = Worker;
            updateWorker.ShowDialog();
        }
    }
}
