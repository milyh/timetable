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
    /// Interaction logic for NewTeacherWindow.xaml
    /// </summary>
    public partial class NewTeacherWindow : Window
    {
        private EntityContext db;

        public NewTeacherWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            // Если было окно с ошибкой, то при получении фокуса скрываем окно с ошибкой
            firstnameTextBox.GotFocus += changeErrorVisible;
            lastnameTextBox.GotFocus += changeErrorVisible;
            middlenameTextBox.GotFocus += changeErrorVisible;
            phoneTextBox.GotFocus += changeErrorVisible;

            // Если название предмета пустое - сделать кнопку неактивной
            firstnameTextBox.TextChanged += changeText;
            lastnameTextBox.TextChanged += changeText;
            middlenameTextBox.TextChanged += changeText;
            phoneTextBox.TextChanged += changeText;
        }

        // Нажатие кнопки "Добавить"
        private void addTeacher(object sender, RoutedEventArgs e)
        {
            // Создаём новую аудиторию
            Teacher newTeacher = new Teacher();
            newTeacher.firstname = firstnameTextBox.Text;
            newTeacher.lastname = lastnameTextBox.Text;
            newTeacher.middlename = middlenameTextBox.Text;
            newTeacher.initials = String.Format("{0}. {1}.", firstnameTextBox.Text[0], middlenameTextBox.Text[0]);
            newTeacher.phone = phoneTextBox.Text;
            newTeacher.adress = adressTextBox.Text;
            newTeacher.email = emailTextBox.Text;
            newTeacher.description = descriptionTextBox.Text;


            // Добавляем созданную аудиторию
            db.teacher.Add(newTeacher);

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
                db.teacher.Remove(newTeacher);                              // Удаляем созданный предмет из списка
            }
            finally
            {
                newTeacher = null;                              // Удаляем предмет
                firstnameTextBox.Text = String.Empty;           // Очищаем поля
                lastnameTextBox.Text = String.Empty;
                middlenameTextBox.Text = String.Empty;
            }

        }

        // Событие ввода текста
        private void changeText(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;

            // Если поля: имя, фамилия, отчество и телефон 
            // пустые - скрыть кнопку
            if (String.IsNullOrEmpty(firstnameTextBox.Text) ||
                String.IsNullOrEmpty(lastnameTextBox.Text) ||
                String.IsNullOrEmpty(middlenameTextBox.Text) ||
                String.IsNullOrEmpty(phoneTextBox.Text))
            {
                addButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                addButton.Visibility = Visibility.Visible;
            }
        }

        // Hide error message
        private void changeErrorVisible(object sender, RoutedEventArgs e)
        {
            errorTextBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && addButton.Visibility == Visibility.Visible)
                addTeacher(null, null);
        }

    }
}
