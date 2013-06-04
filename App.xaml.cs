using System.Windows;

namespace timetable
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string xmlFileName = "connection.xml";
        public static readonly string[] days = new string[] {"понедельник", "вторник", "среда", "четверг", "пятница", "суббота"};
    }
}
