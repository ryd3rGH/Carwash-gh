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
    /// Interaction logic for CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public string ConnStr { get; set; }
        CarCategory category;

        public CategoryControl(CarCategory cat)
        {
            InitializeComponent();

            category = cat;
            CategoryView();
        }

        private void CategoryView()
        {
            catNameLbl.Content = category.Name;
            if (category.Classes.Count > 0)
            {

                for (int i=0; i<category.Classes.Count; i++)
                {
                    if(XMLWorker.ClassSearch(category.Classes[i]) != null)
                    {
                        Label cls = new Label();
                        cls.FontSize = 11;
                        cls.Content = XMLWorker.ClassSearch(category.Classes[i]).Name;
                        cls.Margin = new Thickness(3, 0, 0, 0);
                        cls.Padding = new Thickness(2);
                        classesPanel.Children.Add(cls);
                    }
                }
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            category.DeleteCategory(ConnStr);
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCategoryWindow categoryUpdate = new NewCategoryWindow();
            categoryUpdate.CategoryToUpdate = category;
            categoryUpdate.ShowDialog();
        }
    }
}
