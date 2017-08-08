using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class SubjectService : BaseService
    {
        public SubjectService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(SubjectDTO subject)
        {
            if (subject == null)
                throw new ValidationException("Предмет не найден.", "");
            var addingSubject = AutoMap<SubjectDTO, Subject>.Map(subject);
            Database.Subjects.Add(addingSubject);
            Database.Commit();
        }

        #endregion

        #region Get

        public SubjectDTO GetById(int? id)
        {
            try
            {
                var findingSubject = ValidateModule.ValidateSubject(id);
                return AutoMap<Subject, SubjectDTO>.Map(findingSubject);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<SubjectDTO> GetAll =>
            AutoMap<IEnumerable<Subject>,
                    IEnumerable<SubjectDTO>>
                .Map(Database
                    .Subjects
                    .GetAll());

        public IEnumerable<SubjectDTO> GetMany(Expression<Func<SubjectDTO, bool>> predicate)
        {
            var subjectPredicate = AutoMap<Expression<Func<SubjectDTO, bool>>,
                Expression<Func<Subject, bool>>>
                .Map(predicate);
            IEnumerable<Subject> findingSubjects = Database
                .Subjects
                .GetMany(subjectPredicate);
            return AutoMap<IEnumerable<Subject>,
                IEnumerable<SubjectDTO>>
                .Map(findingSubjects);
        }

        #endregion

        #region Edit

        public void Edit(SubjectDTO subject)
        {
            try
            {
                if (subject == null)
                    throw new ValidationException("Предмет не найден.","");
                var findingSubject = ValidateModule.ValidateSubject(subject.Id);
                findingSubject = AutoMap<SubjectDTO, Subject>.Map(subject, findingSubject);
                Database.Subjects.Edit(findingSubject);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Delete

        public void Delete(int? id)
        {
            try
            {
                var deleteSubject = ValidateModule.ValidateSubject(id);
                new TeacherSubjectRelationService(Database).DeleteManyBySubjectId(id);
                new StudentSubjectTeacherRelationService(Database).DeleteManyBySubjectId(id);
                new GroupSubjectTeacherRelationService(Database).DeleteManyBySubjectId(id);
                new ExamService(Database).DeleteManyBySubjectId(id);
                new OffsetService(Database).DeleteManyBySubjectId(id);
                Database.Subjects.Delete(deleteSubject);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

    }
}