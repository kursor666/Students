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
    public class GroupSubjectTeacherRelationService : BaseService
    {

        public GroupSubjectTeacherRelationService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(GroupSubjectTeacherRelationDTO relation)
        {
            try
            {
                ValidateModule.ValidateStudent(relation.GroupId);
                ValidateModule.ValidateSubject(relation.SubjectId);
                ValidateModule.ValidateTeacher(relation.TeacherId);
                GroupSubjectRelation groupSubjectRelation =
                    AutoMap<GroupSubjectTeacherRelationDTO, GroupSubjectRelation>.Map(relation);
                Database.GroupSubjectRelations.Add(groupSubjectRelation);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;// new ValidationException(ex.Message, ex.Property);
            }
        }

        #endregion

        #region Get

        public GroupSubjectTeacherRelationDTO GetById(int? id)
        {
            try
            {
                var findingRelation = ValidateModule.ValidateGroupSubjectTeacherRelation(id);
                return AutoMap<GroupSubjectRelation, GroupSubjectTeacherRelationDTO>.Map(findingRelation);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<GroupSubjectTeacherRelationDTO> GetMany
            (Expression<Func<GroupSubjectTeacherRelationDTO, bool>> predicate)
        {
            var relationPredicate = AutoMap<Expression<Func<GroupSubjectTeacherRelationDTO, bool>>,
                Expression<Func<GroupSubjectRelation, bool>>>
                .Map(predicate);
            var findingRelations = Database
                .GroupSubjectRelations
                .GetMany(relationPredicate);
            return AutoMap<IEnumerable<GroupSubjectRelation>,
                IEnumerable<GroupSubjectTeacherRelationDTO>>
                .Map(findingRelations);
        }

        public IEnumerable<GroupSubjectTeacherRelationDTO> GetAll =>
            AutoMap<IEnumerable<GroupSubjectRelation>,
                    IEnumerable<GroupSubjectTeacherRelationDTO>>
                .Map(Database
                    .GroupSubjectRelations
                    .GetAll());

        public IEnumerable<GroupSubjectTeacherRelationDTO> GetManyByGroupId(int? groupId)
        {
            try
            {
                ValidateModule.ValidateGroup(groupId);
                return GetMany(relation => relation.GroupId == groupId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<GroupSubjectTeacherRelationDTO> GetManyBySubjectId(int? subjectId)
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

        public IEnumerable<GroupSubjectTeacherRelationDTO> GetManyByTeacherId(int? teacherId)
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

        #endregion

        #region Edit

        public void Edit(GroupSubjectTeacherRelationDTO relation)
        {
            try
            {
                if (relation == null)
                    throw new ValidationException("Связь не найдена.", "");
                var findingRelation = ValidateModule.ValidateGroupSubjectTeacherRelation(relation.Id);
                findingRelation = AutoMap<GroupSubjectTeacherRelationDTO,
                        GroupSubjectRelation>
                    .Map(relation, findingRelation);
                Database
                    .GroupSubjectRelations
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

        public void Delete(int? relationId)
        {
            try
            {
                var findingRelation = ValidateModule.ValidateGroupSubjectTeacherRelation(relationId);
                Database.GroupSubjectRelations.Delete(findingRelation);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByGroupId(int? groupId)
        {
            try
            {
                ValidateModule.ValidateGroup(groupId);
                IEnumerable<GroupSubjectRelation> deleteRelations =
                    Database.GroupSubjectRelations
                        .GetMany(relation => relation.GroupId == groupId);
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
                IEnumerable<GroupSubjectRelation> deleteRelations =
                    Database.GroupSubjectRelations
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
                IEnumerable<GroupSubjectRelation> deleteRelations =
                    Database.GroupSubjectRelations
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