#pragma checksum "..\..\..\ReportPenjualan.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0960D524BBB9EDF1A8C4821AB857BF51777ADC73"
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
using RootLibrary.WPF.Localization;
using SAPBusinessObjects.WPF.Viewer;
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


namespace Project_PCS
{


    /// <summary>
    /// ReportPenjualan
    /// </summary>
    public partial class ReportPenjualan : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 36 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dari;

#line default
#line hidden


#line 37 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker sampai;

#line default
#line hidden


#line 38 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox karyawan;

#line default
#line hidden


#line 39 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox customer;

#line default
#line hidden


#line 40 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox promo;

#line default
#line hidden


#line 41 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox subs;

#line default
#line hidden


#line 42 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox subtotal;

#line default
#line hidden


#line 43 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tampil;

#line default
#line hidden


#line 47 "..\..\..\ReportPenjualan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer cReport;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project PCS;component/reportpenjualan.xaml", System.UriKind.Relative);

#line 1 "..\..\..\ReportPenjualan.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 9 "..\..\..\ReportPenjualan.xaml"
                    ((Project_PCS.ReportPenjualan)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);

#line default
#line hidden
                    return;
                case 2:
                    this.dari = ((System.Windows.Controls.DatePicker)(target));
                    return;
                case 3:
                    this.sampai = ((System.Windows.Controls.DatePicker)(target));
                    return;
                case 4:
                    this.karyawan = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 5:
                    this.customer = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 6:
                    this.promo = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 7:
                    this.subs = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 8:
                    this.subtotal = ((System.Windows.Controls.TextBox)(target));

#line 42 "..\..\..\ReportPenjualan.xaml"
                    this.subtotal.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Subtotal_TextChanged);

#line default
#line hidden
                    return;
                case 9:
                    this.tampil = ((System.Windows.Controls.Button)(target));

#line 43 "..\..\..\ReportPenjualan.xaml"
                    this.tampil.Click += new System.Windows.RoutedEventHandler(this.Tampil_Click);

#line default
#line hidden
                    return;
                case 10:

#line 45 "..\..\..\ReportPenjualan.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);

#line default
#line hidden
                    return;
                case 11:
                    this.cReport = ((SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button btn_report;
        internal System.Windows.Controls.Button btn_report_penjualan;
        internal System.Windows.Controls.Button btn_report_pembelian;
        internal System.Windows.Controls.Button btn_report_member;
        internal System.Windows.Controls.Button LogOut;
    }
}

