namespace Common.DTO
{
    public class TeacherSubjectRelationDTO:BaseModelDTO
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int UniversityId { get; set; }
    }
}