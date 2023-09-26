using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public interface IRepository<T>
    {
        T FindById(long id);
        List<T> FindAll();
        void Create(T entity); 
        void Update(T entity);
        void Delete(long id);
    }
}
