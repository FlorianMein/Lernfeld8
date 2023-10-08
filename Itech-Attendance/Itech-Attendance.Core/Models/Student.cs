using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOnly TimeOfAttendance { get; set; }
        public bool isLate { get; set; }
    }
}
