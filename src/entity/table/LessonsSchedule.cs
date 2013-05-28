using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - расписание звонков
    /// </summary>

    [Table("lessons_schedule", Schema = "public")]
    public class LessonsSchedule : TableEntity
    {
        [Key]
        public int id { get; set; }               // ID пары (первичный ключ)

        [Column("begin_time"), Required, DisplayName("Время начала")]
        public string beginTime { get; set; }     // Время начала пары

        [Column("end_time"), Required, DisplayName("Время окончания")]
        public string endTime { get; set; }       // Время окончания пары

        [Column("number_lesson"), Required, DisplayName("Номер занятия")]
        public int numberLesson { get; set; }     // Номер занятия

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }   // Комментарий
    }
}
