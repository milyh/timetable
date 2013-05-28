using System;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewSubjectWindow.xaml
    /// </summary>
    public partial class NewSubjectWindow : Window
    {
        private EntityContext db;

        public NewSubjectWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            // Если было окно с ошибкой, то при получении фокуса скрываем окно с ошибкой
            subjectNameTextBox.GotFocus += (obj, sndr) => errorTextBox.Visibility = System.Windows.Visibility.Collapsed;

            // Если название предмета пустое - сделать кнопку неактивной
            subjectNameTextBox.TextChanged += (obj, sndr) => addButton.Visibility = String.IsNullOrEmpty(subjectNameTextBox.Text) ? Visibility.Collapsed : Visibility.Visible;
        }

        // Нажатие кнопки "Добавить"
        private void addSubject(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            Subject newSubject = new Subject();
            newSubject.subjectName = subjectNameTextBox.Text;
            newSubject.description = descriptionTextBox.Text;

            // Добавляем созданную аудиторию
            db.subject.Add(newSubject);

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
                db.subject.Remove(newSubject);                              // Удаляем созданный предмет из списка
            }
            finally
            {
                newSubject = null;                            // Удаляем предмет
                subjectNameTextBox.Text = String.Empty;       // Очищаем текстовое поле с названием предмета
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && addButton.Visibility == Visibility.Visible)
                addSubject(null, null);
        }

    }
}
