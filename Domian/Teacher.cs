using System.Collections.Generic;

namespace Domain
{
    public class Teacher:Person
    {
        public int WorkExperience { get; set; }
        public bool IsTeacher { get; set; } = true;
        public IList<TeacherSubjectRelation> TeacherSubjectRelations { get; set; }
        public IList<GroupSubjectRelation> GroupSubjectTeacherRelations { get; set; }
        public IList<StudentSubjectRelation> StudentSubjectTeacherRelations { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Offset> Offsets { get; set; }
    }
}
