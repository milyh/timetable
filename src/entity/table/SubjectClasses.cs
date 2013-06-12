using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - аудитория
    /// </summary>

    [Table("subject_classes", Schema = "public")]
    public class SubjectClasses : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID (первичный ключ)

        [Column("id_subject")]
        public int idSubject { get; set; }       // ID предмета

        [Column("id_classroom")]
        public int idClassroom { get; set; }     // ID аудитории
    }
}