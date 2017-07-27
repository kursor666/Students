using System.Collections.Generic;

namespace Domain
{
    public class Subject:BaseModel
    {
        public string SubjectName { get; set; }
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<Offset> Offsets { get; set; } = new List<Offset>();
    }
}
