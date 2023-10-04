using Itech_Attendance.Core.Models;

namespace Itech_Attendance.Models
{
    public class SchoolDay
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string QrCode { get; set; }
        public long QrCodeId { get; set; }
        public List<Student> AttendingStudents { get; set; } = new List<Student>();
    }
}
