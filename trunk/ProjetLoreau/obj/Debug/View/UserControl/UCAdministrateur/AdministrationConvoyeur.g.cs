﻿#pragma checksum "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "90D4A1CFCFA02246D864B5159B13EAC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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


namespace ProjetLoreau {
    
    
    /// <summary>
    /// AdministrationConvoyeur
    /// </summary>
    public partial class AdministrationConvoyeur : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel dp;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_admin_commandes_editer;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_admin_convoyeur_retour;
        
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
            System.Uri resourceLocater = new System.Uri("/ProjetLoreau;component/view/usercontrol/ucadministrateur/administrationconvoyeur" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml"
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
            this.dp = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 2:
            this.btn_admin_commandes_editer = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml"
            this.btn_admin_commandes_editer.Click += new System.Windows.RoutedEventHandler(this.btn_admin_commandes_editer_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_admin_convoyeur_retour = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\..\..\..\View\UserControl\UCAdministrateur\AdministrationConvoyeur.xaml"
            this.btn_admin_convoyeur_retour.Click += new System.Windows.RoutedEventHandler(this.btn_admin_convoyeur_retour_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
