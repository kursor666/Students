using System;

namespace Domain
{
    public abstract class Mark : StudyInfo
    {
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
    }

    public class Exam : Mark
    {
        public int Value { get; set; }
    }

    public class Offset : Mark
    {
        public bool IsOffset { get; set; }
    }
}
