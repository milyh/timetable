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
    /// Класс - рабочий план
    /// </summary>

    [Table("work_plan", Schema = "public")]
    public class WorkPlan : TableEntity
    {
        [Key]
        public int id { get; set; }                 // ID рабочего плана (первичный ключ)

        [Column("id_teacher"), Required, DisplayName("Идентификатор преподователя")]
        public int idTeacher { get; set; }          // ID преподователя (внешний ключь)
        //[ForeignKey("idTeacher")]

        [Column("id_subject"), Required, DisplayName("Идентификатор предмета")]
        public int idSubject { get; set; }          // ID предмета (внешний ключ)
        //[ForeignKey("idSubject")]

        [Column("lectures_time"), DisplayName("Чассы для лекционных занятий")]
        public int lecturesTime { get; set; }       // Время отводимое на лекции

        [Column("practice_time"), DisplayName("Часы на практические работы")]
        public int practiceTime { get; set; }       // Время отводимое для практических занятий

        [Column("laboratory_time"), DisplayName("Часы на лабораторные работы")]
        public int laboratoryTime { get; set; }     // Время отводимое для лабораторных занятий

        [Column, Required, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
