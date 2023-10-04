using Itech_Attendance.Core.Models;
using Itech_Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public interface ISchoolClassRepository
    {
        SchoolClass FindById(long id);
        List<SchoolClass> FindAll();
        void Create(SchoolClass schoolDay);
        void Update(SchoolClass schoolDay);
        void Delete(long id);
    }
}
