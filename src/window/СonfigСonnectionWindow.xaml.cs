using Npgsql;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace timetable.src.window
{
    /// <summary>
    /// Interaction logic for СonfigСonnectionWindow.xaml
    /// </summary>
    public partial class СonfigСonnectionWindow : Window
    {
        // XML документ с настройками соединения с сервером
        private static XElement xmlConnection = XElement.Load(App.xmlFileName);

        // Был ли документ успешно сохранён
        private bool isSave = false;

        // Событие: соединение установлено
        public event EventHandler<EventArgs> ConnectionComplite;

        public СonfigСonnectionWindow()
        {
            InitializeComponent();

            // Обязательные
            serverTextBox.Text = xmlConnection.Element("Server").Value;
            portTextBox.Text = xmlConnection.Element("Port").Value;
            dataBaseTextBox.Text = xmlConnection.Element("Database").Value;
            userTextBox.Text = xmlConnection.Element("User").Value;
            passwordTextBox.Password = xmlConnection.Element("Password").Value;

            // Дополнительные
            protocolTextBox.Text = xmlConnection.Element("Protocol").Value;
            securityCheckBox.IsChecked = Convert.ToBoolean(xmlConnection.Element("IntegratedSecurity").Value);
            sslCheckBox.IsChecked = Convert.ToBoolean(xmlConnection.Element("Ssl").Value);
            poolingCheckBox.IsChecked = Convert.ToBoolean(xmlConnection.Element("Pooling").Value);
            minPoolSizeTextBox.Text = xmlConnection.Element("MinPoolSize").Value;
            maxPoolSizeTextBox.Text = xmlConnection.Element("MaxPoolSize").Value;
            timeoutTextBox.Text = xmlConnection.Element("Timeout").Value;
            sslModeComboBox.SelectedItem = (ComboBoxItem)sslModeComboBox.FindName(xmlConnection.Element("Sslmode").Value);
            commandTimeoutTextBox.Text = xmlConnection.Element("CommandTimeout").Value;
            connectionLifeTimeTextBox.Text = xmlConnection.Element("ConnectionLifeTime").Value;
            syncNotificationCheckBox.IsChecked = Convert.ToBoolean(xmlConnection.Element("SyncNotification").Value);
            enlistCheckBox.IsChecked = Convert.ToBoolean(xmlConnection.Element("Enlist").Value);
        }

        // Диалоговое окно перед закрытием
        private void closeApplication(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Если документ не сохранён и соединение не настроено
            if (!isSave)
            {
                MessageBoxResult result = MessageBox.Show("Если не будет настроено соединение с сервером программа прекратит работу. \nЗакрыть приложение?", "Настройте соединение с сервером", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.No)
                    e.Cancel = true;
                else
                    App.Current.Shutdown();
            }
        }


        // Нажатие на кнопку save (проверка соединения и сохранение xml файла)
        private void saveAndCloseWindow(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.IsEnabled = false;

            // Изменяем значения в xml
            xmlConnection.Element("Server").Value = serverTextBox.Text;
            xmlConnection.Element("Port").Value = portTextBox.Text;
            xmlConnection.Element("Database").Value = dataBaseTextBox.Text;
            xmlConnection.Element("User").Value = userTextBox.Text;
            xmlConnection.Element("Password").Value = passwordTextBox.Password;
            xmlConnection.Element("Protocol").Value = protocolTextBox.Text;
            xmlConnection.Element("IntegratedSecurity").Value = securityCheckBox.IsChecked.ToString();
            xmlConnection.Element("Ssl").Value = sslCheckBox.IsChecked.ToString();
            xmlConnection.Element("Pooling").Value = poolingCheckBox.IsChecked.ToString();
            xmlConnection.Element("MinPoolSize").Value = minPoolSizeTextBox.Text;
            xmlConnection.Element("MaxPoolSize").Value = maxPoolSizeTextBox.Text;
            xmlConnection.Element("Timeout").Value = timeoutTextBox.Text;
            xmlConnection.Element("CommandTimeout").Value = commandTimeoutTextBox.Text;
            xmlConnection.Element("Sslmode").Value = ((ComboBoxItem)sslModeComboBox.SelectedItem).Content.ToString();
            xmlConnection.Element("ConnectionLifeTime").Value = connectionLifeTimeTextBox.Text;
            xmlConnection.Element("SyncNotification").Value = syncNotificationCheckBox.IsChecked.ToString();
            xmlConnection.Element("Enlist").Value = enlistCheckBox.IsChecked.ToString();


          /// Соединение не установлено
            if (!testConection())
            {
                // Воспроизвести системный звук ошибки
                System.Media.SystemSounds.Beep.Play();

                Mouse.OverrideCursor = null;
                this.IsEnabled = true;

                // Анимация сообщения об ошибке (не удаётся подключиться к серверу)       
                DoubleAnimation amination = new DoubleAnimation(30, TimeSpan.FromSeconds(0.5));
                amination.Completed += (sndr, args) =>
                {
                    amination = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
                    amination.BeginTime = TimeSpan.FromSeconds(2.7);
                    errorTextBox.BeginAnimation(Label.HeightProperty, amination);
                };
                errorTextBox.BeginAnimation(Label.HeightProperty, amination); 

            }
          /// Соединение успешно установлено
            else
            {              
                // Созраняем xml
                xmlConnection.Save(App.xmlFileName);

                // Соединение установлено, xml сохранён
                isSave = true;

                // Создаём событие, соединение успешно установлено
                OnConnectionComplite(EventArgs.Empty);

                Mouse.OverrideCursor = null;

                // Закрываем окно
                this.Close();
            }           
        }       
     

        /// <summary>
        ///     Метод, проверяющий наличие соединения с postgreSQL сервером
        /// </summary>
        /// <returns>
        ///     true - соединение есть
        ///     false - соединения нет
        /// </returns>
        public static bool testConection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(getConnectionString());
            bool isConnection; 

            try
            {
                connection.Open();
                isConnection = true;
            }
            catch   { isConnection = false; }
            finally { connection.Close();   }

            return isConnection;
        }


        /// <summary>
        ///     Метод, возвращающий строку соединения из конфигурационного файла xml
        /// </summary>
        /// <returns>connectionString</returns>
        public static string getConnectionString()
        {
            string connectionString = "";

            connectionString = "Server=" + xmlConnection.Element("Server").Value +
                               "; Port=" + xmlConnection.Element("Port").Value +
                               "; Protocol=" + xmlConnection.Element("Protocol").Value +
                               "; Database=" + xmlConnection.Element("Database").Value +
                               "; User Id=" + xmlConnection.Element("User").Value +
                               "; Integrated Security=" + xmlConnection.Element("IntegratedSecurity").Value +
                               "; Password=" + xmlConnection.Element("Password").Value +
                               "; SSL=" + xmlConnection.Element("Ssl").Value +
                               "; Pooling=" + xmlConnection.Element("Pooling").Value +
                               "; MinPoolSize=" + xmlConnection.Element("MinPoolSize").Value +
                               "; MaxPoolSize=" + xmlConnection.Element("MaxPoolSize").Value +
                               "; Timeout=" + xmlConnection.Element("Timeout").Value +
                               "; CommandTimeout=" + xmlConnection.Element("CommandTimeout").Value +
                               "; Sslmode=" + xmlConnection.Element("Sslmode").Value +
                               "; ConnectionLifeTime=" + xmlConnection.Element("ConnectionLifeTime").Value +
                               "; SyncNotification=" + xmlConnection.Element("SyncNotification").Value +
                               "; Enlist=" + xmlConnection.Element("Enlist").Value + ";";

            return connectionString;
        }


        // Шаблон события (соединение установлено)
        protected virtual void OnConnectionComplite(EventArgs e)
        {
            if (ConnectionComplite != null)
                ConnectionComplite(this, e);
        }

    }
}
