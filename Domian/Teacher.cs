using System.Collections.Generic;

namespace Domain
{
    public class Teacher:Person
    {
        public int WorkExperience { get; set; }
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<AttendingGroupSubject> AttendingGroupSubjects { get; set; } = new List<AttendingGroupSubject>();
        public IList<AttendingStudentSubject> AttendingStudentSubjects { get; set; } = new List<AttendingStudentSubject>();
    }
}
