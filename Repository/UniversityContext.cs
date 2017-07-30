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
        public DbSet<AttendingGroupSubject> AttendingGroupSubjects { get; set; }
        public DbSet<AttendingStudentSubject> AttendingStudentSubjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Offset> Offsets { get; set; }
        public DbSet<University> Universities { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UniversityContext>());

            modelBuilder.Entity<Faculty>()
                .HasOptional(faculty => faculty.Dean)
                .WithRequired(dean => dean.Faculty);
        }

    }
}
