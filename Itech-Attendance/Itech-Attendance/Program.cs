using Itech_Attendance.Core.Models;
using Itech_Attendance.Core.Repositories;
using Itech_Attendance.Models;
using LiteDB;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ILiteDatabase>(provider => new LiteDatabase("C:\\Database\\Itech.db"));
builder.Services.AddSingleton<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddSingleton<ITeacherRepository, TeacherRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath= "/Home/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var x = Guid.NewGuid();
var teacherRepo = app.Services.GetService<ITeacherRepository>();
var attRepo = app.Services.GetService<IAttendanceRepository>();

//attRepo.Create(new SchoolDay()
//{
//    AttendingStudents = new List<Student>()
//    {
//        new Student()
//        {
//            Name= "Selim",
//        },
//        new Student()
//        {
//            Name= "Florian",
//        }
//    },
//    Date = new DateOnly(2023, 9, 27),
//    QrCode = "hello"
//});

//attRepo.Create(new SchoolDay()
//{
//    AttendingStudents = new List<Student>()
//    {
//        new Student()
//        {
//            Name= "Gabriel",
//        },
//        new Student()
//        {
//            Name= "Andre",
//        }
//    },
//    Date = new DateOnly(2023, 9, 26),
//    QrCode = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&t"
//});

teacherRepo.Create(new Itech_Attendance.Core.Models.Teacher()
{
    FirstName = "Selim",
    LastName = "Asik",
    UserName = "selim",
    Password = "1234"
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
