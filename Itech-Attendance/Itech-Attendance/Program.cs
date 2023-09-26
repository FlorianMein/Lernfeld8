using Itech_Attendance.Core.Repositories;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ILiteDatabase>(provider => new LiteDatabase("C:\\Database\\Itech.db"));
builder.Services.AddSingleton<AttendanceRepository>();
builder.Services.AddSingleton<TeacherRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var x = Guid.NewGuid();
var teacherRepo = app.Services.GetService<TeacherRepository>();
//teacherRepo.Create(new Itech_Attendance.Core.Models.Teacher()
//{
//    FirstName = "Selim",
//    LastName = "Asik",
//    UserName = "selim",
//    Password = "1234"
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
