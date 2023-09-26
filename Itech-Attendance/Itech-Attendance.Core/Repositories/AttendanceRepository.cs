using Itech_Attendance.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itech_Attendance.Core.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
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

        public void Create(SchoolDay schoolDay)
        {
            _liteDB.GetCollection<SchoolDay>().Insert(schoolDay);
        }

        public void Update(SchoolDay schoolDay)
        {
            _liteDB.GetCollection<SchoolDay>().Update(schoolDay);
        }

        public void Delete(long id)
        {
            _liteDB.GetCollection<SchoolDay>().Delete(id);
        }


    }
}
