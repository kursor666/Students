using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IUniversityService : IDisposable
    {
        IEnumerable<UniversityDTO> GetAll { get; }

        void Add(UniversityDTO university);
        void Delete(int? id);
        void Edit(UniversityDTO university);
        UniversityDTO GetById(int? id);
        IEnumerable<UniversityDTO> GetMany(Expression<Func<UniversityDTO, bool>> predicate);
    }
}