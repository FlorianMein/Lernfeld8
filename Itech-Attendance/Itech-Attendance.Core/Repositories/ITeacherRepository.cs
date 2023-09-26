using Itech_Attendance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public interface ITeacherRepository
    {
        Teacher FindById(long id);
        List<Teacher> FindAll();
        void Create(Teacher teacher); 
        void Update(Teacher teacher);
        void Delete(long id);
    }
}
