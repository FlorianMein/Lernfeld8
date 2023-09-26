using Itech_Attendance.Core.Repositories;
using Itech_Attendance.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Itech_Attendance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AttendanceRepository _attendanceRepository;
        private TeacherRepository _teacherRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SchoolDay()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                QrCode = GenerateQrCodeByString("https://youtube.com")
            }) ;
        }

        public IActionResult GenerateNewQR()
        {
            SchoolDay? schoolDay = 
                _attendanceRepository.FindAll().FirstOrDefault(x => x.Date == DateOnly.FromDateTime(DateTime.Now));

            if(schoolDay == null)
            {
                throw new Exception("couldnt find object in repo");
            }

            long newId = DateTime.Now.Ticks;

            schoolDay.QrCode = GenerateQrCodeByString($"https://localhost:7219/{newId}");
            _attendanceRepository.Update(schoolDay);
            return Ok();
        }

        [HttpGet]
        [Route("/{id}")]
        public IActionResult Attendance()
        {
            return View();
        }

        [HttpPost]
        [Route("/{id}")]
        public IActionResult AttendancePost(long id, string name)
        {
            SchoolDay schoolDay = _attendanceRepository.FindById(id);
            
            if (schoolDay == null)
            {
                throw new Exception("object not found");
            }

            schoolDay.AttendingStudents.Add(new Core.Models.Student { Name = name });

            _attendanceRepository.Update(schoolDay);

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPost()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string GenerateQrCodeByString(string tableRouteString)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(tableRouteString, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = BitmapToByteArray(QrBitmap);
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            return QrUri;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}