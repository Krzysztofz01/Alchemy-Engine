﻿#pragma checksum "..\..\ExportView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "030BF317BCE21E22BBFE8A6E1737A99B941822FB82C55E0D62D38082146673D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Alchemy_Engine;
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


namespace Alchemy_Engine {
    
    
    /// <summary>
    /// ExportView
    /// </summary>
    public partial class ExportView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\ExportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectFile;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\ExportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveLocation;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\ExportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbImageFormat;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ExportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label slName;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\ExportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slSettings;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\ExportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExport;
        
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
            System.Uri resourceLocater = new System.Uri("/Alchemy-Engine;component/exportview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ExportView.xaml"
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
            this.btnSelectFile = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\ExportView.xaml"
            this.btnSelectFile.Click += new System.Windows.RoutedEventHandler(this.btnSelectFileListener);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnSaveLocation = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\ExportView.xaml"
            this.btnSaveLocation.Click += new System.Windows.RoutedEventHandler(this.btnSaveLocationListener);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cbImageFormat = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.slName = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.slSettings = ((System.Windows.Controls.Slider)(target));
            return;
            case 6:
            this.btnExport = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\ExportView.xaml"
            this.btnExport.Click += new System.Windows.RoutedEventHandler(this.btnExportListener);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

