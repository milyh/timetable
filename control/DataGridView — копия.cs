using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using timetable.src.entity;
using timetable.src.entity.attribute;
using timetable.src.entity.table;

namespace timetable.control
{
    public class DataGridView : DataGrid
    {
        public string tableName;                         // Название таблицы
        public string columnTitle;                       // Навзвание поля таблицы
        private IEnumerable<PropertyInfo> properties;    // Поля, у которых есть атрибут Title


        public DataGridView()       
        {
            this.AutoGenerateColumns = false;          
        }

        public void Change<T>(DbSet<T> tableData) where T : TableEntity
        {            
            tableName = tableData.FirstOrDefault().GetType().GetCustomAttributes(typeof(TableAttribute), true).Select(attr => ((TableAttribute)attr).Name).FirstOrDefault();
            properties = tableData.FirstOrDefault().GetType().GetProperties().Where(prop => prop.IsDefined(typeof(Title), false));

            // Конвертируем DbSet<T> в массив T, что бы обращаться по индексу
            T[] dataArray = tableData.ToArray<T>();

            System.Data.DataTable myTable = new System.Data.DataTable();

            // Цикл по всем свойствам, у которых есть атрибут Title
            foreach (PropertyInfo prop in properties)
            {
                columnTitle = prop.GetCustomAttributes(typeof(Title), true).Select(attr => ((Title)attr).Name).FirstOrDefault();

                //this.Columns.Add(new DataGridTemplateColumn { Header = columnTitle });

                myTable.Columns.Add(columnTitle);

                this.Columns.Add(new DataGridTextColumn()
                                {
                                    Header = columnTitle,
                                    Binding = new System.Windows.Data.Binding(String.Format("[{0}]", columnTitle))
                                });
               
                //System.Diagnostics.Debug.WriteLine(columnTitle + " __ " + prop.Name);       
            }         


            for (int i = 0; i < dataArray.Length; i++)
                foreach (PropertyInfo prop in properties)
                {
                    var o = dataArray[i].GetType().GetProperty(prop.Name).GetValue(dataArray[i], null);

                    System.Data.DataRow row = myTable.NewRow();
                    myTable.Rows.Add("2", "1", "3");

                    System.Diagnostics.Debug.WriteLine(o);
                }

            //this.DataContext = myTable.DefaultView;
            this.ItemsSource = myTable.DefaultView;
        }
    }
}
