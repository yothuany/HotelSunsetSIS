﻿#pragma checksum "..\..\..\Views\ServicosConsultar.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "846E5A92B4806D8D70CF6C16D6D893E8B7001FDD15826DC5F1093D67DADCCDF1"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using HotelSunset.Views;
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


namespace HotelSunset.Views {
    
    
    /// <summary>
    /// ServicosConsultar
    /// </summary>
    public partial class ServicosConsultar : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 139 "..\..\..\Views\ServicosConsultar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNome;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\Views\ServicosConsultar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPreco;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\Views\ServicosConsultar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDescricao;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\Views\ServicosConsultar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btVoltar;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\Views\ServicosConsultar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btLimpar;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\Views\ServicosConsultar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btEditar;
        
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
            System.Uri resourceLocater = new System.Uri("/HotelSunset;component/views/servicosconsultar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ServicosConsultar.xaml"
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
            this.txtNome = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtPreco = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtDescricao = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btVoltar = ((System.Windows.Controls.Button)(target));
            
            #line 155 "..\..\..\Views\ServicosConsultar.xaml"
            this.btVoltar.Click += new System.Windows.RoutedEventHandler(this.btVoltar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btLimpar = ((System.Windows.Controls.Button)(target));
            
            #line 156 "..\..\..\Views\ServicosConsultar.xaml"
            this.btLimpar.Click += new System.Windows.RoutedEventHandler(this.btLimpar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btEditar = ((System.Windows.Controls.Button)(target));
            
            #line 157 "..\..\..\Views\ServicosConsultar.xaml"
            this.btEditar.Click += new System.Windows.RoutedEventHandler(this.btEditar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

