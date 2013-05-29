using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using timetable.src.entity;
using timetable.src.entity.table;

namespace timetable.src.window.add
{
    /// <summary>
    /// Interaction logic for NewRegulationWindow.xaml
    /// </summary>
    public partial class NewRegulationWindow : Window
    {
        private EntityContext db;
        private int lessonsCount;

        public NewRegulationWindow(EntityContext _db)
        {
            InitializeComponent();

            db = _db;

            lessonsCount = db.lessonsSchedule.Count();

            // Список  преподоватлей (ФИО)
            teacherNameComboBox.ItemsSource = db.teacher
                                                  .Select(item => item.lastname + " " + item.firstname + " " + item.middlename)
                                                  .ToList<string>();

            // Максимальное количество занятий
            maxLessonsComboBox.ItemsSource = Enumerable.Range(1, lessonsCount);

            maxLessonsComboBox.SelectedIndex = teacherNameComboBox.SelectedIndex = 0;

            // Заполнение номеров пар
            lessonsLayout.ItemsSource = Enumerable.Range(1, lessonsCount)
                                                  .Select(les => les + " пара")
                                                  .OrderBy(les => les);
        }

        // Нажатие кнопки добавить
        private void addRegulation(object sender, RoutedEventArgs e)
        {
            string[] fio = ((string)teacherNameComboBox.SelectedValue).Split();
            string _firstname = fio[0];
            string _lastname = fio[1];
            string _middlename = fio[2];

            // Создаём новое правилоы
            Regulation newRegulation = new Regulation();
            newRegulation.idTeacher = db.teacher
                                            .Where(item => item.lastname == _firstname &&
                                                           item.firstname == _lastname &&
                                                           item.middlename == _middlename)
                                            .Select(item => item.id)
                                            .Single();

            newRegulation.day = dayOfWeekComboBox.SelectedIndex;

            newRegulation.lessons = String.Join(",", lessonsLayout.SelectedValue
                                                                .Replace(" пара", String.Empty)
                                                                .Replace(",", " ")
                                                                .Split(' ')
                                                          .Select(x => int.Parse(x))
                                                          .OrderBy(x => x)
                                                          .ToArray());

            newRegulation.maxLesson = (int)maxLessonsComboBox.SelectedItem;
            newRegulation.description = descriptionTextBox.Text;

            // Добавляем созданное правило
            db.regulation.Add(newRegulation);

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
                db.regulation.Remove(newRegulation);                                    // Удаляем созданную аудитории из списка
            }
            finally
            {
                newRegulation = null;                            // Удаляем аудиторию
            }

        }

        // Нажатие Enter
        private void enterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                addRegulation(null, null);
        }

        // Нажатие на номера занятий
        private void lessonsChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            if (lessonsLayout.SelectedItems.Count > 0)
            {
                addButton.Visibility = Visibility.Visible;
            }
            else
            {
                addButton.Visibility = Visibility.Collapsed;
            }
        }

        // Выбор преподователя - убираем дни, которые уже добавлены в правила
        private void selectedTeacher(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string[] fio = ((string)teacherNameComboBox.SelectedValue).Split();
            string _firstname = fio[0];
            string _lastname = fio[1];
            string _middlename = fio[2];

            int[] days = db.teacher
                            .Where(item => item.lastname == _firstname &&
                                           item.firstname == _lastname &&
                                           item.middlename == _middlename)
                            .Join(
                                db.regulation,
                                teacher => teacher.id,
                                regulation => regulation.idTeacher,
                                (teacher, regulation) => regulation.day
                            )
                            .ToArray();

            dayOfWeekComboBox.Items.Clear();

            dayOfWeekComboBox.Items.Add("Понедельник");
            dayOfWeekComboBox.Items.Add("Вторник");
            dayOfWeekComboBox.Items.Add("Среда");
            dayOfWeekComboBox.Items.Add("Четверг");
            dayOfWeekComboBox.Items.Add("Пятница");
            dayOfWeekComboBox.Items.Add("Суббота");

            foreach (int i in days)
            {
                dayOfWeekComboBox.Items.RemoveAt(i);
            }

            dayOfWeekComboBox.SelectedIndex = 0;
        }
    }
}
