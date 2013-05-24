using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - правила (время, количество пар и день недели когда преподователь может вести занятия)
    /// </summary>

    [Table("regulation", Schema = "public")]
    public class Regulation : TableEntity
    {
        [Key]
        public int id { get; set; }                // ID правила (первичный ключ)

        //[ ForeignKey("teacher")]
        [Column("id_teacher"), Required, DisplayName("Идентификатор преподователя")]
        public int idTeacher { get; set; }         // ID преподователя (внешний ключ)

        [Required, DisplayName("Номер дня недели")]
        public int day { get; set; }               // День недели (скрыт, вместо него возвращается день недели из перечисления)

        [Column, Required, DisplayName("Свободные пары")]
        public byte[] lessons { get; set; }        // В какое время преподователь может вести занятие
        
        [Required, Column("max_lesson"), DisplayName("Максимальное количество занятий")]
        public int maxLesson { get; set; }          // Максимальное количество занятий, которые может вести преподователь в день

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий      
    }

    /// <summary>
    /// Перечислене - дни недели
    /// </summary>

    public enum DaysOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
}
