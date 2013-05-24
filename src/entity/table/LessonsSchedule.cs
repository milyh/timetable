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
    /// Класс - расписание звонков
    /// </summary>

    [Table("lessons_schedule", Schema = "public")]
    public class LessonsSchedule : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID пары (первичный ключ)

        [Column("begin_time"), Required, DisplayName("Время начала")]
        public string beginTime { get; set; }     // Время начала пары

        [Column("end_time"), Required, DisplayName("Время окончания")]
        public string endTime { get; set; }       // Время окончания пары

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
