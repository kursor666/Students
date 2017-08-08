using Domain;

namespace Common.DTO
{
    public class DeanDTO:TeacherDTO
    {
        public int FacultyId { get; set; }
        public bool IsTeacher { get; set; }
    }
}