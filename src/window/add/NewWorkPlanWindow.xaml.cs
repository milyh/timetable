using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewWorkPlanWindow.xaml
    /// </summary>
    public partial class NewWorkPlanWindow : Window
    {
        private EntityContext db;

        public NewWorkPlanWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            /*
             * Первое полугодие 1 сентбря - 28 декабря
             * Второе полугодие 14 января - 28 июня
            */

            // Утсановка даты начала и конца семестра
            beginDate.SelectedDate = new DateTime(DateTime.Now.Year, 9, 1); 
            endDate.SelectedDate = new DateTime(DateTime.Now.Year, 12, 28); 

            // Список  преподоватлей (ФИО)
            teacherNameComboBox.ItemsSource = db.teacher
                                                  .Select(item => item.lastname + " " + item.firstname + " " + item.middlename)
                                                  .ToList<string>();
            // Список предметов
            subjectNameComboBox.ItemsSource = db.subject
                                                  .Select(item => item.subjectName)
                                                  .ToList<string>();

            teacherNameComboBox.SelectedIndex = subjectNameComboBox.SelectedIndex = 0;
        }

        // Нажатие кнопки "Добавить"
        private void addWorkPlan(object sender, RoutedEventArgs e)
        {
            string[] fio = ((string)teacherNameComboBox.SelectedValue).Split();
            string _firstname = fio[0];
            string _lastname = fio[1];
            string _middlename = fio[2];

            // Создаём новый рабочий план
            WorkPlan newWorkPlan = new WorkPlan();

            newWorkPlan.idTeacher = db.teacher
                                            .Where(item => item.lastname == _firstname &&
                                                           item.firstname == _lastname &&
                                                           item.middlename == _middlename)
                                            .Select(item => item.id)
                                            .Single();
            newWorkPlan.idSubject = db.subject
                                            .Where(item => item.subjectName == (string)subjectNameComboBox.SelectedValue)
                                            .Select(item => item.id)
                                            .First();
            newWorkPlan.beginDate = ((DateTime)beginDate.SelectedDate).ToString("D");
            newWorkPlan.endDate = ((DateTime)endDate.SelectedDate).ToString("D");
            newWorkPlan.lecturesTime = (int)lecturesTime.Value;
            newWorkPlan.practiceTime = (int)practiceTime.Value;
            newWorkPlan.laboratoryTime = (int)laboratoryTime.Value;
            newWorkPlan.description = descriptionTextBox.Text;

            // Добавляем созданный план
            db.workPlan.Add(newWorkPlan);

            try
            {
                db.SaveChanges();   // Пытаемся сохранить изменения
                this.Close();       // Закрываем окно, в случае успеха
            }
            catch (Exception error)
            {
                errorTextBox.Visibility = System.Windows.Visibility.Visible;  // Показываем текстовое поле
                errorTextBox.Text = error.Message;                            // с текстом ошибки
                errorTextBox.Focus();                                         // устанавливаем фокус на это поле
                db.workPlan.Remove(newWorkPlan);                // Удаляем созданный рабочий план из списка
            }
            finally
            {
                newWorkPlan = null;                             // Удаляем рабочий план
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                addWorkPlan(null, null);
        }

    }
}
