using System.Collections.Generic;

namespace Domain
{
    public class Faculty : BaseModel
    {
        public string FacultyName { get; set; }
        public int? DeanId { get; set; }
        public int UniversityId { get; set; }
        public IList<Group> Groups { get; set; }
    }
}
