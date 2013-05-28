using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using timetable.src.entity;


namespace timetable.control
{
    public class DataGridView : DataGrid
    {
        public string tableName;                         // Название таблицы
        public string columnTitle;                       // Навзвание поля таблицы
        private IEnumerable<PropertyInfo> properties;    // Поля, у которых есть атрибут Title      
        private DataTable dataTable;                     // Структура с данными (отображается на DataGrid)
        private DataRow row;                             // Ячейка для значений

        public DataGridView()
        {
            // Автоматическая ширина столбцов (на всю возможную ширину)
            this.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Auto);
            //this.HeadersVisibility = DataGridHeadersVisibility.All;

            // Убираем последнюю / нижнюю пустую строку
            this.CanUserAddRows = false;
            // Запрещает редактировать ячейки
            this.IsReadOnly = true;

            dataTable = new DataTable();
            
            // Перед редактированием
            this.BeginningEdit += (obj, sndr) => 
                                                {
                                                    DataRowView current = (DataRowView)this.SelectedItem;
                                                    System.Diagnostics.Debug.WriteLine(current.Row[0]);
                                                    System.Diagnostics.Debug.WriteLine(sndr.Column.Header);
                                                };
            // После редактирования
            this.CellEditEnding += (obj, sndr) =>
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("после редактированием");
                                                        this.IsReadOnly = true;     // Запрещает редактировать                                                        
                                                    };

            // Создаём контекстное меню
            ContextMenu menu = new ContextMenu();
            MenuItem add = new MenuItem { Header = "Добавить" };
            MenuItem edit = new MenuItem { Header = "Редактировать" };
                     edit.Click += (obj, sndr) => 
                                                { 
                                                    this.IsReadOnly = false;       // Разрешает редактировать
                                                    this.BeginEdit();              // Начинает редактирвоать выбранную ячейку
                                                }; 
            MenuItem delete = new MenuItem { Header = "Удалить" };

            menu.Items.Add(add);
            menu.Items.Add(edit);
            menu.Items.Add(delete);

            // Устанавливаем контекстное меню для rows таблицы
            System.Windows.Style rowStyle = new System.Windows.Style { TargetType = typeof(DataGridRow) };
            rowStyle.Setters.Add(new System.Windows.Setter(Control.ContextMenuProperty, menu));
            this.RowStyle = rowStyle;          
        }

        public void Change<T>(DbSet<T> tableData) where T : TableEntity
        {
            this.ItemsSource = null;
            
            dataTable.Columns.Clear();   // Удаляем все колонки из структуры
            dataTable.Clear();           // Удаляем все строки (ячейки)            
            
            tableName = tableData.FirstOrDefault().GetType().GetCustomAttributes(typeof(TableAttribute), true).Select(attr => ((TableAttribute)attr).Name).FirstOrDefault();
            properties = tableData.FirstOrDefault().GetType().GetProperties().Where(prop => prop.IsDefined(typeof(DisplayNameAttribute), false));

            // Конвертируем DbSet<T> в массив T, что бы обращаться по индексу
            T[] dataArray = tableData.ToArray<T>();

            // Цикл по всем свойствам, у которых есть атрибут Title
            //foreach (PropertyInfo prop in properties)
            //{
            //    columnTitle = prop.GetCustomAttributes(typeof(Title), true).Select(attr => ((Title)attr).Name).FirstOrDefault();

            //    // Создание колонки с названием из атрибута Title
            //    dataTable.Columns.Add(columnTitle, prop.PropertyType);
            //}

            // Заполнение DataGrid данными
            for (int i = 0; i < dataArray.Length; i++)
            {
                row = dataTable.NewRow();

                foreach (PropertyInfo prop in properties)
                {              
                    // Название колонки    
                    columnTitle = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).Select(attr => ((DisplayNameAttribute)attr).DisplayName).FirstOrDefault();
                    
                    // Создаём колонку, если её нет
                    if (!dataTable.Columns.Contains(columnTitle))
                        dataTable.Columns.Add(columnTitle, prop.PropertyType);

                    // Значение ячейки
                    row[columnTitle] = dataArray[i].GetType().GetProperty(prop.Name).GetValue(dataArray[i], null);                     
                }

                dataTable.Rows.Add(row);
            }            

            this.ItemsSource = dataTable.DefaultView;
        }
    }
}
