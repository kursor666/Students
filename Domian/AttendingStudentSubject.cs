namespace Domain
{
    public class AttendingStudentSubject:ActvitySubject
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}