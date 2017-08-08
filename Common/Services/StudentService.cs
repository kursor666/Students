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
    public class StudentService : BaseService
    {

        public StudentService(IUnitOfWork database)
        {
            Database = database;
            ValidateModule = new ValidateModule(Database);
        }

        #region Add

        public void Add(StudentDTO student)
        {
            if (student == null)
                throw new ValidationException("Студент не найден", "");
            Student addingStudent = AutoMap<StudentDTO, Student>.Map(student);
            Database.Students.Add(addingStudent);
            Database.Commit();
        }

        #endregion

        #region Get

        public StudentDTO GetById(int? id)
        {
            try
            {
                var findingStudent = ValidateModule.ValidateStudent(id);
                return AutoMap<Student, StudentDTO>.Map(findingStudent);
            }
            catch (ValidationException)
            {
                throw;
            }

        }

        public IEnumerable<StudentDTO> GetAll() =>
            AutoMap<IEnumerable<Student>, IEnumerable<StudentDTO>>
            .Map(Database
                .Students
                .GetAll());

        public IEnumerable<StudentDTO> GetMany(Expression<Func<StudentDTO, bool>> predicate)
        {
            var studentPredicate = AutoMap<Expression<Func<StudentDTO, bool>>,
                    Expression<Func<Student, bool>>>
                .Map(predicate);
            IEnumerable<StudentDTO> findingStudents =
                AutoMap<IQueryable<Student>, List<StudentDTO>>
                    .Map(Database
                        .Students
                        .GetMany(studentPredicate));
            return findingStudents;
        }

        public IEnumerable<StudentDTO> GetByGroupId(int? id)
        {
            Group findingGroup;
            try
            {
                findingGroup = ValidateModule.ValidateGroup(id);
            }
            catch (ValidationException)
            {
                throw;
            }
            return GetMany(student => student.GroupId == findingGroup.Id);
        }

        public IEnumerable<StudentDTO> GetManyByFacultyId(int? id)
        {
            try
            {
                ValidateModule.ValidateFaculty(id);
            }
            catch (ValidationException)
            {
                throw;
            }
            return GetMany(student => student.FacultyId == id);
        }

        #endregion

        #region Edit

        public void Edit(StudentDTO student)
        {
            try
            {
                if (student == null)
                    throw new ValidationException("Студент не найден.","");
                var findingStudent = ValidateModule.ValidateStudent(student.Id);
                findingStudent = AutoMap<StudentDTO, Student>.Map(student, findingStudent);
                Database.Students.Edit(findingStudent);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion

        #region Delete

        //Пока не придумал как иначе, буду использовать методы сервисов реляций
        public void Delete(int? id)
        {
            try
            {
                Student findingStudent = ValidateModule.ValidateStudent(id);
                new StudentSubjectTeacherRelationService(Database).DeleteManyByStudentId(findingStudent.Id);
                new ExamService(Database).DeleteManyByStudentId(findingStudent.Id);
                new OffsetService(Database).DeleteManyByStudentId(findingStudent.Id);
                Database.Students.Delete(findingStudent);
                Database.Commit();
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByGroupId(int? groupId)
        {
            try
            {
                ValidateModule.ValidateGroup(groupId);
                var deleteStudents = Database
                    .Students
                    .GetMany(student => student.GroupId == groupId);
                foreach (var student in deleteStudents)
                    Delete(student.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void DeleteManyByFacultyId(int? facultyId)
        {
            try
            {
                ValidateModule.ValidateFaculty(facultyId);
                var deleteStudents = Database
                    .Students
                    .GetMany(student => student.FacultyId == facultyId);
                foreach (var student in deleteStudents)
                    Delete(student.Id);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        #endregion



    }
}
