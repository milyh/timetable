using System;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewClassroomWindow.xaml
    /// </summary>
    public partial class NewClassroomWindow : Window
    {
        private EntityContext db;

        public NewClassroomWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            // Если было окно с ошибкой, то при получении фокуса скрываем окно с ошибкой
            classNameTextBox.GotFocus += (obj, sndr) => errorTextBox.Visibility = System.Windows.Visibility.Collapsed;

            // Если название аудитории пустое - сделать кнопку неактивной
            classNameTextBox.TextChanged += (obj, sndr) => addButton.Visibility = String.IsNullOrEmpty(classNameTextBox.Text) ? Visibility.Collapsed : Visibility.Visible; 
        }

        // Нажатие кнопки "Добавить"
        private void addNewClassroom(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            Classroom newClassroom = new Classroom();
            newClassroom.className = classNameTextBox.Text;
            newClassroom.description = descriptionTextBox.Text;

            // Добавляем созданную аудиторию
            db.classroom.Add(newClassroom);

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
                db.classroom.Remove(newClassroom);                            // Удаляем созданную аудитории из списка
            }
            finally
            {
                newClassroom = null;                        // Удаляем аудиторию
                classNameTextBox.Text = String.Empty;       // Очищаем текстовое поле с названием аудиторией
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && addButton.Visibility == Visibility.Visible)
                addNewClassroom(null, null);
        }
    }
}
