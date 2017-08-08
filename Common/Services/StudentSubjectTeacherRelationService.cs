using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class StudentSubjectTeacherRelationService : BaseService
    {

        public StudentSubjectTeacherRelationService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(StudentSubjectTeacherRelationDTO relation)
        {
            try
            {
                ValidateModule.ValidateStudent(relation.StudentId);
                ValidateModule.ValidateSubject(relation.SubjectId);
                ValidateModule.ValidateTeacher(relation.TeacherId);
                StudentSubjectRelation studentSubjectRelation =
                    AutoMap<StudentSubjectTeacherRelationDTO, StudentSubjectRelation>.Map(relation);
                Database.StudentSubjectRelations.Add(studentSubjectRelation);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;// new ValidationException(ex.Message, ex.Property);
            }
        }

        #endregion

        #region Get

        public StudentSubjectTeacherRelationDTO GetById(int? id)
        {
            try
            {
                StudentSubjectRelation findingRelation =
                    ValidateModule.ValidateStudentSubjectTeacherRelation(id);
                return AutoMap<StudentSubjectRelation, StudentSubjectTeacherRelationDTO>.Map(findingRelation);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<StudentSubjectTeacherRelationDTO> GetAll =>
            AutoMap<IEnumerable<StudentSubjectRelation>, IEnumerable<StudentSubjectTeacherRelationDTO>>
            .Map(Database
                .StudentSubjectRelations
                .GetAll());

        public IEnumerable<StudentSubjectTeacherRelationDTO>
            GetMany(Expression<Func<StudentSubjectTeacherRelationDTO, bool>> predicate)
        {
            var relationPredicate =
                AutoMap<Expression<Func<StudentSubjectTeacherRelationDTO, bool>>,
                    Expression<Func<StudentSubjectRelation, bool>>>.Map(predicate);
            IEnumerable<StudentSubjectRelation> findingRelations =
                Database
                .StudentSubjectRelations
                .GetMany(relationPredicate);
            return AutoMap<IEnumerable<StudentSubjectRelation>,
                IEnumerable<StudentSubjectTeacherRelationDTO>>
                .Map(findingRelations);
        }

        public IEnumerable<StudentSubjectTeacherRelationDTO> GetManyByStudentId(int? studentId)
        {
            try
            {
                ValidateModule.ValidateStudent(studentId);
                return GetMany(student => student.StudentId == studentId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<StudentSubjectTeacherRelationDTO> GetManyBySubjectId(int? subjectId)
        {
            try
            {
                ValidateModule.ValidateSubject(subjectId);
                return GetMany(student => student.SubjectId == subjectId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<StudentSubjectTeacherRelationDTO> GetManyByTeacherId(int? teacherId)
        {
            try
            {
                ValidateModule.ValidateTeacher(teacherId);
                return GetMany(student => student.TeacherId == teacherId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(StudentSubjectTeacherRelationDTO relation)
        {
            try
            {
                if (relation == null)
                    throw new ValidationException("Связь не найдена.", "");
                var findingRelation = ValidateModule.ValidateStudentSubjectTeacherRelation(relation.Id);
                findingRelation = AutoMap<StudentSubjectTeacherRelationDTO,
                        StudentSubjectRelation>
                    .Map(relation, findingRelation);
                Database
                    .StudentSubjectRelations
                    .Edit(findingRelation);
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
                var findingRelation = ValidateModule.ValidateStudentSubjectTeacherRelation(id);
                Database.StudentSubjectRelations.Delete(findingRelation);
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
                IEnumerable<StudentSubjectRelation> deleteRelations =
                    Database.StudentSubjectRelations
                        .GetMany(relation => relation.StudentId == studentId);
                foreach (var relation in deleteRelations)
                    Delete(relation.Id);
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
                IEnumerable<StudentSubjectRelation> deleteRelations =
                    Database.StudentSubjectRelations
                        .GetMany(relation => relation.TeacherId == teacherId);
                foreach (var relation in deleteRelations)
                    Delete(relation.Id);
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
                IEnumerable<StudentSubjectRelation> deleteRelations =
                    Database.StudentSubjectRelations
                        .GetMany(relation => relation.SubjectId == subjectId);
                foreach (var relation in deleteRelations)
                    Delete(relation.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

    }
}