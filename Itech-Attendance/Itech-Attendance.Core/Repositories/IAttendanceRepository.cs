using Itech_Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public interface IAttendanceRepository
    {
        SchoolDay FindById(long id);
        List<SchoolDay> FindAll();
        void Create(SchoolDay schoolDay);
        void Update(SchoolDay schoolDay);
        void Delete(long id);
    }
}
