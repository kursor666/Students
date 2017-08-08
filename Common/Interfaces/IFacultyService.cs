using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IFacultyService : IDisposable
    {
        IEnumerable<FacultyDTO> GetAll { get; }

        void Add(FacultyDTO faculty);
        void Delete(int? id);
        void DeleteManyByUniversityId(int? universityId);
        void Edit(FacultyDTO faculty);
        FacultyDTO GetByDeanId(int? deanId);
        FacultyDTO GetById(int? id);
        IEnumerable<FacultyDTO> GetMany(Expression<Func<FacultyDTO, bool>> predicate);
        IEnumerable<FacultyDTO> GetManyByUniversityId(int? universityId);
    }
}