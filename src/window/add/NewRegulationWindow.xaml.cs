using System.Windows;
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
