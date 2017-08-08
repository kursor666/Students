using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Common.Interfaces;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(TeacherDTO teacher)
        {
            if (teacher == null)
                throw new ValidationException("Преподаватель не найден", "");
            var addingTeacher = AutoMap<TeacherDTO, Teacher>.Map(teacher);
            Database.Teachers.Add(addingTeacher);
            Database.Commit();
        }

        #endregion

        #region Get

        public TeacherDTO GetById(int? id)
        {
            try
            {
                var findingTeacher = ValidateModule.ValidateTeacher(id);
                return AutoMap<Teacher, TeacherDTO>.Map(findingTeacher);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<TeacherDTO> GetAll() =>
            AutoMap<IEnumerable<Teacher>,
                    IEnumerable<TeacherDTO>>
                .Map(Database
                    .Teachers
                    .GetAll().Where(teacher => teacher.IsTeacher));

        public IEnumerable<TeacherDTO> GetMany(Expression<Func<TeacherDTO, bool>> predicate)
        {
            var teacherPredicate = AutoMap<Expression<Func<TeacherDTO, bool>>,
                    Expression<Func<Teacher, bool>>>
                .Map(predicate);
            IEnumerable<TeacherDTO> findingTeachers =
                AutoMap<IQueryable<Teacher>, List<TeacherDTO>>
                    .Map(Database
                        .Teachers
                        .GetMany(teacherPredicate).Where(teacher => teacher.IsTeacher));
            return findingTeachers;
        }

        public IEnumerable<TeacherDTO> GetManyByUniversityId(int? universityId)
        {
            try
            {
                ValidateModule.ValidateUniversity(universityId);
                return GetMany(teacher => teacher.UniversityId == universityId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(TeacherDTO teacher)
        {
            try
            {
                if (teacher == null)
                    throw new ValidationException("Преподаватель не найден", "");
                var findingTeacher = ValidateModule.ValidateTeacher(teacher.Id);
                findingTeacher = AutoMap<TeacherDTO, Teacher>.Map(teacher, findingTeacher);
                Database.Teachers.Edit(findingTeacher);
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
                var deleteTeacher = ValidateModule.ValidateTeacher(id);
                new TeacherSubjectRelationService(Database).DeleteManyByTeacherId(id);
                new StudentSubjectTeacherRelationService(Database).DeleteManyByTeacherId(id);
                new GroupSubjectTeacherRelationService(Database).DeleteManyByTeacherId(id);
                new ExamService(Database).DeleteManyByTeacherId(id);
                new OffsetService(Database).DeleteManyByTeacherId(id);
                Database.Teachers.Delete(deleteTeacher);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByUniversityId(int? universityId)
        {
            try
            {
                ValidateModule.ValidateUniversity(universityId);
                foreach (var teacher in GetManyByUniversityId(universityId))
                    Delete(teacher.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion
    }
}