namespace Common.DTO
{
    public class FacultyDTO : BaseModelDTO
    {
        public string FacultyName { get; set; }
        public int DeanId { get; set; }
        public int UniversityId { get; set; }
    }
}