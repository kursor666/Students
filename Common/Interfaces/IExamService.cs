using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;

namespace Common.Interfaces
{
    public interface IExamService : IDisposable
    {
        void Add(ExamDTO exam);
        void Delete(int? id);
        void DeleteManyByStudentId(int? studentId);
        void DeleteManyBySubjectId(int? subjectId);
        void DeleteManyByTeacherId(int? teacherId);
        void DeleteManyByUniversityId(int? universityId);
        void Edit(ExamDTO exam);
        IEnumerable<ExamDTO> GetAll();
        ExamDTO GetById(int? id);
        IEnumerable<ExamDTO> GetMany(Expression<Func<ExamDTO, bool>> predicate);
        IEnumerable<ExamDTO> GetManyByStudentId(int? studentId);
        IEnumerable<ExamDTO> GetManyBySubjectId(int? subjectId);
        IEnumerable<ExamDTO> GetManyByTeacherId(int? teacherId);
        IEnumerable<ExamDTO> GetManyByUniversityId(int? universityId);
    }
}