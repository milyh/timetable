using System.Data.Common;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using timetable.control;
using timetable.src;
using System.Linq;
using timetable.src.entity;
using timetable.src.entity.table;
using timetable.src.window;
using timetable.src.window.add;
using System;

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
            addRegulation.Click += (obj, sndr)      => new NewRegulationWindow(context).ShowDialog();

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

            int[] teachers = context.regulation.Select(id => id.idTeacher).Distinct().ToArray();

            foreach (int teacherID in teachers)
            {
                // Текущий преподователь
                Teacher currentTeacher = context.teacher
                                                   .Where(id => id.id == teacherID)
                                                   .Single();

                // Рабочий план текущего преподователя
                WorkPlan[] workPlan = context.workPlan
                                                   .Where(id => id.idTeacher == teacherID)
                                                   .ToArray();

                // Необходимое число лекций
                int lessonCount;

                
                if (workPlan.Length > 0)
                {                    
                    // Количество дней в семестре
                    TimeSpan daysOfSemester = DateTime.Parse(workPlan[0].endDate) - DateTime.Parse(workPlan[0].beginDate);

                    //Количество недель в семестре
                    int weeksCount = daysOfSemester.Days / 7;

                    // Количество часов в неделю у данного преподователя на данный предмет
                      // Если количество часов на практику меньше количество недель в семестре
                      // То это пара будет идти раз в 2 недели
                    if (workPlan[0].lecturesTime < weeksCount)
                    {
                        lessonCount = workPlan[0].lecturesTime / (weeksCount / 2);
                    }
                      // Иначе лекция будет каждую неделю
                    else
                    {
                        lessonCount = workPlan[0].lecturesTime / weeksCount;
                    }
                }
            }
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
