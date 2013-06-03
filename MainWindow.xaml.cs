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
using System.Data.Entity;
using System;
using System.Collections.Generic;

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
            connectionSetting.Click += (obj, sndr) => new СonfigСonnectionWindow().ShowDialog();

            exitApp.Click += (obj, sndr) => App.Current.Shutdown();
            aboutApp.Click += (obj, sndr) => new AboutWindow().Show();

            addTeacher.Click += (obj, sndr) => new NewTeacherWindow(context).ShowDialog();
            addClassroom.Click += (obj, sndr) => new NewClassroomWindow(context).ShowDialog();
            addGroup.Click += (obj, sndr) => new NewGroupWindow(context).ShowDialog();
            addLessonsSchedule.Click += (obj, sndr) => new NewLessonsScheduleWindow(context).ShowDialog();
            addSubject.Click += (obj, sndr) => new NewSubjectWindow(context).ShowDialog();
            addSpecialty.Click += (obj, sndr) => new NewSpecialtyWindow(context).ShowDialog();
            addWorkPlan.Click += (obj, sndr) => new NewWorkPlanWindow(context).ShowDialog();
            addRegulation.Click += (obj, sndr) => new NewRegulationWindow(context).ShowDialog();

            printSetting.Click += (obj, sndr) =>
            {
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




            //// Загружаем данные из бд для локльного использования без изменений в базе
            //context.teacher.Load();
            //context.regulation.Load();
            //context.workPlan.Load();

            // Список всех преподователей из  таблицы правил
            int[] teachers = context.regulation.Select(id => id.idTeacher).Distinct().ToArray();

            //Количество недель в семестре
            int weeksCount;

            int lectures;     // Количество часов на лекции
            int practices;    // Количество часов на практику
            int laboratorys;  // КОличествво часов на лабораторные
            int maxLessons;   // Максимальное число пар за день


            foreach (int teacherID in teachers)
            {
                ///////////////////\\\\\\\\\\\\\\\\\\\\
                System.Diagnostics.Debug.WriteLine("------------------------------------------------------------------>");
                ///////////////////\\\\\\\\\\\\\\\\\\\\

                // Текущий преподователь
                Teacher currentTeacher = context.teacher
                                                   .Where(id => id.id == teacherID)
                                                   .Single();

                ///////////////////\\\\\\\\\\\\\\\\\\\\
                System.Diagnostics.Debug.WriteLine("Преподователь:  ID = {0},  Фамилия = {1}", currentTeacher.id, currentTeacher.lastname);
                ///////////////////\\\\\\\\\\\\\\\\\\\\
                
                // Рабочий план текущего преподователя
                WorkPlan[] workPlan = context.workPlan
                                                   .Where(id => id.idTeacher == teacherID)
                                                   .ToArray();

                foreach (WorkPlan plan in workPlan)
                {
                    // Количество дней в семестре
                    TimeSpan daysOfSemester = DateTime.Parse(plan.endDate) - DateTime.Parse(plan.beginDate);

                    //Количество недель в семестре
                    weeksCount = daysOfSemester.Days / 7;

                    // Количество часов на лекции в неделю у данного преподователя на данный предмет                      
                    lectures = plan.lecturesTime / weeksCount;

                    // Количество часов на практику в неделю у данного преподователя на данный предмет                      
                    practices = plan.practiceTime / weeksCount;

                    // Количество часов на лабораторные в неделю у данного преподователя на данный предмет                      
                    laboratorys = plan.laboratoryTime / weeksCount;


                    ///////////////////\\\\\\\\\\\\\\\\\\\\                    
                    System.Diagnostics.Debug.WriteLine("Количество недель в семестре {0}", weeksCount);
                    System.Diagnostics.Debug.WriteLine("Рабочий план НА СЕМЕСТР:  ID = {0},  Лекций = {1},  Практик = {2},  Лаб = {3}", plan.id, plan.lecturesTime, plan.practiceTime, plan.laboratoryTime);
                    System.Diagnostics.Debug.WriteLine("Рабочий план НА НЕДЕЛЮ:   ID = {0},  Лекций = {1},  Практик = {2},  Лаб = {3}", plan.id, lectures, practices, laboratorys);
                    ///////////////////\\\\\\\\\\\\\\\\\\\\


                    // Все правила текущего преподователя из рабочего плана, отсортированных по дням недели
                    Regulation[] teacherFromReg = context.regulation
                                                                .Where(id => id.idTeacher == teacherID)
                                                                .OrderBy(d => d.day)
                                                                .ToArray();

                    foreach (Regulation reg in teacherFromReg)
                    {
                        maxLessons = reg.maxLesson;

                        ///////////////////\\\\\\\\\\\\\\\\\\\\
                        System.Diagnostics.Debug.WriteLine("");
                        System.Diagnostics.Debug.WriteLine("Правило:  ID = {0},  День недели = {1},  Занятия = {2},  Макс. занятий = {3}", reg.id, reg.day, reg.lessons, reg.maxLesson);
                        ///////////////////\\\\\\\\\\\\\\\\\\\\

                        int[] lessons = reg.lessons.Split(',')
                                                   .Select(lesson => int.Parse(lesson))
                                                   .ToArray();

                        List<int> taskLessons = new List<int>();

                        foreach (int lesson in lessons)
                        {

                            // Условие выхода из цикла
                            if (maxLessons == 0)
                            {
                                break;
                            }                                                     

                            // Уменьшаем количество часов на занятия
                            if (lectures > 0) lectures --;
                            else if (practices > 0) practices --;
                            else if (laboratorys > 0) laboratorys --;
                            else break;

                            maxLessons--;

                            taskLessons.Add(lesson);
                        }

                        ///////////////////\\\\\\\\\\\\\\\\\\\\
                        System.Diagnostics.Debug.WriteLine("Остаток НА НЕДЕЛЮ:  Лекций: = {0},  Практических =  {1},  Лабораторных = {2}", lectures, practices, laboratorys);                        
                        ///////////////////\\\\\\\\\\\\\\\\\\\\
                        System.Diagnostics.Debug.WriteLine("Расписание: ID предмета = {0},  День недели = {1},  Занятия = {2},  Осталось свободных занятий = {3}",
                                                            plan.idSubject,
                                                            reg.day,
                                                            String.Join(",", taskLessons
                                                                              .Select(x => x)
                                                                              .OrderBy(x => x)
                                                                              .ToArray()), 
                                                            maxLessons);                        
                        ///////////////////\\\\\\\\\\\\\\\\\\\\

                        
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
