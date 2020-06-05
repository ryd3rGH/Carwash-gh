using CarwashManager.Classes;
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
using System.Resources;
using System.Reflection;
using System.ComponentModel;
using System.Threading;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for CashboxWindow.xaml
    /// </summary>
    public partial class CashboxWindow : Window, IWindow
    {
        private string ConnStr { get; set; }
        private string StartDate { get; set; }
        private string EndDate { get; set; }
        private List<CashboxOperation> Operations { get; set; }

        public CashboxWindow()
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();

            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();

            sumBox.Visibility = Visibility.Hidden;
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            this.Title = rm.GetString("CashboxWindowName");
        }

        private void ShowOperations(List<CashboxOperation> operations)
        {
            allOperationsPanel.Children.Clear();
            inOperationsPanel.Children.Clear();
            outOperationsPanel.Children.Clear();

            decimal inSum = 0;
            decimal outSum = 0;

            if (operations.Count > 0)
            {
                for (int i=0; i<operations.Count; i++)
                {
                    if (!operations[i].IsConsumption && !operations[i].IsDeleted)
                        inSum += operations[i].Sum;
                    else if (operations[i].IsConsumption && !operations[i].IsDeleted)
                        outSum += operations[i].Sum;

                    CashboxOperationControl operation = new CashboxOperationControl(operations[i]);
                    operation.deleteBtn.Click += DeleteBtn_Click;
                    allOperationsPanel.Children.Add(operation);

                    if (!operations[i].IsConsumption)
                    {
                        CashboxOperationControl inOperation = new CashboxOperationControl(operations[i]);
                        inOperation.deleteBtn.Click += DeleteBtn_Click;
                        inOperationsPanel.Children.Add(inOperation);
                    }
                    else
                    {
                        CashboxOperationControl outOperation = new CashboxOperationControl(operations[i]);
                        outOperation.deleteBtn.Click += DeleteBtn_Click;
                        outOperationsPanel.Children.Add(outOperation);
                    }
                }

                inSumlbl.Content = inSum.ToString() + " руб.";
                outSumlbl.Content = outSum.ToString() + " руб.";

                sumBox.Visibility = Visibility.Visible;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            searchOperationsBtn_Click(null, null);
            ShowOperations(Operations);
        }

        private void GetOperations()
        {
            searchGroupBox.IsEnabled = false;
            actionsGroupBox.IsEnabled = false;
            sumBox.IsEnabled = false;

            StartDate = ((DateTime)startDateTxt.SelectedDate).ToString("yyyy-MM-dd 00:00:00");
            EndDate = ((DateTime)endDateTxt.SelectedDate).ToString("yyyy-MM-dd 23:59:59");

            using (BackgroundWorker operationsWorker = new BackgroundWorker())
            {
                operationsWorker.DoWork += OperationsWorker_DoWork;
                operationsWorker.RunWorkerCompleted += OperationsWorker_RunWorkerCompleted;
                operationsWorker.RunWorkerAsync();
            }
        }

        private void OperationsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Operations = CashboxOperation.GetOperations(ConnStr, StartDate, EndDate);
            Thread.Sleep(1500);
        }

        private void OperationsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowOperations(Operations);

            searchGroupBox.IsEnabled = true;
            actionsGroupBox.IsEnabled = true;
            sumBox.IsEnabled = true;
        }        

        private void searchOperationsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (startDateTxt.SelectedDate != null)
            {
                if (endDateTxt.SelectedDate != null)                
                    GetOperations();                
                else
                    MessageBox.Show("Не указано окончание периода");
            }
            else
                MessageBox.Show("Не указано начало периода");
        }

        private void addMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCashboxOperationWindow addMoney = new NewCashboxOperationWindow(false);
            addMoney.Owner = this;
            addMoney.ShowDialog();

            searchOperationsBtn_Click(null, null);
        }

        private void withdrawMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCashboxOperationWindow addMoney = new NewCashboxOperationWindow(true);
            addMoney.Owner = this;
            addMoney.ShowDialog();

            searchOperationsBtn_Click(null, null);
        }
    }
}
