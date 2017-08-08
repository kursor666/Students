using System.Collections.Generic;

namespace Domain
{
    public class University:BaseModel
    {
        public string UniversityName { get; set; }
        public IList<Faculty> Faculties { get; set; }
    }
}
