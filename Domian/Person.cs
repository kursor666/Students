using System.Collections.Generic;

namespace Domain
{
    public abstract class Person:BaseModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public int BornYear { get; set; }
        
        
    }
}
