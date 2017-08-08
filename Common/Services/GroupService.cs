using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class GroupService:BaseService
    {
        public GroupService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(GroupDTO group)
        {
            if (group==null)
                throw new ValidationException("Группа не найдена.","");
            var addingGroup = AutoMap<GroupDTO, Group>.Map(group);
            Database.Groups.Add(addingGroup);
            Database.Commit();
        }

        #endregion

        #region Get

        public GroupDTO GetById(int? id)
        {
            try
            {
                var findingGroup = ValidateModule.ValidateGroup(id);
                return AutoMap<Group, GroupDTO>.Map(findingGroup);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<GroupDTO> GetAll =>
            AutoMap<IEnumerable<Group>,
                    IEnumerable<GroupDTO>>
                .Map(Database
                    .Groups
                    .GetAll());

        public IEnumerable<GroupDTO> GetMany(Expression<Func<GroupDTO, bool>> predicate)
        {
            var groupPredicate = AutoMap<Expression<Func<GroupDTO, bool>>, 
                Expression<Func<Group, bool>>>
                .Map(predicate);
            IEnumerable<Group> findingGroups = Database
                .Groups
                .GetMany(groupPredicate);
            return AutoMap<IEnumerable<Group>,
                IEnumerable<GroupDTO>>
                .Map(findingGroups);
        }

        public IEnumerable<GroupDTO> GetManyByFacultyId(int? facultyId)
        {
            try
            {
                ValidateModule.ValidateFaculty(facultyId);
                return GetMany(group => group.FacultyId == facultyId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(GroupDTO group)
        {
            try
            {
                if (group==null)
                    throw new ValidationException("Группа не найдена.","");
                var findingGroup = ValidateModule.ValidateGroup(group.Id);
                findingGroup = AutoMap<GroupDTO, Group>.Map(group, findingGroup);
                Database.Groups.Edit(findingGroup);
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
                var deleteGroup = ValidateModule.ValidateGroup(id);
                new StudentService(Database).DeleteManyByGroupId(id);
                new GroupSubjectTeacherRelationService(Database).DeleteManyByGroupId(id);
                Database.Groups.Delete(deleteGroup);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByFacultyId(int? facultyId)
        {
            try
            {
                ValidateModule.ValidateFaculty(facultyId);
                var deleteGroups = GetManyByFacultyId(facultyId);
                foreach (var group in deleteGroups)
                {
                    Delete(group.Id);
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