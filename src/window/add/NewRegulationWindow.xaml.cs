using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using timetable.src.entity;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewRegulationWindow.xaml
    /// </summary>
    public partial class NewRegulationWindow : Window
    {
        public NewRegulationWindow(EntityContext db)
        {
            InitializeComponent();
        }
    }
}
