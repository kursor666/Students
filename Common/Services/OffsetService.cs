using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Common.Interfaces;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class OffsetService : BaseService, IOffsetService
    {
        public OffsetService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(OffsetDTO offset)
        {
            if (offset == null)
                throw new ValidationException("Оценка не найдена", "");
            Offset addingOffset = AutoMap<OffsetDTO, Offset>.Map(offset);
            Database.Offsets.Add(addingOffset);
            Database.Commit();
        }

        #endregion

        #region Get

        public OffsetDTO GetById(int? id)
        {
            try
            {
                Offset findingOffset = ValidateModule.ValidateOffset(id);
                return AutoMap<Offset, OffsetDTO>.Map(findingOffset);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<OffsetDTO> GetAll =>
            AutoMap<IEnumerable<Offset>, IEnumerable<OffsetDTO>>
                .Map(Database
                    .Offsets
                    .GetAll());

        public IEnumerable<OffsetDTO> GetMany(Expression<Func<OffsetDTO, bool>> predicate)
        {
            var offsetPredicate = AutoMap<Expression<Func<OffsetDTO, bool>>,
                    Expression<Func<Offset, bool>>>
                .Map(predicate);
            IEnumerable<OffsetDTO> findingOfssets =
                AutoMap<IQueryable<Offset>, List<OffsetDTO>>
                    .Map(Database
                        .Offsets
                        .GetMany(offsetPredicate));
            return findingOfssets;
        }

        public IEnumerable<OffsetDTO> GetManyByStudentId(int? studentId)
        {
            try
            {
                ValidateModule.ValidateStudent(studentId);
                return GetMany(offset => offset.StudentId == studentId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<OffsetDTO> GetManyByTeacherId(int? teacherId)
        {
            try
            {
                ValidateModule.ValidateTeacher(teacherId);
                return GetMany(offset => offset.TeacherId == teacherId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<OffsetDTO> GetManyBySubjectId(int? subjectId)
        {
            try
            {
                ValidateModule.ValidateSubject(subjectId);
                return GetMany(offset => offset.SubjectId == subjectId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(OffsetDTO offset)
        {
            try
            {
                if (offset==null)
                    throw new ValidationException("Зачет не найден.","");
                var findingOffset = ValidateModule.ValidateOffset(offset.Id);
                findingOffset = AutoMap<OffsetDTO, Offset>.Map(offset, findingOffset);
                Database.Offsets.Edit(findingOffset);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Delete

        public void Delete(int? id)
        {
            try
            {
                Offset findingOffset = ValidateModule.ValidateOffset(id);
                Database.Offsets.Delete(findingOffset);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByStudentId(int? studentId)
        {
            try
            {
                ValidateModule.ValidateStudent(studentId);
                IEnumerable<Offset> deleteOffsets =
                    Database.Offsets
                        .GetMany(offset => offset.StudentId == studentId);
                foreach (var offset in deleteOffsets)
                    Delete(offset.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByTeacherId(int? teacherId)
        {
            try
            {
                ValidateModule.ValidateTeacher(teacherId);
                IEnumerable<Offset> deleteOffsets =
                    Database.Offsets
                        .GetMany(offset => offset.TeacherId == teacherId);
                foreach (var offset in deleteOffsets)
                    Delete(offset.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyBySubjectId(int? subjectId)
        {
            try
            {
                ValidateModule.ValidateSubject(subjectId);
                IEnumerable<Offset> deleteOffsets =
                    Database.Offsets
                        .GetMany(offset => offset.SubjectId == subjectId);
                foreach (var offset in deleteOffsets)
                    Delete(offset.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion
    }
}