using Itech_Attendance.Core.Models;
using Itech_Attendance.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private ILiteDatabase _liteDB;

        public SchoolClassRepository(ILiteDatabase liteDB)
        {
            _liteDB = liteDB;
        }

        public void Create(SchoolClass schoolDay)
        {
             _liteDB.GetCollection<SchoolClass>().FindAll().ToList();
        }

        public void Delete(long id)
        {
             _liteDB.GetCollection<SchoolClass>().Delete(id);
        }

        public List<SchoolClass> FindAll()
        {
            return _liteDB.GetCollection<SchoolClass>().FindAll().ToList();
        }

        public SchoolClass FindById(long id)
        {
            return _liteDB.GetCollection<SchoolClass>().FindById(id);
        }

        public void Update(SchoolClass schoolDay)
        {
            _liteDB.GetCollection<SchoolClass>().Update(schoolDay);
        }
    }
}
