using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - преподователь
    /// </summary>

    [Table("teacher", Schema = "public")]
    public class Teacher : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID преподавателя (первичный ключ)

        [Column, DisplayName("Фамилия")]
        public string lastname { get; set; }        // Фамилия преподавателя

        [Column, DisplayName("Имя")]
        public string firstname { get; set; }       // Имя преподавателя

        [Column, DisplayName("Отчество")]
        public string middlename { get; set; }      // Отчество преподавателя

        [Column, DisplayName("Инициалы")]
        public string initials { get; set; }        // Инициалы преподавателя 

        [Column, DisplayName("Телефон")]
        public string phone { get; set; }           // Телефон преподавателя

        [Column, DisplayName("Адрес")]
        public string address { get; set; }          // Адрес преподавателя

        [Column, DisplayName("E-mail")]
        public string email { get; set; }           // E-mail преподавателя

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
