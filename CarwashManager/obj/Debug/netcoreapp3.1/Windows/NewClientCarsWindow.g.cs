﻿#pragma checksum "..\..\..\..\Windows\NewClientCarsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "920016A1511DD18D5EE1E5CC1513E32BE19067DC"
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
    /// NewClientCarsWindow
    /// </summary>
    public partial class NewClientCarsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel topPanel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox brandsList;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox modelsList;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox plateTxt;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addCarBtn;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel carsPanel;
        
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
            System.Uri resourceLocater = new System.Uri("/CarwashManager;V0.0.0.1;component/windows/newclientcarswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
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
            
            #line 8 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
            ((CarwashManager.Windows.NewClientCarsWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.topPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.brandsList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
            this.brandsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.brandsList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.modelsList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.plateTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.addCarBtn = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\Windows\NewClientCarsWindow.xaml"
            this.addCarBtn.Click += new System.Windows.RoutedEventHandler(this.addCarBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.carsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

