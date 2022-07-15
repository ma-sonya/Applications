using Microsoft.AspNetCore.Mvc;

namespace DormWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : Controller
    {
        private readonly DBDormContext _context;

        public ChartController(DBDormContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var facs = _context.Faculties.ToList();
            List<object> facStudents = new List<object>();
            facStudents.Add(new[] { "Факультет", "Кількість студентів на факультеті" });
            foreach (var f in facs)
            {
                var students = _context.Inhabitants.Where(s => s.FacultyId == f.Id).ToList();
                facStudents.Add(new object[] { f.Name, students.Count() });
            }

            return new JsonResult(facStudents);
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData_Floors()
        {
            var floors = _context.Floors.OrderBy(f => f.FloorNumber).ToList();
            List<object> floorStudents = new List<object>();
            floorStudents.Add(new[] { "Поверх", "Кількість студентів на поверсі" });
            foreach (var f in floors)
            {
                var students = (from s in _context.Inhabitants
                                join r in _context.Rooms on s.RoomId equals r.Id
                                where r.FloorNumber == f.Id
                                select s).ToList();

                floorStudents.Add(new object[] { f.FloorNumber, students.Count() });
            }


            return new JsonResult(floorStudents);
        }
    }
}
