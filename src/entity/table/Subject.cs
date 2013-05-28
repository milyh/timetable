using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - предметы
    /// </summary>

    [Table("subject", Schema = "public")]
    public class Subject : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID предмета (первичный ключ)

        [Column("subject_name"), Required, DisplayName("Название предмета")]
        public string subjectName { get; set; }     // Название предмета

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
