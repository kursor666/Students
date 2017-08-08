using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IStudentSubjectTeacherRelationService : IDisposable
    {
        IEnumerable<StudentSubjectTeacherRelationDTO> GetAll { get; }

        void Add(StudentSubjectTeacherRelationDTO relation);
        void Delete(int? id);
        void DeleteManyByStudentId(int? studentId);
        void DeleteManyBySubjectId(int? subjectId);
        void DeleteManyByTeacherId(int? teacherId);
        void Edit(StudentSubjectTeacherRelationDTO relation);
        StudentSubjectTeacherRelationDTO GetById(int? id);
        IEnumerable<StudentSubjectTeacherRelationDTO> GetMany(Expression<Func<StudentSubjectTeacherRelationDTO, bool>> predicate);
        IEnumerable<StudentSubjectTeacherRelationDTO> GetManyByStudentId(int? studentId);
        IEnumerable<StudentSubjectTeacherRelationDTO> GetManyBySubjectId(int? subjectId);
        IEnumerable<StudentSubjectTeacherRelationDTO> GetManyByTeacherId(int? teacherId);
    }
}