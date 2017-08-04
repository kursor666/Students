using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Group:BaseModel
    {
        public string GroupName { get; set; }
        public int FacultyId { get; set; }
        public IList<Student> Students = new List<Student>();
        public IList<GroupSubjectRelation> GroupSubjectRelations { get; set; } = new List<GroupSubjectRelation>();
    }
}
