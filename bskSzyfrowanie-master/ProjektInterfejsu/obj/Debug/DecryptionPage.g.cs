﻿#pragma checksum "..\..\DecryptionPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4C4FE2CE6F0FFC25107FA0894122480671884A7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ProjektInterfejsu;
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


namespace ProjektInterfejsu {
    
    
    /// <summary>
    /// DecryptionPage
    /// </summary>
    public partial class DecryptionPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxDecryptInputFile;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDecryptInputFile;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxDecryptOutputFile;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordDecrypt;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxRecipient;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgBarDecrypt;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\DecryptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DecryptBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ProjektInterfejsu;component/decryptionpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DecryptionPage.xaml"
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
            this.textBoxDecryptInputFile = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.BtnDecryptInputFile = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\DecryptionPage.xaml"
            this.BtnDecryptInputFile.Click += new System.Windows.RoutedEventHandler(this.btnInputFileDecrypt_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textBoxDecryptOutputFile = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 32 "..\..\DecryptionPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PasswordDecrypt = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.ComboBoxRecipient = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.ProgBarDecrypt = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 8:
            this.DecryptBtn = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\DecryptionPage.xaml"
            this.DecryptBtn.Click += new System.Windows.RoutedEventHandler(this.BtnDecrypt_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

