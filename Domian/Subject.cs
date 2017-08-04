using System.Collections.Generic;

namespace Domain
{
    public class Subject:BaseModel
    {
        public string SubjectName { get; set; }
        public int Hours { get; set; }
        public IList<TeacherSubjectRelation> TeacherSubjectRelations { get; set; } = new List<TeacherSubjectRelation>();
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<Offset> Offsets { get; set; } = new List<Offset>();
        public IList<GroupSubjectRelation> AttendingGroupSubjects { get; set; } = new List<GroupSubjectRelation>();
        public IList<StudentSubjectRelation> AttendingStudentSubjects { get; set; } = new List<StudentSubjectRelation>();
    }
}
