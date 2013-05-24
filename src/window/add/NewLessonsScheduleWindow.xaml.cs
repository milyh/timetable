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
    /// Interaction logic for NewLessonsScheduleWindow.xaml
    /// </summary>
    public partial class NewLessonsScheduleWindow : Window
    {
        private EntityContext db;

        public NewLessonsScheduleWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;
        }

        // Нажатие кнопки "Добавить"
        private void addLessonsSchedule(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            LessonsSchedule newLessonsSchedule = new LessonsSchedule();

            newLessonsSchedule.beginTime = ((DateTime)beginTimePicker.Value).ToString("t"); 
            newLessonsSchedule.endTime = ((DateTime)endTimePicker.Value).ToString("t");
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
