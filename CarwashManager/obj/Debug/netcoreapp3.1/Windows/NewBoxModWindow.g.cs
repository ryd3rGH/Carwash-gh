﻿#pragma checksum "..\..\..\..\Windows\NewBoxModWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4A3640998246B8C1ED23D24C0B1F4B8705DE78A0"
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
    /// NewBoxModWindow
    /// </summary>
    public partial class NewBoxModWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label winNameLbl;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox boxNameTxt;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton notbusyBtn;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton busyBtn;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton IsWorkingBtn;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton IsNotWorkingBtn;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel btnsPanel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delBtn;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Windows\NewBoxModWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/CarwashManager;V0.0.0.1;component/windows/newboxmodwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\NewBoxModWindow.xaml"
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
            
            #line 8 "..\..\..\..\Windows\NewBoxModWindow.xaml"
            ((CarwashManager.Windows.NewBoxModWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.winNameLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.boxNameTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.notbusyBtn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.busyBtn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.IsWorkingBtn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.IsNotWorkingBtn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.btnsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.delBtn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\Windows\NewBoxModWindow.xaml"
            this.delBtn.Click += new System.Windows.RoutedEventHandler(this.delBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.addBtn = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\Windows\NewBoxModWindow.xaml"
            this.addBtn.Click += new System.Windows.RoutedEventHandler(this.addBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

