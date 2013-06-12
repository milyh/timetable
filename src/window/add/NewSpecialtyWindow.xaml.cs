using System;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewSpecialtyWindow.xaml
    /// </summary>
    public partial class NewSpecialtyWindow : Window
    {
        private EntityContext db;

        public NewSpecialtyWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            // Если было окно с ошибкой, то при получении фокуса скрываем окно с ошибкой
            specialtyNameTextBox.GotFocus += (obj, sndr) => errorTextBox.Visibility = System.Windows.Visibility.Collapsed;

            // Если название специальности пустое - сделать кнопку неактивной
            specialtyNameTextBox.TextChanged += (obj, sndr) =>
            {
                addButton.Visibility = String.IsNullOrEmpty(specialtyNameTextBox.Text) ? Visibility.Collapsed : Visibility.Visible;

                // Программно включаем CAPS LOCK
                specialtyNameTextBox.Text = specialtyNameTextBox.Text.ToUpper();
                specialtyNameTextBox.SelectionStart = specialtyNameTextBox.Text.Length;
            };
        }

        // Нажатие кнопки "Добавить"
        private void addSpecialty(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            Specialty newSpecialty = new Specialty();
            newSpecialty.specialtyName = specialtyNameTextBox.Text; //  .ToUpper()
            newSpecialty.description = descriptionTextBox.Text;

            // Добавляем созданную аудиторию
            db.specialty.Add(newSpecialty);

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
                db.specialty.Remove(newSpecialty);                            // Удаляем созданную аудитории из списка
            }
            finally
            {
                newSpecialty = null;                            // Удаляем аудиторию
                specialtyNameTextBox.Text = String.Empty;       // Очищаем текстовое поле с названием аудиторией
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && addButton.Visibility == Visibility.Visible)
                addSpecialty(null, null);
        }

    }
}
