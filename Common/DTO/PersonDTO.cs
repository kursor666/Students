namespace Common.DTO
{
    public class PersonDTO:BaseModelDTO
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int BornYear { get; set; }
        public int UniversityId { get; set; }
    }
}