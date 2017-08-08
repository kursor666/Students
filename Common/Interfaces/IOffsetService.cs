using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IOffsetService : IDisposable
    {
        IEnumerable<OffsetDTO> GetAll { get; }

        void Add(OffsetDTO offset);
        void Delete(int? id);
        void DeleteManyByStudentId(int? studentId);
        void DeleteManyBySubjectId(int? subjectId);
        void DeleteManyByTeacherId(int? teacherId);
        void Edit(OffsetDTO offset);
        OffsetDTO GetById(int? id);
        IEnumerable<OffsetDTO> GetMany(Expression<Func<OffsetDTO, bool>> predicate);
        IEnumerable<OffsetDTO> GetManyByStudentId(int? studentId);
        IEnumerable<OffsetDTO> GetManyBySubjectId(int? subjectId);
        IEnumerable<OffsetDTO> GetManyByTeacherId(int? teacherId);
    }
}