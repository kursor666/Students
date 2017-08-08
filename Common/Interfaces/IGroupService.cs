using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IGroupService : IDisposable
    {
        IEnumerable<GroupDTO> GetAll { get; }

        void Add(GroupDTO group);
        void Delete(int? id);
        void DeleteManyByFacultyId(int? facultyId);
        void Edit(GroupDTO group);
        GroupDTO GetById(int? id);
        IEnumerable<GroupDTO> GetMany(Expression<Func<GroupDTO, bool>> predicate);
        IEnumerable<GroupDTO> GetManyByFacultyId(int? facultyId);
    }
}