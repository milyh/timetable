using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
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
                settingWindow.ShowDialog();
                settingWindow.ConnectionComplite += (obj, sndr) => initConnection();
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

            //System.Diagnostics.Debug.WriteLine("________ " + context.regulation.GetType() + " __________");
            //System.Diagnostics.Debug.WriteLine("________ " + context.regulation.FirstOrDefault().GetType() + " __________");

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

            //System.Diagnostics.Debug.WriteLine("+++++++++++++++ " + context.classroom.First().className + " +++++++++++++++");

            // Прмиер LINQ
            //LessonsSchedule reg = context.lessonsSchedule.Where(n => n.id == 5).FirstOrDefault();
            //System.Diagnostics.Debug.WriteLine(DateTime.Parse(reg.beginTime) + " ____________________________");
            
            //LessonsSchedule f = new LessonsSchedule();
            //f.beginTime = DateTime.Now.ToString("t");
            //f.endTime = DateTime.Now.ToString("t");
            // DateTime(2000, 1, 2, 17, 18, 19)
            // 2000 - год, 1 - месяц, 2 - число,  17 - часов, 18 - минут, 19 - секунд
            //context.lessonsSchedule.Add(f);
            //context.SaveChanges();

            //System.Diagnostics.Debug.WriteLine(reg.lessons);

            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(reg.lessons);

            //int[] fr = Array.ConvertAll(reg.lessons, n => (int)n);

            //foreach (var a in reg.lessons)
            //{
            //    System.Diagnostics.Debug.WriteLine(a.ToString());
            //}
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

            //System.Diagnostics.Debug.WriteLine((sender as Button).Name.Replace("Button", string.Empty));
        }
    }
}
