using System.Collections.Generic;

namespace Domain
{
    public class Student : Person
    {
        public int AdmissionYear { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
        public int GroupId { get; set; }
        public IList<StudentSubjectRelation> StudentSubjectRelations { get; set; } = new List<StudentSubjectRelation>();
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<Offset> Offsets { get; set; } = new List<Offset>();
    }
}