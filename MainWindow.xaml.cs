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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

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






            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            if (excel == null)
            {
                MessageBox.Show("EXCEL не может быть запущен. Убедитесь, что у вас установлен пакет MS Office.", "Excel Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //excel.Visible = true;
            Excel.Workbook wb = excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
            ws.Name = "Расписание";            

            LessonsSchedule[] rings = context.lessonsSchedule
                                        .OrderBy(lesson => lesson.numberLesson)
                                        .ToArray();
            int column = 2;

            for (int row = 2; row < (App.days.Length) * rings.Length; )
            {
                foreach (LessonsSchedule ring in rings)
                {
                    ws.Cells[column, 2] = ring.numberLesson + " пара" + "\n" + ring.beginTime + " - " + ring.endTime;
                    column++;
                }

                excel.get_Range(String.Format("A{0}:A{1}", row, column - 1), Type.Missing).Merge(Type.Missing);
                ws.Cells[row, 1] = App.days[(row - 2) / rings.Length];

                row += rings.Length;
            }


            
            // Список всех преподователей из  таблицы правил
            int[] teachers = context.regulation.Select(id => id.idTeacher).Distinct().ToArray();

            //Количество недель в семестре
            int weeksCount;

            int lectures;     // Количество часов на лекции
            int practices;    // Количество часов на практику
            int laboratorys;  // КОличествво часов на лабораторные
            int maxLessons;   // Максимальное число пар за день
            int currentTeacherNumber = 2;   // Номер преподователя, нужен для заполнения excel


            foreach (int teacherID in teachers)
            {
                ///////////////////\\\\\\\\\\\\\\\\\\\\
                System.Diagnostics.Debug.WriteLine("------------------------------------------------------------------>");
                ///////////////////\\\\\\\\\\\\\\\\\\\\

                currentTeacherNumber++;

                // Текущий преподователь
                Teacher currentTeacher = context.teacher
                                                   .Where(id => id.id == teacherID)
                                                   .Single();

                ///////////////////\\\\\\\\\\\\\\\\\\\\
                System.Diagnostics.Debug.WriteLine("Преподователь:  ID = {0},  Фамилия = {1}", currentTeacher.id, currentTeacher.lastname);
                ///////////////////\\\\\\\\\\\\\\\\\\\\
                ws.Cells[1, currentTeacherNumber] = currentTeacher.lastname;

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
                        System.Diagnostics.Debug.WriteLine("Правило:  ID = {0},  День недели = {1},  Занятия = {2},  Макс. занятий = {3}", reg.id, App.days[reg.day], reg.lessons, reg.maxLesson);
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
                            if (lectures > 0) lectures--;
                            else if (practices > 0) practices--;
                            else if (laboratorys > 0) laboratorys--;
                            else break;

                            maxLessons--;

                            taskLessons.Add(lesson);

                            
                            // Возвращаем название предмета и его id
                            var subject = context.workPlan
                                         .Where(currentPlan => currentPlan.id == plan.id)
                                         .Join(
                                             context.subject,
                                             currentPlan => currentPlan.idSubject,
                                             sub => sub.id,
                                             (currentPlan, sub) => new { sub.subjectName, sub.id }
                                         )
                                         .Single();

                            // По id предмета возвращаем все аудитории, в которых он преподаётся
                            string classes = String.Join(", ", from sc in context.subjectClasses
                                                               join cl in context.classroom on new { K1 = sc.idSubject, K2 = sc.idClassroom }
                                                                                         equals new { K1 = subject.id, K2 = cl.id }
                                                               select cl.className);

                            ws.Cells[reg.day * rings.Length + 2, currentTeacherNumber] = subject.subjectName + "\n" + classes;
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
        /// ------- Цикл по составлению рассписания кончился ------------
            
            SaveFileDialog saveExcelFile = new SaveFileDialog();
            saveExcelFile.FileName = "Расписание";
            saveExcelFile.DefaultExt = ".xlsx";
            saveExcelFile.Filter = "Книга excel (*.xlsx, *.xls)|*.xlsx;*.xls";

            // Сохранение файла и закрытие excel
            if ((bool)saveExcelFile.ShowDialog())
            {
                wb.SaveAs(saveExcelFile.FileName);
                wb.Close(true);
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
