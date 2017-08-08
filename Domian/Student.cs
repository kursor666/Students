using System.Collections.Generic;

namespace Domain
{
    public class Student : Person
    {
        public int AdmissionYear { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
        public int GroupId { get; set; }
        public IList<StudentSubjectRelation> StudentSubjectTeacherRelations { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Offset> Offsets { get; set; }
    }
}