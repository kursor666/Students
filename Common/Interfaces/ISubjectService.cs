using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface ISubjectService : IDisposable
    {
        IEnumerable<SubjectDTO> GetAll { get; }

        void Add(SubjectDTO subject);
        void Delete(int? id);
        void Edit(SubjectDTO subject);
        SubjectDTO GetById(int? id);
        IEnumerable<SubjectDTO> GetMany(Expression<Func<SubjectDTO, bool>> predicate);
    }
}