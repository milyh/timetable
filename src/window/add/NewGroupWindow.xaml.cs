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
    /// Interaction logic for NewGroupWindow.xaml
    /// </summary>
    public partial class NewGroupWindow : Window
    {
        private EntityContext db;

        public NewGroupWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            // Список специальностей
            specialtyNameComboBox.ItemsSource = db.specialty.Select(item => item.specialtyName).ToList<string>();
            specialtyNameComboBox.SelectedIndex = 0;

            // Если было окно с ошибкой, то при получении фокуса скрываем окно с ошибкой
            groupNameTextBox.GotFocus += (obj, sndr) => errorTextBox.Visibility = System.Windows.Visibility.Collapsed;

            // Если название группы пустое - сделать кнопку неактивной
            groupNameTextBox.TextChanged += (obj, sndr) => addButton.Visibility = String.IsNullOrEmpty(groupNameTextBox.Text) ? Visibility.Collapsed : Visibility.Visible; 
        }

        // Нажатие кнопки "Добавить"
        private void addNewGroup(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            Group newGroup = new Group();
            newGroup.groupName = groupNameTextBox.Text;
            newGroup.idSpecialty = db.specialty
                                            .Where(item => item.specialtyName == (string)specialtyNameComboBox.SelectedValue)
                                            .Select(item => item.id)
                                            .First();
            newGroup.description = descriptionTextBox.Text;

            // Добавляем созданную аудиторию
            db.group.Add(newGroup);

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
                db.group.Remove(newGroup);                                    // Удаляем созданную аудитории из списка
            }
            finally
            {
                newGroup = null;                            // Удаляем аудиторию
                groupNameTextBox.Text = String.Empty;       // Очищаем текстовое поле с названием аудиторией
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && addButton.Visibility == Visibility.Visible)
                addNewGroup(null, null);
        }


    }
}
