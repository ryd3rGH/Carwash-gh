using CarwashManager.Classes;
using CWLib;
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

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewCashboxOperationWindow.xaml
    /// </summary>
    public partial class NewCashboxOperationWindow : Window, IWindow
    {
        private string ConnStr { get; set; }
        private bool IsWithdraw { get; set; }

        public NewCashboxOperationWindow(bool isWithdraw)
        {
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
            IsWithdraw = isWithdraw;

            InitializeComponent();

            SetWindowBackGround();
            SetWindowTitle();
            LoadMoneyTypes();
        }

        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            if (IsWithdraw)
                this.Title = rm.GetString("WithdrawMoneyWindowName");
            else
                this.Title = rm.GetString("AddMoneyWindowName");
        }

        private void LoadMoneyTypes()
        {
            moneyList.ItemsSource = null;
            moneyList.Items.Clear();

            moneyList.ItemsSource = MoneyType.GetMoneyTypes(ConnStr);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (moneyList.SelectedIndex != -1)
            {
                if (String.IsNullOrWhiteSpace(commentTxt.Text) != true && commentTxt.Text != string.Empty)
                {
                    if (String.IsNullOrWhiteSpace(sumTxt.Text) != true && Convert.ToDecimal(sumTxt.Text) > 0)
                    {
                        CashboxOperation operation = new CashboxOperation(null, Convert.ToDecimal(sumTxt.Text), DateTime.Now, (MoneyType)moneyList.SelectedItem, IsWithdraw, commentTxt.Text);
                        if (operation.AddCashboxOperation(ConnStr))
                        {
                            MessageBox.Show("Кассовая операция успешно добавлена");
                            this.Close();
                        }
                        else
                            MessageBox.Show("При добавлении кассовой операции возникла ошибка");
                    }
                    else
                        MessageBox.Show("Не указана сумма");
                }
                else
                    MessageBox.Show("Не указан комментарий");
            }
            else
                MessageBox.Show("Не выбран тип средств");
        }

        private void sumTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
