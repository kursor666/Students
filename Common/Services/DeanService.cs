using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DTO;
using Common.Infrastructure;
using Domain;
using Repository.Interfaces;

namespace Common.Services
{
    public class DeanService : BaseService
    {
        public DeanService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(DeanDTO dean)
        {
            if (dean == null)
                throw new ValidationException("Декан не найден.", "");
            Dean addingDean = AutoMap<DeanDTO, Dean>.Map(dean);
            Database.Deans.Add(addingDean);
            Database.Commit();
        }

        #endregion

        #region Get

        public DeanDTO GetById(int? id)
        {
            try
            {
                var findingDean = ValidateModule.ValidateDean(id);
                return AutoMap<Dean, DeanDTO>.Map(findingDean);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public IEnumerable<DeanDTO> GetAll =>
            AutoMap<IEnumerable<Dean>, IEnumerable<DeanDTO>>
                .Map(Database
                    .Deans
                    .GetAll());

        public IEnumerable<DeanDTO> GetMany(Expression<Func<DeanDTO, bool>> predicate)
        {
            var deanPredicate = AutoMap<Expression<Func<DeanDTO, bool>>,
                Expression<Func<Dean, bool>>>
                .Map(predicate);
            IEnumerable<Dean> findingDeans = Database
                .Deans
                .GetMany(deanPredicate);
            return AutoMap<IEnumerable<Dean>, IEnumerable<DeanDTO>>
                .Map(findingDeans);
        }

        public IEnumerable<DeanDTO> GetByUniversityId(int? universityId)
        {
            try
            {
                ValidateModule.ValidateUniversity(universityId);
                return GetMany(dean => dean.UniversityId == universityId);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public DeanDTO GetByFacultyId(int? facultyId)
        {
            try
            {
                ValidateModule.ValidateFaculty(facultyId);
                return GetMany(dean => dean.FacultyId == facultyId).FirstOrDefault();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Edit

        public void Edit(DeanDTO dean)
        {
            try
            {
                if (dean==null)
                    throw new ValidationException("Декан не найден.","");
                var findingDean = ValidateModule.ValidateDean(dean.Id);
                findingDean = AutoMap<DeanDTO, Dean>.Map(dean, findingDean);
                Database.Deans.Edit(findingDean);
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
                Dean deleteDean = ValidateModule.ValidateDean(id);
                var faculty = Database.Faculties.GetById(deleteDean.FacultyId);
                faculty.DeanId = null;
                Database.Faculties.Edit(faculty);
                if (deleteDean.IsTeacher)
                    new TeacherService(Database).Delete(id);
                else
                {
                    Database.Deans.Delete(deleteDean);
                    Database.Commit();
                }
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteByFacutyId(int? facultyId)
        {
            try
            {
                var findingFaculty = ValidateModule.ValidateFaculty(facultyId);
                Delete(findingFaculty.DeanId);
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
                foreach (var dean in GetByUniversityId(universityId))
                {
                    Delete(dean.Id);
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