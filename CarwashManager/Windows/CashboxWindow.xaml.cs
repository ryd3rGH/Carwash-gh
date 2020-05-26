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

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for CashboxWindow.xaml
    /// </summary>
    public partial class CashboxWindow : Window, IWindow
    {
        private string ConnStr { get; set; }
        private List<CashboxOperation> operations { get; set; }

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
            ShowOperations(operations);
        }

        private void searchOperationsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (startDateTxt.SelectedDate != null)
            {
                if (endDateTxt.SelectedDate != null)
                {
                    operations = CashboxOperation.GetOperations(ConnStr,
                        ((DateTime)startDateTxt.SelectedDate).ToString("yyyy-MM-dd 00:00:00"),
                        ((DateTime)endDateTxt.SelectedDate).ToString("yyyy-MM-dd 23:59:59"));

                    ShowOperations(operations);
                }
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
