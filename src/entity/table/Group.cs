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
    /// Класс - группы
    /// </summary>

    [Table("group", Schema = "public")]
    public class Group : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID группы (первичный ключ)

        [Column("group_name"), Required, DisplayName("Название группы")]
        public string groupName { get; set; }       // Название группы

        [Column("id_specialty"), Required, DisplayName("Идентификатор специальности")]
        public int idSpecialty { get; set; }        // ID специальности / факультета (внешний ключ)
        //[ForeignKey("idSpecialty")]

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
