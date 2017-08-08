using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Common.Interfaces;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class UniversityService : BaseService, IUniversityService
    {
        public UniversityService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(UniversityDTO university)
        {
            if (university == null)
                throw new ValidationException("Университет не найден.", "");
            var addingUniversity = AutoMap<UniversityDTO, University>.Map(university);
            Database.Universities.Add(addingUniversity);
            Database.Commit();
        }

        #endregion

        #region Get

        public UniversityDTO GetById(int? id)
        {
            try
            {
                var findingUniversity = ValidateModule.ValidateUniversity(id);
                return AutoMap<University, UniversityDTO>.Map(findingUniversity);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<UniversityDTO> GetAll =>
            AutoMap<IEnumerable<University>,
                    IEnumerable<UniversityDTO>>
                .Map(Database
                    .Universities
                    .GetAll());

        public IEnumerable<UniversityDTO> GetMany(Expression<Func<UniversityDTO, bool>> predicate)
        {
            var universityPredicate = AutoMap<Expression<Func<UniversityDTO, bool>>,
                    Expression<Func<University, bool>>>
                .Map(predicate);
            var findingUniversities = Database
                .Universities
                .GetMany(universityPredicate);
            return AutoMap<IEnumerable<University>,
                    IEnumerable<UniversityDTO>>
                .Map(findingUniversities);
        }

        #endregion

        #region Edit

        public void Edit(UniversityDTO university)
        {
            try
            {
                if (university == null)
                    throw new ValidationException("Университет не найден.", "");
                var findingUniversity = ValidateModule.ValidateUniversity(university.Id);
                findingUniversity = AutoMap<UniversityDTO,
                        University>
                    .Map(university, findingUniversity);
                Database.Universities.Edit(findingUniversity);
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
                var deleteUniversity = ValidateModule.ValidateUniversity(id);
                new FacultyService(Database).DeleteManyByUniversityId(id);
                new TeacherService(Database).DeleteManyByUniversityId(id);
                Database.Universities.Delete(deleteUniversity);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

    }
}