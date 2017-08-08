using System.Collections.Generic;

namespace Domain
{
    public class Subject:BaseModel
    {
        public string SubjectName { get; set; }
        public int Hours { get; set; }
        public IList<TeacherSubjectRelation> TeacherSubjectRelations { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Offset> Offsets { get; set; }
        public IList<GroupSubjectRelation> GroupSubjectTeacherRelations { get; set; }
        public IList<StudentSubjectRelation> StudentSubjectTeacherRelations { get; set; }
    }
}
