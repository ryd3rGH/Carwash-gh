﻿#pragma checksum "..\..\..\..\Controls\CashboxOperationControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80D8D212F2DC9CA9368310080EAD904342AB1C2B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CarwashManager.Controls;
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


namespace CarwashManager.Controls {
    
    
    /// <summary>
    /// CashboxOperationControl
    /// </summary>
    public partial class CashboxOperationControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainControlGrid;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label dateLbl;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label typeLbl;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label descrLbl;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label moneyLbl;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label sumLbl;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Controls\CashboxOperationControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/CarwashManager;component/controls/cashboxoperationcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\CashboxOperationControl.xaml"
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
            this.mainControlGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.dateLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.typeLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.descrLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.moneyLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.sumLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.deleteBtn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\Controls\CashboxOperationControl.xaml"
            this.deleteBtn.Click += new System.Windows.RoutedEventHandler(this.deleteBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

