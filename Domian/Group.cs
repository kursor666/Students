using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Group:BaseModel
    {
        public string GroupName { get; set; }
        public IList<Student> Students = new List<Student>();
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public IList<AttendingGroupSubject> AttendingSubjects { get; set; } = new List<AttendingGroupSubject>();
    }
}
