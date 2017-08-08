using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain;

namespace Repository
{
    public class UniversityContext:DbContext
    {
        public UniversityContext() : base("StudentsDb")
        {

        }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Dean> Deans { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GroupSubjectRelation> GroupSubjectRelations { get; set; }
        public DbSet<StudentSubjectRelation> StudentSubjectRelations { get; set; }
        public DbSet<TeacherSubjectRelation> TeacherSubjectRelations { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Offset> Offsets { get; set; }
        public DbSet<University> Universities { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UniversityContext>());

            //modelBuilder.Entity<Faculty>()
            //    .HasOptional(faculty => faculty.DeanId)
            //    .WithRequired(dean => dean.Faculty);
        }

    }
}
