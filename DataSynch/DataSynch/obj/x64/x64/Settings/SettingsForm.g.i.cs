﻿#pragma checksum "..\..\..\..\Settings\SettingsForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5B9EF0B944289E4A0DF1B5D98BF5855575DCA587782216A7B67E1082B8287201"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DataSynch.Controllers;
using DataSynch.Settings;
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


namespace DataSynch.Settings {
    
    
    /// <summary>
    /// SettingsForm
    /// </summary>
    public partial class SettingsForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grTitleBar;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblClientGuid;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblWorkPointGuid;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbClientGUID;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbWorkStationGUID;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DataSynch.Controllers.NumericSpinner spUpload;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DataSynch.Controllers.NumericSpinner spDownload;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Settings\SettingsForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAccept;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Settings\SettingsForm.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DataSynch;component/settings/settingsform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Settings\SettingsForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.grTitleBar = ((System.Windows.Controls.Grid)(target));
            
            #line 12 "..\..\..\..\Settings\SettingsForm.xaml"
            this.grTitleBar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MoveWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 14 "..\..\..\..\Settings\SettingsForm.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.FormClose);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lblClientGuid = ((System.Windows.Controls.Label)(target));
            
            #line 22 "..\..\..\..\Settings\SettingsForm.xaml"
            this.lblClientGuid.LostFocus += new System.Windows.RoutedEventHandler(this.ValidateGUIDTextBox);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblWorkPointGuid = ((System.Windows.Controls.Label)(target));
            
            #line 23 "..\..\..\..\Settings\SettingsForm.xaml"
            this.lblWorkPointGuid.LostFocus += new System.Windows.RoutedEventHandler(this.ValidateGUIDTextBox);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbClientGUID = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\..\Settings\SettingsForm.xaml"
            this.tbClientGUID.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.GUID_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbWorkStationGUID = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\..\..\Settings\SettingsForm.xaml"
            this.tbWorkStationGUID.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.GUID_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.spUpload = ((DataSynch.Controllers.NumericSpinner)(target));
            return;
            case 8:
            this.spDownload = ((DataSynch.Controllers.NumericSpinner)(target));
            return;
            case 9:
            this.btnAccept = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\Settings\SettingsForm.xaml"
            this.btnAccept.Click += new System.Windows.RoutedEventHandler(this.SaveSetting);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\Settings\SettingsForm.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.FormClose);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

