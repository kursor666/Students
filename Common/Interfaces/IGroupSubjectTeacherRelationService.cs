using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IGroupSubjectTeacherRelationService : IDisposable
    {
        IEnumerable<GroupSubjectTeacherRelationDTO> GetAll { get; }

        void Add(GroupSubjectTeacherRelationDTO relation);
        void Delete(int? relationId);
        void DeleteManyByGroupId(int? groupId);
        void DeleteManyBySubjectId(int? subjectId);
        void DeleteManyByTeacherId(int? teacherId);
        void Edit(GroupSubjectTeacherRelationDTO relation);
        GroupSubjectTeacherRelationDTO GetById(int? id);
        IEnumerable<GroupSubjectTeacherRelationDTO> GetMany(Expression<Func<GroupSubjectTeacherRelationDTO, bool>> predicate);
        IEnumerable<GroupSubjectTeacherRelationDTO> GetManyByGroupId(int? groupId);
        IEnumerable<GroupSubjectTeacherRelationDTO> GetManyBySubjectId(int? subjectId);
        IEnumerable<GroupSubjectTeacherRelationDTO> GetManyByTeacherId(int? teacherId);
    }
}