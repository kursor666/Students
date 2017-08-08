using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class TeacherSubjectRelationService : BaseService
    {
        public TeacherSubjectRelationService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(TeacherSubjectRelationDTO relation)
        {
            if (relation == null)
                throw new ValidationException("Связь не найдена", "");
            TeacherSubjectRelation addingRelation =
                AutoMap<TeacherSubjectRelationDTO, TeacherSubjectRelation>
                .Map(relation);
            Database.TeacherSubjectRelations.Add(addingRelation);
            Database.Commit();
        }

        #endregion

        #region Get

        public TeacherSubjectRelationDTO GetById(int? id)
        {
            try
            {
                var findingRelation = ValidateModule.ValidateTeacherSubjectRelation(id);
                return AutoMap<TeacherSubjectRelation, TeacherSubjectRelationDTO>.Map(findingRelation);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<TeacherSubjectRelationDTO> GetAll =>
            AutoMap<IEnumerable<TeacherSubjectRelation>,
                    IEnumerable<TeacherSubjectRelationDTO>>
                .Map(Database
                    .TeacherSubjectRelations
                    .GetAll());

        public IEnumerable<TeacherSubjectRelationDTO> GetMany
            (Expression<Func<TeacherSubjectRelationDTO, bool>> predicate)
        {
            var relationPredicate =
                AutoMap<Expression<Func<TeacherSubjectRelationDTO, bool>>,
                    Expression<Func<TeacherSubjectRelation, bool>>>
                    .Map(predicate);
            IEnumerable<TeacherSubjectRelation> findingRelations =
                Database
                .TeacherSubjectRelations
                .GetMany(relationPredicate);
            return AutoMap<IEnumerable<TeacherSubjectRelation>,
                IEnumerable<TeacherSubjectRelationDTO>>
                .Map(findingRelations);
        }

        public IEnumerable<TeacherSubjectRelationDTO> GetManyByTeacherId(int? teacherId)
        {
            try
            {
                ValidateModule.ValidateTeacher(teacherId);
                return GetMany(relation => relation.TeacherId == teacherId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<TeacherSubjectRelationDTO> GetManyBySubjectId(int? subjectId)
        {
            try
            {
                ValidateModule.ValidateSubject(subjectId);
                return GetMany(relation => relation.SubjectId == subjectId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(TeacherSubjectRelationDTO relation)
        {
            try
            {
                if (relation == null)
                    throw new ValidationException("Связь не найдена.", "");
                var findingRelation = ValidateModule.ValidateTeacherSubjectRelation(relation.Id);
                findingRelation = AutoMap<TeacherSubjectRelationDTO,
                        TeacherSubjectRelation>
                    .Map(relation, findingRelation);
                Database
                    .TeacherSubjectRelations
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
                var deleteRelation = ValidateModule.ValidateTeacherSubjectRelation(id);
                Database.TeacherSubjectRelations.Delete(deleteRelation);
                Database.Commit();
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
                var deleteRelations = Database
                    .TeacherSubjectRelations
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
                var deleteRelations = Database
                    .TeacherSubjectRelations
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