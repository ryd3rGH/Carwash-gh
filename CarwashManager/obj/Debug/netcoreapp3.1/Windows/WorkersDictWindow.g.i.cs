﻿#pragma checksum "..\..\..\..\Windows\WorkersDictWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C60D1427714AE59B830759512B543358C93998B1"
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
    /// WorkersDictWindow
    /// </summary>
    public partial class WorkersDictWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Windows\WorkersDictWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox groupList;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Windows\WorkersDictWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox workersList;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Windows\WorkersDictWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addWorkerBtn;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Windows\WorkersDictWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid contentPanel;
        
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
            System.Uri resourceLocater = new System.Uri("/CarwashManager;V0.0.0.1;component/windows/workersdictwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\WorkersDictWindow.xaml"
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
            this.groupList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\..\Windows\WorkersDictWindow.xaml"
            this.groupList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.groupList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.workersList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 16 "..\..\..\..\Windows\WorkersDictWindow.xaml"
            this.workersList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.workersList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.addWorkerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\Windows\WorkersDictWindow.xaml"
            this.addWorkerBtn.Click += new System.Windows.RoutedEventHandler(this.addWorkerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.contentPanel = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

