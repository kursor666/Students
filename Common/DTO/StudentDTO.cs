using System.Collections.Generic;

namespace Common.DTO
{
    public class StudentDTO:PersonDTO
    {
        public int AdmissionYear { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
        public int GroupId { get; set; }
    }
}