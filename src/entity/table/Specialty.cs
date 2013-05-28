using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - специальность / факультет
    /// </summary>

    [Table("specialty", Schema = "public")]
    public class Specialty : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID специальности / факультета (первичный ключ)

        [Column("specialty_name"), Required, DisplayName("Название специальности")]
        public string specialtyName { get; set; }   // Название специаьлности

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
