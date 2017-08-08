using System.Collections.Generic;

namespace Domain
{
    public class Group : BaseModel
    {
        public string GroupName { get; set; }
        public int FacultyId { get; set; }
        public int UniversityId { get; set; }
        public IList<Student> Students { get; set; }
        public IList<GroupSubjectRelation> GroupSubjectTeacherRelations { get; set; }
    }
}
