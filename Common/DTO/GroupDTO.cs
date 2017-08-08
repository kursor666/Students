namespace Common.DTO
{
    public class GroupDTO : BaseModelDTO
    {
        public string GroupName { get; set; }
        public int FacultyId { get; set; }
        public int UniversityId { get; set; }
    }
}