using CWLib;
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

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for CashboxOperationControl.xaml
    /// </summary>
    public partial class CashboxOperationControl : UserControl
    {
        private string ConnStr { get; set; }
        private CashboxOperation Operation { get; set; }

        public CashboxOperationControl(CashboxOperation operation)
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            Operation = operation;

            InitializeComponent();
            ShowInfo();
        }

        private void ShowInfo()
        {
            dateLbl.Content = Operation.OperationDateTime.ToShortTimeString() + " " + Operation.OperationDateTime.ToShortDateString();
            typeLbl.Content = Operation.IsConsumption ? "Расход" : "Приход";
            descrLbl.Content = Operation.Description;
            moneyLbl.Content = Operation.MoneyType.Name;
            sumLbl.Content = Operation.Sum.ToString("####.##") + " руб.";

            if (Operation.IsDeleted)
            {
                for (int i=2; i<mainControlGrid.Children.Count; i++)
                {
                    if (mainControlGrid.Children[i].GetType() == typeof(Label))
                    {
                        Label lbl = (Label)mainControlGrid.Children[i];
                        lbl.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    
                    if (mainControlGrid.Children[i].GetType() == typeof(Button))
                    {
                        Button btn = (Button)mainControlGrid.Children[i];
                        btn.IsEnabled = false;
                    }
                }
            }    
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Operation.OrderId != null)
            {
                MessageBoxResult dialog = MessageBox.Show("Если удалить операцию, связанный с ней заказ также будет удален.\nПродолжить?", "ВНИМАНИЕ", MessageBoxButton.OKCancel);
                if (dialog == MessageBoxResult.OK)
                {
                    if (Operation.DeleteOperation(ConnStr))
                        MessageBox.Show("Кассовая операция успешно удалена");
                    else
                        MessageBox.Show("При удалении кассовой операции произошла ошибка");
                }
            }

            else
            {
                if (Operation.DeleteOperation(ConnStr))
                    MessageBox.Show("Кассовая операция успешно удалена");
                else
                    MessageBox.Show("При удалении кассовой операции произошла ошибка");
            }
        }
    }
}
