using System;

namespace Common.DTO
{
    public abstract class MarkDTO:StudentSubjectTeacherRelationDTO
    {
        public DateTime Date { get; set; }
    }

    public class ExamDTO : MarkDTO
    {
        public int Value { get; set; }
    }

    public class OffsetDTO : MarkDTO
    {
        public bool IsOffset { get; set; }
    }
}