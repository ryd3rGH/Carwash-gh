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
    /// Interaction logic for BoxModControl.xaml
    /// </summary>
    public partial class BoxModControl : UserControl
    {
        public string ConnStr { get; set; }
        private bool AddBoxFlag { get; set; }
        private Box Box { get; set; }

        public BoxModControl(bool addBoxFlag)
        {
            InitializeComponent();

            if (addBoxFlag)
            {
                AddBoxFlag = true;
                try
                {
                    imageBox.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Edit.png"));
                    nameLbl.Content = "Новый бокс";
                    statusLbl.Content = "Добавить новый бокс";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }                
        }

        public BoxModControl(Box box)
        {
            Box = box;
            InitializeComponent();
            AddBoxFlag = false;

            BoxView();
        }

        public void BoxView()
        {
            nameLbl.Content = Box.Name;
            if (Box.TechState == true)
                statusLbl.Content = Box.State == true ? "Свободен" : $"В работе, заказ {Box.OrderId}";
            else
                statusLbl.Content = "Ремонт";
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!AddBoxFlag)
            {
                NewBoxModWindow updateBox = new NewBoxModWindow();
                updateBox.BoxToUpdate = Box;
                updateBox.ShowDialog(); 
            }

            else
            {
                NewBoxModWindow addNewBox = new NewBoxModWindow();
                addNewBox.ShowDialog();
            }
        }
    }
}
