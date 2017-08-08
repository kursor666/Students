using System;
using Domain;
using Repository.Interfaces;

namespace Common.Infrastructure
{
    public class ValidateModule
    {
        private readonly IUnitOfWork _database;
        public ValidateModule(IUnitOfWork database)
        {
            _database = database;
        }

        #region Relations

        #region StudentSubjectTeacher

        public StudentSubjectRelation ValidateStudentSubjectTeacherRelation(int? relationId)
        {
            if (relationId == null || relationId == 0)
                throw new ValidationException("Задан неверный Id связи.", "");
            StudentSubjectRelation findingRelation =
                _database.StudentSubjectRelations
                .GetById((int)relationId);
            try
            {
                return ValidateStudentSubjectTeacherRelation(findingRelation);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public StudentSubjectRelation ValidateStudentSubjectTeacherRelation(StudentSubjectRelation relation) =>
            relation ?? throw new ValidationException("Связь не найдена", "");

        #endregion

        #region GroupSubjectTeacher

        public GroupSubjectRelation ValidateGroupSubjectTeacherRelation(int? relationId)
        {
            if (relationId == null || relationId == 0)
                throw new ValidationException("Задан неверный Id связи.", "");
            GroupSubjectRelation findingRelation =
                _database.GroupSubjectRelations
                    .GetById((int)relationId);
            try
            {
                return ValidateGroupSubjectTeacherRelation(findingRelation);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public GroupSubjectRelation ValidateGroupSubjectTeacherRelation(GroupSubjectRelation relation) =>
            relation ?? throw new ValidationException("Связь не найдена", "");

        #endregion

        #region TeacherSubject

        public TeacherSubjectRelation ValidateTeacherSubjectRelation(int? relationId)
        {
            if (relationId == null || relationId == 0)
                throw new ValidationException("Задан неверный Id связи.", "");
            TeacherSubjectRelation findingRelation =
                _database.TeacherSubjectRelations
                    .GetById((int)relationId);
            try
            {
                return ValidateTeacherSubjectRelation(findingRelation);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public TeacherSubjectRelation ValidateTeacherSubjectRelation(TeacherSubjectRelation relation) =>
            relation ?? throw new ValidationException("Связь не найдена", "");

        #endregion

        #endregion

        #region Models

        #region Exam

        public Exam ValidateExam(int? examId)
        {
            if (examId == null || examId == 0)
                throw new ValidationException("Задан неверный Id оценки.", "");
            Exam findingExam = _database.Exams.GetById((int)examId);
            try
            {
                return ValidateExam(findingExam);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Exam ValidateExam(Exam exam) =>
            exam ?? throw new ValidationException("Оценка по экзамену не найдена", "");

        #endregion

        #region Offset

        public Offset ValidateOffset(int? offsetId)
        {
            if (offsetId == null || offsetId == 0)
                throw new ValidationException("Задан неверный Id зачета.", "");
            Offset findingOffset = _database.Offsets.GetById((int)offsetId);
            try
            {
                return ValidateOffset(findingOffset);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Offset ValidateOffset(Offset offset) =>
            offset ?? throw new ValidationException("Зачет не найден.", "");

        #endregion

        #region Student

        public Student ValidateStudent(int? studentId)
        {
            if (studentId == null || studentId == 0)
                throw new ValidationException("Задан неверный Id студента.", "");
            Student findingStudent =
                _database.Students.GetById((int)studentId);
            try
            {
                return ValidateStudent(findingStudent);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Student ValidateStudent(Student student) =>
            student ?? throw new ValidationException("Студент не найден", "");

        #endregion

        #region Subject

        public Subject ValidateSubject(int? subjectId)
        {
            if (subjectId == null || subjectId == 0)
                throw new ValidationException("Задан неверный Id предмета.", "");
            Subject findingSubject = _database.Subjects.GetById((int)subjectId);
            try
            {
                return ValidateSubject(findingSubject);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Subject ValidateSubject(Subject subject) =>
            subject ?? throw new ValidationException("Предмет не найден.", "");

        #endregion

        #region Teacher

        public Teacher ValidateTeacher(int? teacherId)
        {
            if (teacherId == null || teacherId == 0)
                throw new ValidationException("Задан неверный Id преподавателя.", "");
            Teacher findingTeacher = _database.Teachers.GetById((int)teacherId);
            try
            {
                return ValidateTeacher(findingTeacher);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Teacher ValidateTeacher(Teacher teacher) =>
            teacher ?? throw new ValidationException("Преподаватель не найден.", "");

        #endregion

        #region Group

        public Group ValidateGroup(int? groupId)
        {
            if (groupId == null || groupId == 0)
                throw new ValidationException("Задан неверный Id группы.", "");
            Group findingGroup = _database.Groups.GetById((int)groupId);
            try
            {
                return ValidateGroup(findingGroup);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Group ValidateGroup(Group group) =>
            group ?? throw new ValidationException("Группа не найдена.", "");

        #endregion

        #region Faculty

        public Faculty ValidateFaculty(int? facultyId)
        {
            if (facultyId == null || facultyId == 0)
                throw new ValidationException("Задан неверный Id факультета.", "");
            Faculty findingFaculty = _database.Faculties.GetById((int)facultyId);
            try
            {
                return ValidateFaculty(findingFaculty);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Faculty ValidateFaculty(Faculty faculty) =>
            faculty ?? throw new ValidationException("Факультет не найден.", "");

        #endregion

        #region Dean

        public Dean ValidateDean(int? deanId)
        {
            if (deanId == null || deanId == 0)
                throw new ValidationException("Задан неверный Id декана.", "");
            Dean findingDean = _database.Deans.GetById((int)deanId);
            try
            {
                return ValidateDean(findingDean);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public Dean ValidateDean(Dean dean) =>
            dean ?? throw new ValidationException("Декан не найден.", "");

        #endregion

        #region University

        public University ValidateUniversity(int? universityId)
        {
            if (universityId == null || universityId == 0)
                throw new ValidationException("Задан неверный Id университета.", "");
            University findingUniversity = _database.Universities.GetById((int)universityId);
            try
            {
                return ValidateUniversity(findingUniversity);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public University ValidateUniversity(University university) =>
            university ?? throw new ValidationException("Универ не найден.","")

        #endregion

        #endregion
    }
}