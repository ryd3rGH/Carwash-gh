using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text;

namespace CarwashManager.Classes
{
    interface IPanel
    {
        static void SetBackground(string value, Control control)
        {
            var bc = new BrushConverter();
            control.Background = (Brush)bc.ConvertFromString(value);
        }
    }
}
