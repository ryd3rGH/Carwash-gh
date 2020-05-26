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
    /// Interaction logic for ClientGroupBigControl.xaml
    /// </summary>
    public partial class ClientGroupBigControl : UserControl
    {
        ClientGroup Group { get; set; }

        public ClientGroupBigControl(ClientGroup group)
        {
            InitializeComponent();

            Group = group;

            groupNameLbl.Content = Group.Name;
            groupDiscountlbl.Content = Group.Discount + "%";
        }
    }
}
