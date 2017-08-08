using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IStudentService : IDisposable
    {
        void Add(StudentDTO student);
        void Delete(int? id);
        void DeleteManyByFacultyId(int? facultyId);
        void DeleteManyByGroupId(int? groupId);
        void Edit(StudentDTO student);
        IEnumerable<StudentDTO> GetAll();
        IEnumerable<StudentDTO> GetByGroupId(int? id);
        StudentDTO GetById(int? id);
        IEnumerable<StudentDTO> GetMany(Expression<Func<StudentDTO, bool>> predicate);
        IEnumerable<StudentDTO> GetManyByFacultyId(int? id);
    }
}