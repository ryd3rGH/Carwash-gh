﻿#pragma checksum "..\..\..\..\Windows\CashboxWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "32C41400A23322E98ACD1B0DE561A9A09752FFD9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CarwashManager.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CarwashManager.Windows {
    
    
    /// <summary>
    /// CashboxWindow
    /// </summary>
    public partial class CashboxWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker startDateTxt;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker endDateTxt;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button searchOperationsBtn;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addMoneyBtn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button withdrawMoneyBtn;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox sumBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label inSumlbl;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label outSumlbl;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel allOperationsPanel;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel inOperationsPanel;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Windows\CashboxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel outOperationsPanel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CarwashManager;V0.0.0.1;component/windows/cashboxwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\CashboxWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.startDateTxt = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 2:
            this.endDateTxt = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.searchOperationsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\Windows\CashboxWindow.xaml"
            this.searchOperationsBtn.Click += new System.Windows.RoutedEventHandler(this.searchOperationsBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.addMoneyBtn = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\Windows\CashboxWindow.xaml"
            this.addMoneyBtn.Click += new System.Windows.RoutedEventHandler(this.addMoneyBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.withdrawMoneyBtn = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\Windows\CashboxWindow.xaml"
            this.withdrawMoneyBtn.Click += new System.Windows.RoutedEventHandler(this.withdrawMoneyBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sumBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 7:
            this.inSumlbl = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.outSumlbl = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.allOperationsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.inOperationsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.outOperationsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

