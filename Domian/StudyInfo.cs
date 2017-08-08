namespace Domain
{
    public abstract class StudyInfo:BaseModel
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int UniversityId { get; set; }
    }
}