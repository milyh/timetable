using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - аудитория
    /// </summary>

    [Table("classroom", Schema = "public")]
    public class Classroom : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID аудитории (первичный ключ)

        [Column("class_name"), DisplayName("Название класса")]
        public string className { get; set; }       // Название аудитории

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
