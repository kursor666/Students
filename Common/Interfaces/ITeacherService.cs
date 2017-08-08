using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface ITeacherService : IDisposable
    {
        void Add(TeacherDTO teacher);
        void Delete(int? id);
        void DeleteManyByUniversityId(int? universityId);
        void Edit(TeacherDTO teacher);
        IEnumerable<TeacherDTO> GetAll();
        TeacherDTO GetById(int? id);
        IEnumerable<TeacherDTO> GetMany(Expression<Func<TeacherDTO, bool>> predicate);
        IEnumerable<TeacherDTO> GetManyByUniversityId(int? universityId);
    }
}