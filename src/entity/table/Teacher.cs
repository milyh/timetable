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
        public int id { get; set; }                 // ID преподователя (первичный ключ)

        [Column, DisplayName("Имя")]
        public string firstname { get; set; }       // Имя преподователя

        [Column, DisplayName("Фамилия")]
        public string lastname { get; set; }        // Фамилия преподователя

        [Column, DisplayName("Отчество")]
        public string middlename { get; set; }      // Отчество преподователя

        [Column, DisplayName("Инициалы")]
        public string initials { get; set; }        // Инициалы преподователя 

        [Column, DisplayName("Телефон")]
        public string phone { get; set; }           // Телефон преподователя

        [Column, DisplayName("Адрес")]
        public string address { get; set; }          // Адрес преподователя

        [Column, DisplayName("E-mail")]
        public string email { get; set; }           // E-mail преподователя

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
