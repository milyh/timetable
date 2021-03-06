﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace timetable.src.entity.table
{
    /// <summary>
    /// Класс - правила (время, количество пар и день недели когда преподаватель может вести занятия)
    /// </summary>

    [Table("regulation", Schema = "public")]
    public class Regulation : TableEntity
    {
        [Key]
        public int id { get; set; }                // ID правила (первичный ключ)

        //[ ForeignKey("teacher")]
        [Column("id_teacher"), DisplayName("Идентификатор преподователя")]
        public int idTeacher { get; set; }         // ID преподавателя (внешний ключ)

        [DisplayName("Номер дня недели")]
        public int day { get; set; }               // День недели (скрыт, вместо него возвращается день недели из перечисления)

        [Column, DisplayName("Свободные пары")]
        public string lessons { get; set; }        // В какое время преподаватель может вести занятие
        
        [Column("max_lesson"), DisplayName("Максимальное количество занятий")]
        public int maxLesson { get; set; }          // Максимальное количество занятий, которые может вести преподаватель в день

        [Column, DisplayName("Комментарий")]
        public string description { get; set; }     // Комментарий      
    }
}
