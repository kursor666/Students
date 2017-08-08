using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IDeanService : IDisposable
    {
        IEnumerable<DeanDTO> GetAll { get; }
        void Add(DeanDTO dean);
        void Delete(int? id);
        void DeleteByFacutyId(int? facultyId);
        void DeleteManyByUniversityId(int? universityId);
        void Edit(DeanDTO dean);
        DeanDTO GetByFacultyId(int? facultyId);
        DeanDTO GetById(int? id);
        IEnumerable<DeanDTO> GetByUniversityId(int? universityId);
        IEnumerable<DeanDTO> GetMany(Expression<Func<DeanDTO, bool>> predicate);
    }
}