using System.Data.Common;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using timetable.control;
using timetable.src;
using timetable.src.entity;
using timetable.src.entity.table;
using timetable.src.window;
using timetable.src.window.add;

namespace timetable
{

    public partial class MainWindow : Window
    {
        // Создаём модель EF
        private EntityContext context;

        // Устанавливаем провайдер бд ( Npgsql - postgreSQL)
        private DbProviderFactory provider;

        // Создаём соединение
        private DbConnection connection;

        // Таблица, отображает данные из БД
        public DataGridView grid;


        public MainWindow()
        {
            InitializeComponent();

            // События контекстного меню
            connectionSetting.Click += (obj, sndr)  => new СonfigСonnectionWindow().ShowDialog();

            exitApp.Click += (obj, sndr)            => App.Current.Shutdown();
            aboutApp.Click += (obj, sndr)           => new AboutWindow().Show();

            addTeacher.Click += (obj, sndr)         => new NewTeacherWindow(context).ShowDialog();
            addClassroom.Click += (obj, sndr)       => new NewClassroomWindow(context).ShowDialog();
            addGroup.Click += (obj, sndr)           => new NewGroupWindow(context).ShowDialog();
            addLessonsSchedule.Click += (obj, sndr) => new NewLessonsScheduleWindow(context).ShowDialog();
            addSubject.Click += (obj, sndr)         => new NewSubjectWindow(context).ShowDialog();   
            addSpecialty.Click += (obj, sndr)       => new NewSpecialtyWindow(context).ShowDialog();
            addWorkPlan.Click += (obj, sndr)        => new NewWorkPlanWindow(context).ShowDialog();
            //addRegulation.Click += (obj, sndr) =>

            printSetting.Click += (obj, sndr) => {
                                                     PrintDialog printDlg = new PrintDialog();
                                                     printDlg.ShowDialog();
                                                     printDlg.PrintVisual(grid, "Печать...");
                                                 };

            // Если файла xml нет, создать новый файл (настройки соединения с сервером)
            if (!File.Exists(App.xmlFileName))
                XMLconnection.CreateConnectionXML();

            // Проверяем соединение, если нету - показать модальное окно с настройками
            if (!СonfigСonnectionWindow.testConection())
            {
                System.Diagnostics.Debug.WriteLine("Нет соединения, открыть настройки");

                СonfigСonnectionWindow settingWindow = new СonfigСonnectionWindow();
                settingWindow.ConnectionComplite += (obj, sndr) => initConnection();
                settingWindow.ShowDialog();                
            }
            else
            {
                initConnection();
            }

        }

        // Инициируем соединение с сервером
        private void initConnection()
        {
            // Провайдер бд
            provider = DbProviderFactories.GetFactory("Npgsql");

            // Создаём соединение
            connection = provider.CreateConnection();

            // Устанавливаем строку соединения
            connection.ConnectionString = СonfigСonnectionWindow.getConnectionString();

            // Передаём connection в EF
            context = new EntityContext(connection);

            grid = new DataGridView();
            Layout.Children.Add(grid);
            Grid.SetRow(grid, 1);
            Grid.SetColumn(grid, 1);

            // Норма
            grid.Change<Teacher>(context.teacher);

            // Reflection
            //System.Reflection.MethodInfo change = grid.GetType().GetMethod("Change");
            //System.Reflection.MethodInfo genericChange = change.MakeGenericMethod(context.regulation.FirstOrDefault().GetType());
            //genericChange.Invoke(grid, new object[] { context.regulation });
        }

        private void menuButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            string type = (sender as Button).Name.Replace("Button", string.Empty);

            switch (type)
            {
                case "Teacher":
                    grid.Change<Teacher>(context.teacher);
                    break;
                case "Classroom":
                    grid.Change<Classroom>(context.classroom);
                    break;
                case "Subject":
                    grid.Change<Subject>(context.subject);
                    break;
                case "Specialty":
                    grid.Change<Specialty>(context.specialty);
                    break;
            }
        }
    }
}
