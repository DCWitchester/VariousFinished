﻿#pragma checksum "..\..\..\Message\MessageForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "20889E6FC02886B4C258A09DBE3A333B545ADF1F24A055062567924AA7787A6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace WorkOrder {
    
    
    /// <summary>
    /// MessageForm
    /// </summary>
    public partial class MessageForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\Message\MessageForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WorkOrder.MessageForm Message;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Message\MessageForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush msgBackground;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Message\MessageForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label msgTitle;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Message\MessageForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label msgMessage;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Message\MessageForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAccept;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Message\MessageForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WorkOrder;component/message/messageform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Message\MessageForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Message = ((WorkOrder.MessageForm)(target));
            return;
            case 2:
            this.msgBackground = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 3:
            this.msgTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.msgMessage = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnAccept = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Message\MessageForm.xaml"
            this.btnAccept.Click += new System.Windows.RoutedEventHandler(this.ButtonPress);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Message\MessageForm.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.ButtonPress);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

