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
    public class TeacherRepository : ITeacherRepository
    {
        private ILiteDatabase _liteDB;

        public TeacherRepository(ILiteDatabase liteDB)
        {
            _liteDB = liteDB;
        }

        public void Create(Teacher entity)
        {
            _liteDB.GetCollection<Teacher>().Insert(entity);
        }

        public void Delete(long id)
        {
            _liteDB.GetCollection<Teacher>().Delete(id);
        }

        public List<Teacher> FindAll()
        {
            return _liteDB.GetCollection<Teacher>().FindAll().ToList();
        }

        public Teacher FindById(long id)
        {
            return _liteDB.GetCollection<Teacher>().FindById(id);
        }

        public void Update(Teacher entity)
        {
            _liteDB.GetCollection<Teacher>().Update(entity);
        }
    }
}
