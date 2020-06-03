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
using CWLib;

namespace CarwashManager.Controls
{
    /// <summary>
    /// Interaction logic for ClassPriceControl.xaml
    /// </summary>
    public partial class ClassPriceControl : UserControl
    {
        public CarClass carClass;
        public CarCategory carCategory;

        public ClassPriceControl()
        {
            InitializeComponent();            
        }

        public void SetPrice()
        {
            if (carClass != null)
                carClass.Price = priceTxt.Text != string.Empty
                              && String.IsNullOrWhiteSpace(priceTxt.Text) != true
                              ? Convert.ToDecimal(priceTxt.Text)
                              : 0;
            else if (carCategory != null)
                carCategory.Price = priceTxt.Text != string.Empty
                              && String.IsNullOrWhiteSpace(priceTxt.Text) != true
                              ? Convert.ToDecimal(priceTxt.Text)
                              : 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (carClass != null)
            {
                nameLbl.Content = carClass.Name;
                nameLbl.ToolTip = carClass.Name;
                priceTxt.Text = carClass.Price != null ? carClass.Price.ToString() : "0";
            }

            else if (carCategory != null)
            {
                nameLbl.Content = carCategory.Name;
                nameLbl.ToolTip = carCategory.Name;
                priceTxt.Text = carCategory.Price != null ? carCategory.Price.ToString() : "0";
            }
        }

        private void priceTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
