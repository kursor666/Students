using System.Collections.Generic;

namespace Domain
{
    public class Teacher:Person
    {
        public int WorkExperience { get; set; }
        public bool IsTeacher { get; set; } = true;
        public IList<TeacherSubjectRelation> TeacherSubjectRelations { get; set; } = new List<TeacherSubjectRelation>();
        public IList<GroupSubjectRelation> AttendingGroupSubjects { get; set; } = new List<GroupSubjectRelation>();
        public IList<StudentSubjectRelation> AttendingStudentSubjects { get; set; } = new List<StudentSubjectRelation>();
        public IList<Exam> Exams = new List<Exam>();
        public IList<Offset> Offsets = new List<Offset>();
    }
}
