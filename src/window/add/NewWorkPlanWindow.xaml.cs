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

            // Список  преподоватлей (ФИО)
            teacherNameTextBox.ItemsSource = db.teacher
                                                  .Select(item => item.lastname + " " + item.firstname + " " + item.middlename)
                                                  .ToList<string>();
            // Список предметов
            subjectNameComboBox.ItemsSource = db.subject
                                                  .Select(item => item.subjectName)
                                                  .ToList<string>();

            teacherNameTextBox.SelectedIndex = subjectNameComboBox.SelectedIndex = 0;

        }

        // Нажатие кнопки "Добавить"
        private void addWorkPlan(object sender, RoutedEventArgs e)
        {
            string[] fio = ((string)teacherNameTextBox.SelectedValue).Split();
            string _firstname = fio[0];
            string _lastname = fio[1];
            string _middlename = fio[2];

            // Создаём новую аудиторию
            WorkPlan newWorkPlan = new WorkPlan();

            newWorkPlan.idTeacher = db.teacher
                                            .Where(item => item.lastname == _firstname &&
                                                           item.firstname == _lastname &&
                                                           item.middlename == _middlename)
                                            .Select(item => item.id)
                                            .First();
            newWorkPlan.idSubject = db.subject
                                            .Where(item => item.subjectName == (string)subjectNameComboBox.SelectedValue)
                                            .Select(item => item.id)
                                            .First();
            newWorkPlan.lecturesTime = (int)lecturesTime.Value;
            newWorkPlan.practiceTime = (int)practiceTime.Value;
            newWorkPlan.laboratoryTime = (int)laboratoryTime.Value;
            newWorkPlan.description = descriptionTextBox.Text;

            // Добавляем созданную аудиторию
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
                db.workPlan.Remove(newWorkPlan);                // Удаляем созданную аудитории из списка
            }
            finally
            {
                newWorkPlan = null;                        // Удаляем аудиторию
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
