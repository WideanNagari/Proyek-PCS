﻿#pragma checksum "..\..\..\MasterProdusen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6F021FFAC64A1E93B5DB37148450E657FB0B4323"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project_PCS;
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


namespace Project_PCS {
    
    
    /// <summary>
    /// MasterProdusen
    /// </summary>
    public partial class MasterProdusen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox keyword;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cari;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgvProdusen;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox id;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nama;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button insert;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button update;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\MasterProdusen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resets;
        
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
            System.Uri resourceLocater = new System.Uri("/Project PCS;component/masterprodusen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MasterProdusen.xaml"
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
            
            #line 8 "..\..\..\MasterProdusen.xaml"
            ((Project_PCS.MasterProdusen)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.back = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\MasterProdusen.xaml"
            this.back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.keyword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cari = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\MasterProdusen.xaml"
            this.cari.Click += new System.Windows.RoutedEventHandler(this.Cari_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dgvProdusen = ((System.Windows.Controls.DataGrid)(target));
            
            #line 33 "..\..\..\MasterProdusen.xaml"
            this.dgvProdusen.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DgvProdusen_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.nama = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.insert = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\MasterProdusen.xaml"
            this.insert.Click += new System.Windows.RoutedEventHandler(this.Insert_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.update = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\MasterProdusen.xaml"
            this.update.Click += new System.Windows.RoutedEventHandler(this.Update_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\MasterProdusen.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.resets = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\MasterProdusen.xaml"
            this.resets.Click += new System.Windows.RoutedEventHandler(this.Resets_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

