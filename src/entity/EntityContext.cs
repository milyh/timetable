using System.Data.Common;
using System.Data.Entity;
using timetable.src.entity.table;

namespace timetable.src.entity
{
    /// <summary>
    /// Класс - контекст. Коннектится с базой данных и соединяет классы - сущности
    /// </summary>

    public class EntityContext : DbContext
    {
        public EntityContext(DbConnection connection)         
            : base(connection, true)
        {
        }

        public DbSet<Classroom> classroom { get; set; }
        public DbSet<Group> group { get; set; }                         // внешний ключ ?
        public DbSet<LessonsSchedule> lessonsSchedule { get; set; }
        public DbSet<Regulation> regulation { get; set; }               // массив ? внешний ключ ?
        public DbSet<Specialty> specialty { get; set; }
        public DbSet<Subject> subject { get; set; }
        public DbSet<Teacher> teacher { get; set; }
        public DbSet<WorkPlan> workPlan { get; set; }                   // внешний ключ ?
    }
}
