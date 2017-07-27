using System.Collections.Generic;

namespace Domain
{
    public class Teacher:Person
    {
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<AttendingSubject> AttendingSubjects { get; set; } = new List<AttendingSubject>();
    }
}
