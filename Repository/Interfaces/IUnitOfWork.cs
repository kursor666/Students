using Domain;
using Repository.Interfaces;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IDbRepository<GroupSubjectRelation> GroupSubjectRelations { get; }
        IDbRepository<StudentSubjectRelation> StudentSubjectRelations { get; }
        IDbRepository<TeacherSubjectRelation> TeacherSubjectRelations { get; }
        IDbRepository<Dean> Deans { get; }
        IDbRepository<Exam> Exams { get; }
        IDbRepository<Faculty> Faculties { get; }
        IDbRepository<Group> Groups { get; }
        IDbRepository<Offset> Offsets { get; }
        IDbRepository<Student> Students { get; }
        IDbRepository<Subject> Subjects { get; }
        IDbRepository<Teacher> Teachers { get; }
        IDbRepository<University> Universities { get; }

        void Commit();
        void Dispose();
    }
}