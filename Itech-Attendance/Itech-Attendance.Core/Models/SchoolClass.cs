using Itech_Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Models
{
    public class SchoolClass
    {
        public string Name { get; set; }
        public List<SchoolDay> SchoolDays { get; set; }
        public List<Student> Students { get; set; }
    }
}
