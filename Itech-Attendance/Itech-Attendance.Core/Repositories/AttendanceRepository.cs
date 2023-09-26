using Itech_Attendance.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public class AttendanceRepository : IRepository<SchoolDay>
    {
        private ILiteDatabase _liteDB;

        public AttendanceRepository(ILiteDatabase liteDB)
        {
            _liteDB = liteDB;
        }
        public List<SchoolDay> FindAll()
        {
            return _liteDB.GetCollection<SchoolDay>().FindAll().ToList();
        }

        public SchoolDay FindById(long id)
        {
            return _liteDB.GetCollection<SchoolDay>().FindById(id);
        }

        public void Create(SchoolDay entity)
        {
            _liteDB.GetCollection<SchoolDay>().Insert(entity);
        }

        public void Update(SchoolDay entity)
        {
            _liteDB.GetCollection<SchoolDay>().Update(entity);
        }

        public void Delete(long id)
        {
            _liteDB.GetCollection<SchoolDay>().Delete(id);
        }


    }
}
