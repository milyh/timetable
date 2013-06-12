using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewLessonsScheduleWindow.xaml
    /// </summary>
    public partial class NewLessonsScheduleWindow : Window
    {
        private EntityContext db;
        private int lessonsCount;

        private readonly int lessonDuration = 95;
        private readonly int breakDuration = 10;

        public NewLessonsScheduleWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            
            // Последняя пара в таблице
            lessonsCount = db.lessonsSchedule.Count();                                

            // Создание списка с номером занятия
            numberLesson.ItemsSource = Enumerable.Range(1, 10);
            numberLesson.SelectedIndex = lessonsCount;

            // Ставим начальные значения для начала и конца пары относительно номеры пары
            beginTimePicker.Value = new DateTime(2000, 1, 1, 8, 0, 0)
                                            .AddMinutes(lessonDuration * lessonsCount + (lessonsCount + 1) * breakDuration);
            endTimePicker.Value = new DateTime(2000, 1, 1, 9, 35, 0)
                                            .AddMinutes(lessonDuration * lessonsCount + (lessonsCount + 1) * breakDuration);
        }

        // Нажатие кнопки "Добавить"
        private void addLessonsSchedule(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            LessonsSchedule newLessonsSchedule = new LessonsSchedule();

            newLessonsSchedule.beginTime = ((DateTime)beginTimePicker.Value).ToString("t"); 
            newLessonsSchedule.endTime = ((DateTime)endTimePicker.Value).ToString("t");
            newLessonsSchedule.numberLesson = (int)numberLesson.SelectedValue; 
            newLessonsSchedule.description = descriptionTextBox.Text;

            // Добавляем созданную аудиторию
            db.lessonsSchedule.Add(newLessonsSchedule);

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
                db.lessonsSchedule.Remove(newLessonsSchedule);                // Удаляем созданную аудитории из списка
                numberLesson.SelectedIndex = lessonsCount;
            }
            finally
            {
                newLessonsSchedule = null;                        // Удаляем аудиторию
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                addLessonsSchedule(null, null);
        }

    }
}