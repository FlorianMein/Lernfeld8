using Itech_Attendance.Controllers;
using System.Security.Principal;
using System.Drawing;
using Itech_Attendance.Helpers;

namespace Itech_Attendance.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BitmapToByteArray_WhenCorrectly_ThenPass()
        {
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); ;
            var x = HomeController.BitmapToByteArray(bitmap);

            if(x is byte[] && x is not null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ToTimeOnly_WhenCorrectly_ThenPass()
        {
            string time = "04:10";
            TimeOnly timeOnly = new TimeOnly(4, 10);
            
            if(time.ToTimeOnly() is TimeOnly && time.ToTimeOnly() == timeOnly)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ToTimeOnly_WhenUncorrectly_ThenPass()
        {
            string time = "04:11";
            TimeOnly timeOnly = new TimeOnly(4, 10);

            if (time.ToTimeOnly() != timeOnly)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}