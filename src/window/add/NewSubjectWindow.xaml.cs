using System;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;
using System.Linq;

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
            subjectNameTextBox.GotFocus += (obj, sndr) => errorTextBox.Visibility = Visibility.Collapsed;

            // Список всех доступных аудиторий
            classesComboBox.ItemsSource = db.classroom.Select(classroom => classroom.className).ToArray();

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

            // Добавляем созданный предмет
            db.subject.Add(newSubject);            

            try
            {
                db.SaveChanges();   // Пытаемся сохранить изменения     

                // Если бли выбраны аудитории записать значения в таблицу
                foreach (var item in classesComboBox.SelectedItems)
                {
                    SubjectClasses temp = new SubjectClasses();
                    temp.idSubject = newSubject.id;
                    temp.idClassroom = db.classroom
                                            .Where(classroom => classroom.className == (string)item)
                                            .Select(classroom => classroom.id)
                                            .Single();

                    db.subjectClasses.Add(temp);
                }

                db.SaveChanges();

                this.Close();       // Закрываем окно, в случае успеха
            }
            catch (Exception error)
            {
                errorTextBox.Visibility = System.Windows.Visibility.Visible;  // Показываем текстовое поле
                errorTextBox.Text = error.Message;                            // с текстом ошибки
                errorTextBox.Focus();                                         // устанавливаем фокус на это поле
                db.subject.Remove(newSubject);                                // Удаляем созданный предмет из списка
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
