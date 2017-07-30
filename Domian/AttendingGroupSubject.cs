namespace Domain
{
    public class AttendingGroupSubject:ActvitySubject
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}