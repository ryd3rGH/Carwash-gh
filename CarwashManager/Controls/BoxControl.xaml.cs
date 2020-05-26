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
    /// Interaction logic for BoxControl.xaml
    /// </summary>
    public partial class BoxControl : UserControl
    {
        public string ConnStr { get; set; }
        Box Box { get; set; }        

        public BoxControl(Box box)
        {
            Box = box;
            InitializeComponent();

            BoxView();
        }

        public void BoxView()
        {
            nameLbl.Content = Box.Name;
            if (Box.TechState == true)
                stateLbl.Content = Box.State == true ? "Свободен" : $"В работе, заказ {Box.OrderId}";
            else
                stateLbl.Content = "Ремонт";
        }

        private void changeStateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Box.ChangeTechState(ConnStr))
                MessageBox.Show($"Ошибка при смене тех.состояния бокса");

            BoxView();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Box.DeleteBox(ConnStr))
                MessageBox.Show("Ошибка при удалении бокса");
            else
                MessageBox.Show("Бокс удален");
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            NewBoxWindow updateBox = new NewBoxWindow();
            updateBox.BoxToUpdate = Box;
            updateBox.ShowDialog();
        }
    }
}
