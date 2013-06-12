using System.Xml.Linq;

namespace timetable.src
{
    public class XMLconnection
    {
        public static void CreateConnectionXML()
        {
            XDocument xml = new XDocument(
                new XComment(" Настройки соединения с PostgreSQL сервером "),
	            new XElement("connectionString",

                    new XComment(" IP адрес или имя PostgresSQL сервера "),
                    new XElement("Server", "localhost"),

                    new XComment(" Порт для соединения с сервером "),
                    new XElement("Port", "5432"),
                    
                    new XComment(" Настройки версии протокола "),
                    new XElement("Protocol", "3"),
                    
                    new XComment(" Имя базы данных "),
                    new XElement("Database", "schedule"),

                    new XComment(" Имя пользователя "),
                    new XElement("User", "postgres"),
                    
                    new XComment(" Windows Аутентификация "),
                    new XElement("IntegratedSecurity", "False"),

                    new XComment(" Пароль пользователя "),
                    new XElement("Password", "postgres"),

                    new XComment(" Установить безопасное соединение "),
                    new XElement("Ssl", "False"),
                    
                    new XComment(" Использовать пул соединения "),
                    new XElement("Pooling", "True"),
                    
                    new XComment(" Минимальный размер пула соединения. Предварительно выделяет количество соединения с серваром "),
                    new XElement("MinPoolSize", "1"),

                    new XComment(" Максимальный размер пула соединения. Соединения будут удалятсья, если пул переполнен "),
                    new XElement("MaxPoolSize", "20"),
                    
                    new XComment(" Время ожидания для подключения "),
                    new XElement("Timeout", "15"),

                    new XComment(" Время ожидания окончания команды до вызова исключения (секунды) "),
                    new XElement("CommandTimeout", "20"),

                    new XComment(" Тип ssl соединения: "),
                    new XComment("Prefer - по возможности будет использоваться подключение через SLL или SSL"),
                    new XComment("Require - выкидывать исключение, если SSL соединение н е может быть установлено"),
                    new XComment("Disable - отключить SSL"),
                    new XElement("Sslmode", "Disable"),

                    new XComment(" Время ожидания до закрытия неиспользуемых соединений в пуле (секунды) "),
                    new XElement("ConnectionLifeTime", "15"),

                    new XComment(" Использовать синхронные уведомления "),
                    new XElement("SyncNotification", "False"),
                    
                    new XComment(" Поддержка транзакций. Экспериментальная функция, если есть проблемы поставить значение False "),
                    new XElement("Enlist", "True")
                    ));


            xml.Save(App.xmlFileName);
        }

    }
}
