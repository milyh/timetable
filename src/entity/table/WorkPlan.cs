using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        [Column("begin_date"), DisplayName("Дата начала семестра")]
        public string beginDate { get; set; }       // Дата начала семестра

        [Column("end_date"), DisplayName("Дата конца семестра")]
        public string endDate { get; set; }         // Дата конца семестра

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий
    }
}
