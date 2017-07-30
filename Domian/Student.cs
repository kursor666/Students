using System.Collections.Generic;

namespace Domain
{
    public class Student : Person
    {
        public int AdmissionYear { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public IList<AttendingStudentSubject> AttendingStudentSubjects { get; set; } = new List<AttendingStudentSubject>();
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<Offset> Offsets { get; set; } = new List<Offset>();
    }
}
