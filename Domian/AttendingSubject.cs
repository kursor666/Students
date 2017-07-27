namespace Domain
{
    public class AttendingSubject:StudyInfo
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public bool IsActive { get; set; }
    }
}