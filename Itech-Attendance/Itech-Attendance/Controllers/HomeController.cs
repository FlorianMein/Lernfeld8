using Itech_Attendance.Core.Models;
using Itech_Attendance.Core.Repositories;
using Itech_Attendance.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Claims;

namespace Itech_Attendance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ITeacherRepository _teacherRepository;

        public HomeController(ILogger<HomeController> logger, ITeacherRepository teacherRepository, IAttendanceRepository attendanceRepository)
        {
            _logger = logger;
            _teacherRepository = teacherRepository;
            _attendanceRepository = attendanceRepository;

            ViewBag.User = true;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            SetLoginViewbag();

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(ClassName className)
        {
            SchoolDay? schoolDay = _attendanceRepository.FindAll().FirstOrDefault(x => x.Date == DateOnly.FromDateTime(DateTime.Today) && x.ClassName == className);
            long newQrCodeId = DateTime.Now.Ticks;
            
            if (schoolDay == null)
            {
                _attendanceRepository.Create(new SchoolDay()
                {
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    ClassName = className,
                    AttendingStudents = new List<Student>(),
                    QrCode = GenerateQrCodeByString($"https://localhost:7219/{newQrCodeId}"),
                    QrCodeId = newQrCodeId,
                });
            }
            else
            {
                return RedirectToAction("SchoolDayQR", new { id = schoolDay.QrCodeId });
            }
            

            return RedirectToAction("SchoolDayQR", new { id = newQrCodeId });
        }

        [Authorize]
        [Route("SchoolDayQR/{id}")]
        public IActionResult SchoolDayQR(long id)
        {
            SetLoginViewbag();
            SchoolDay? schoolDay = _attendanceRepository.FindAll().FirstOrDefault(x => x.QrCodeId == id);

            if(schoolDay == null)
            {
                throw new Exception("Fehler");
            }

            return View(schoolDay);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            List<Teacher> teachers = _teacherRepository.FindAll();

            if (teachers.Any(x => x.UserName == username && x.Password == password))
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                claims.Add(new Claim("AdminID", $"{username}"));
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                ViewBag.isLogged = true;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            SetLoginViewbag();

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            SetLoginViewbag();

            return View("Login");
        }

        [HttpPost]
        public IActionResult Registration(string name)
        {
            return View();
        }

        public IActionResult GenerateNewQR(ClassName className)
        {
            SchoolDay? schoolDay =
                _attendanceRepository.FindAll().FirstOrDefault(x => x.Date == DateOnly.FromDateTime(DateTime.Now) && x.ClassName == className);

            if (schoolDay == null)
            {
                throw new Exception("Fehler");
            }

            long newId = DateTime.Now.Ticks;
            schoolDay.QrCode = GenerateQrCodeByString($"https://localhost:7219/{newId}");
            schoolDay.QrCodeId = newId;

            _attendanceRepository.Update(schoolDay);

            return RedirectToAction("SchoolDayQR", new { id = newId });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Attendance(long id)
        {
            SchoolDay? schoolDay = _attendanceRepository.FindAll().FirstOrDefault(x => x.QrCodeId == id);

            return View(schoolDay);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult AttendancePost(long id, string name)
        {
            SchoolDay? schoolDay = _attendanceRepository.FindAll().FirstOrDefault(x => x.QrCodeId == id);

            if (schoolDay == null)
            {
                throw new Exception("object not found");
            }

            schoolDay.AttendingStudents.Add(new Student { Name = name });

            _attendanceRepository.Update(schoolDay);

            return RedirectToAction("AttendanceSuccessful");
        }

        [HttpGet]
        public IActionResult AttendanceSuccessful()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Table()
        {
            SetLoginViewbag();

            return View(_attendanceRepository.FindAll());
        }

        [HttpGet]
        public IActionResult Login()
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

        protected void SetLoginViewbag()
        {
            if (HttpContext.User.Claims.Any()) // eingeloggt
            {
                ViewBag.IsLoggedIn = true;
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }
        }
    }
}