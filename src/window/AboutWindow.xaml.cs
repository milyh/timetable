using System.Windows;

namespace timetable.src.window
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            milyh.Click += (obj, sndr) => System.Diagnostics.Process.Start("http://www.vk.com/mulyh");
        }
    }
}
