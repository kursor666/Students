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
    public class FacultyService : BaseService, IFacultyService
    {
        public FacultyService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(FacultyDTO faculty)
        {
            if (faculty == null)
                throw new ValidationException("Факультет не найден.", "");
            var addingFaculty = AutoMap<FacultyDTO, Faculty>.Map(faculty);
            Database.Faculties.Add(addingFaculty);
            Database.Commit();
        }

        #endregion

        #region Get

        public FacultyDTO GetById(int? id)
        {
            try
            {
                var findingFaculty = ValidateModule.ValidateFaculty(id);
                return AutoMap<Faculty, FacultyDTO>.Map(findingFaculty);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<FacultyDTO> GetAll =>
            AutoMap<IEnumerable<Faculty>,
                IEnumerable<FacultyDTO>>
            .Map(Database
                .Faculties
                .GetAll());

        public IEnumerable<FacultyDTO> GetMany(Expression<Func<FacultyDTO, bool>> predicate)
        {
            var facultyPredicate = AutoMap<Expression<Func<FacultyDTO, bool>>,
                Expression<Func<Faculty, bool>>>
                .Map(predicate);
            IEnumerable<Faculty> findingFaculty = Database
                .Faculties
                .GetMany(facultyPredicate);
            return AutoMap<IEnumerable<Faculty>,
                IEnumerable<FacultyDTO>>
                .Map(findingFaculty);
        }

        public IEnumerable<FacultyDTO> GetManyByUniversityId(int? universityId)
        {
            try
            {
                ValidateModule.ValidateUniversity(universityId);
                return GetMany(faculty => faculty.UniversityId == universityId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public FacultyDTO GetByDeanId(int? deanId)
        {
            try
            {
                ValidateModule.ValidateDean(deanId);
                var findingFaculty = Database.Faculties.GetMany(faculty => faculty.DeanId == deanId).FirstOrDefault();
                return AutoMap<Faculty, FacultyDTO>.Map(findingFaculty);
            }
            catch (ValidationException)
            {
                throw;
            }
        }


        #endregion

        #region Edit

        public void Edit(FacultyDTO faculty)
        {
            try
            {
                if (faculty==null)
                    throw new ValidationException("Факультет не найден.","");
                var findingFaculty = ValidateModule.ValidateFaculty(faculty.Id);
                findingFaculty = AutoMap<FacultyDTO, Faculty>.Map(faculty, findingFaculty);
                Database.Faculties.Edit(findingFaculty);
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
                var deleteFaculty = ValidateModule.ValidateFaculty(id);
                new StudentService(Database).DeleteManyByFacultyId(id);
                new DeanService(Database).DeleteByFacutyId(id);
                new GroupService(Database).DeleteManyByFacultyId(id);
                Database.Faculties.Delete(deleteFaculty);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByUniversityId(int? universityId)
        {
            try
            {
                ValidateModule.ValidateUniversity(universityId);
                foreach (var faculty in GetManyByUniversityId(universityId))
                {
                    Delete(faculty.Id);
                }
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion
    }
}