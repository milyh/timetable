﻿#pragma checksum "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6ACE447C399EE0CE08102CA060C7CCD6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace timetable.src.window.add {
    
    
    /// <summary>
    /// NewRegulationWindow
    /// </summary>
    public partial class NewRegulationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox teacherNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox dayOfWeekComboBox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.CheckListBox lessonsLayout;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox maxLessonsComboBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/timetable;component/src/window/add/newregulationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
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
            
            #line 5 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
            ((timetable.src.window.add.NewRegulationWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.enterKeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.teacherNameComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 24 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
            this.teacherNameComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.selectedTeacher);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dayOfWeekComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.lessonsLayout = ((Xceed.Wpf.Toolkit.CheckListBox)(target));
            
            #line 30 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
            this.lessonsLayout.ItemSelectionChanged += new Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventHandler(this.lessonsChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.maxLessonsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.descriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\..\src\window\add\NewRegulationWindow.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addRegulation);
            
            #line default
            #line hidden
            return;
            case 8:
            this.errorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

