using System;
using System.Reflection;
using Domain;
using Ninject;
using Ninject.Parameters;
using Repository.Interfaces;

namespace Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        readonly UniversityContext _context = new UniversityContext();

        readonly IKernel _ninjectKernel = new StandardKernel();

        private readonly ConstructorArgument _constructorArgumentContext;


        public UnitOfWork()
        {
            //_ninjectKernel.Load(Assembly.GetExecutingAssembly());
            _constructorArgumentContext = new ConstructorArgument("context", _context);
        }

        private IDbRepository<University> _universityRepository;
        private IDbRepository<Teacher> _teacherRepository;
        private IDbRepository<Subject> _subjectRepository;
        private IDbRepository<Student> _studentRepository;
        private IDbRepository<Exam> _examRepository;
        private IDbRepository<Offset> _offsetRepository;
        private IDbRepository<Group> _groupRepository;
        private IDbRepository<Faculty> _facultyRepository;
        private IDbRepository<Dean> _deanRepository;
        private IDbRepository<AttendingStudentSubject> _attendingStudentSubjectRepository;
        private IDbRepository<AttendingGroupSubject> _attendingGroupSubjectRepository;

        public IDbRepository<University> Universities => _universityRepository ??
                                                         (_universityRepository = _ninjectKernel
                                                             .Get<IDbRepository<University>>(
                                                                 _constructorArgumentContext));

        public IDbRepository<Teacher> Teachers => _teacherRepository ??
                                                  (_teacherRepository = _ninjectKernel
                                                      .Get<IDbRepository<Teacher>>(
                                                          _constructorArgumentContext));

        public IDbRepository<Subject> Subjects => _subjectRepository ??
                                                  (_subjectRepository = _ninjectKernel
                                                      .Get<IDbRepository<Subject>>(
                                                          _constructorArgumentContext));

        public IDbRepository<Student> Students => _studentRepository ??
                                                  (_studentRepository = _ninjectKernel
                                                      .Get<IDbRepository<Student>>(
                                                          _constructorArgumentContext));

        public IDbRepository<Exam> Exams => _examRepository ??
                                            (_examRepository = _ninjectKernel
                                                .Get<IDbRepository<Exam>>(
                                                    _constructorArgumentContext));

        public IDbRepository<Offset> Offsets => _offsetRepository ??
                                                (_offsetRepository = _ninjectKernel
                                                    .Get<IDbRepository<Offset>>(
                                                        _constructorArgumentContext));

        public IDbRepository<Group> Groups => _groupRepository ??
                                              (_groupRepository = _ninjectKernel
                                                  .Get<IDbRepository<Group>>(
                                                      _constructorArgumentContext));

        public IDbRepository<Faculty> Faculties => _facultyRepository ??
                                                   (_facultyRepository = _ninjectKernel
                                                       .Get<IDbRepository<Faculty>>(
                                                           _constructorArgumentContext));

        public IDbRepository<Dean> Deans => _deanRepository ??
                                            (_deanRepository = _ninjectKernel
                                                .Get<IDbRepository<Dean>>(
                                                    _constructorArgumentContext));

        public IDbRepository<AttendingStudentSubject> AttendingStudentSubjects =>
            _attendingStudentSubjectRepository ??
            (_attendingStudentSubjectRepository = _ninjectKernel
                .Get<IDbRepository<AttendingStudentSubject>>(
                    _constructorArgumentContext));

        public IDbRepository<AttendingGroupSubject> AttendingGroupSubjects =>
            _attendingGroupSubjectRepository ??
            (_attendingGroupSubjectRepository = _ninjectKernel
                .Get<IDbRepository<AttendingGroupSubject>>(
                    _constructorArgumentContext));


        public void Commit()
        {
            _context.SaveChanges();
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}