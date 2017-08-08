using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class ExamService : BaseService
    {
        public ExamService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(ExamDTO exam)
        {
            if (exam == null)
                throw new ValidationException("Оценка не найдена", "");
            Exam addingExam = AutoMap<ExamDTO, Exam>.Map(exam);
            Database.Exams.Add(addingExam);
            Database.Commit();
        }

        #endregion

        #region Get

        public ExamDTO GetById(int? id)
        {
            Exam findingExam;
            try
            {
                findingExam = ValidateModule.ValidateExam(id);
            }
            catch (ValidationException)
            {
                throw;
            }
            return AutoMap<Exam, ExamDTO>.Map(findingExam);
        }

        public IEnumerable<ExamDTO> GetMany(Expression<Func<ExamDTO, bool>> predicate)
        {
            var examPredicate = AutoMap<Expression<Func<ExamDTO, bool>>,
                    Expression<Func<Exam, bool>>>
                .Map(predicate);
            IEnumerable<ExamDTO> findingExams =
                AutoMap<IQueryable<Exam>, List<ExamDTO>>
                    .Map(Database
                        .Exams
                        .GetMany(examPredicate));
            return findingExams;
        }

        public IEnumerable<ExamDTO> GetAll() => AutoMap<IEnumerable<Exam>, IEnumerable<ExamDTO>>
            .Map(Database
                .Exams
                .GetAll());

        public IEnumerable<ExamDTO> GetManyByStudentId(int? studentId)
        {
            try
            {
                ValidateModule.ValidateStudent(studentId);
                return GetMany(exam => exam.StudentId == studentId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<ExamDTO> GetManyByTeacherId(int? teacherId)
        {
            try
            {
                ValidateModule.ValidateTeacher(teacherId);
                return GetMany(exam => exam.TeacherId == teacherId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<ExamDTO> GetManyBySubjectId(int? subjectId)
        {
            try
            {
                ValidateModule.ValidateSubject(subjectId);
                return GetMany(exam => exam.SubjectId == subjectId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<ExamDTO> GetManyByUniversityId(int? universityId)
        {
            try
            {
                ValidateModule.ValidateUniversity(universityId);
                return GetMany(exam => exam.UniversityId == universityId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(ExamDTO exam)
        {
            try
            {
                if (exam==null)
                    throw new ValidationException("Экзамен не найден.","");
                var findingExam = ValidateModule.ValidateExam(exam.Id);
                findingExam = AutoMap<ExamDTO, Exam>.Map(exam, findingExam);
                Database.Exams.Edit(findingExam);
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
                Exam findingExam = ValidateModule.ValidateExam(id);
                Database.Exams.Delete(findingExam);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByStudentId(int? studentId)
        {
            try
            {
                ValidateModule.ValidateStudent(studentId);
                IEnumerable<Exam> deleteExams =
                    Database.Exams
                        .GetMany(exam => exam.StudentId == studentId);
                foreach (var exam in deleteExams)
                    Delete(exam.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByTeacherId(int? teacherId)
        {
            try
            {
                ValidateModule.ValidateTeacher(teacherId);
                IEnumerable<Exam> deleteExams =
                    Database.Exams
                        .GetMany(exam => exam.TeacherId == teacherId);
                foreach (var exam in deleteExams)
                    Delete(exam.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyBySubjectId(int? subjectId)
        {
            try
            {
                ValidateModule.ValidateSubject(subjectId);
                IEnumerable<Exam> deleteExams =
                    Database.Exams
                        .GetMany(exam => exam.SubjectId == subjectId);
                foreach (var exam in deleteExams)
                    Delete(exam.Id);
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
                foreach (var exam in GetManyByUniversityId(universityId))
                {
                    Delete(exam.Id);
                }
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

    }
}