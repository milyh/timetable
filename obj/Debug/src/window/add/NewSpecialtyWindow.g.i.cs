﻿#pragma checksum "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D60BFC3E15BA58BB47BDAA764FAF3B95"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
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


namespace timetable.src.window.add {
    
    
    /// <summary>
    /// NewSpecialtyWindow
    /// </summary>
    public partial class NewSpecialtyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox specialtyNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox errorTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/timetable;component/src/window/add/newspecialtywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
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
            
            #line 4 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
            ((timetable.src.window.add.NewSpecialtyWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.enterKeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.specialtyNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.descriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\..\src\window\add\NewSpecialtyWindow.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addSpecialty);
            
            #line default
            #line hidden
            return;
            case 5:
            this.errorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

