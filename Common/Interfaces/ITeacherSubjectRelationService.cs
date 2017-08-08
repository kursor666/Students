using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface ITeacherSubjectRelationService : IDisposable
    {
        IEnumerable<TeacherSubjectRelationDTO> GetAll { get; }

        void Add(TeacherSubjectRelationDTO relation);
        void Delete(int? id);
        void DeleteManyBySubjectId(int? subjectId);
        void DeleteManyByTeacherId(int? teacherId);
        void Edit(TeacherSubjectRelationDTO relation);
        TeacherSubjectRelationDTO GetById(int? id);
        IEnumerable<TeacherSubjectRelationDTO> GetMany(Expression<Func<TeacherSubjectRelationDTO, bool>> predicate);
        IEnumerable<TeacherSubjectRelationDTO> GetManyBySubjectId(int? subjectId);
        IEnumerable<TeacherSubjectRelationDTO> GetManyByTeacherId(int? teacherId);
    }
}