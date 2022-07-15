using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormWebApplication;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;

namespace DormWebApplication.Controllers
{
    [Authorize(Roles ="admin, user")]
    public class RoomsController : Controller
    {
        private readonly DBDormContext _context;

        public RoomsController(DBDormContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var dBDormContext = _context.Rooms.Include(r => r.Floor);
            return View(await dBDormContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Floor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            var students = _context.Inhabitants.Where(s => s.RoomId == id);
            ViewBag.Students = students;

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["FloorNumber"] = new SelectList(_context.Floors, "Id", "FloorNumber");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountPlace,FloorNumber,Resident,RoomNumber")] Room room)
        {
            if (ModelState.IsValid)
            {
                var isRoom = _context.Rooms.Where(r=> r.RoomNumber == room.RoomNumber).Where(i=>i.CountPlace == room.CountPlace);
                var floor = _context.Floors.Where(f => f.Id == room.FloorNumber).FirstOrDefault();
                var floorRoom = room.RoomNumber[0].ToString();
                //var placeRoom = room.CountPlace.ToString();

                if(isRoom != null)
                {
                    if (isRoom.Count() == 0)
                    {
                        if (floorRoom == floor.FloorNumber.ToString())
                        {
                            _context.Add(room);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            await _context.SaveChangesAsync();
                            ViewData["FloorNumber"] = new SelectList(_context.Floors, "Id", "Id", room.FloorNumber);
                            return View(room);
                        }
                    }
                    else
                    {
                        
                        await _context.SaveChangesAsync();
                        ViewData["FloorNumber"] = new SelectList(_context.Floors, "Id", "Id", room.FloorNumber);
                        return View(room);
                    }
                }


            }
            ViewData["FloorNumber"] = new SelectList(_context.Floors, "Id", "Id", room.FloorNumber);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["FloorNumber"] = new SelectList(_context.Floors, "Id", "FloorNumber", room.FloorNumber);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountPlace,FloorNumber,Resident,RoomNumber")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FloorNumber"] = new SelectList(_context.Floors, "Id", "Id", room.FloorNumber);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Floor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            var inhabs = _context.Inhabitants.Where(i => i.RoomId == room.Id).ToList();
            var furnts = _context.Furnitures.Where(f => f.RoomId == room.Id).ToList();

            if (furnts != null)
            {
                if (furnts.Count() != 0)
                {
                    foreach (var fur in furnts)
                    {
                        fur.RoomId = null;
                        _context.SaveChanges();
                    }
                }
            }

            if (inhabs!=null)
            {
                if (inhabs.Count() != 0)
                {
                    foreach (var inh in inhabs)
                    {
                        _context.Remove(inh);
                    }
                }
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                var worksheetRoomName = worksheet.Name[0].ToString() + worksheet.Name[1].ToString() + worksheet.Name[2].ToString();
                                Room newRoom;

                                var r = (from room in _context.Rooms
                                         where room.RoomNumber.Contains(worksheetRoomName)
                                         select room).ToList();

                                if (r.Count > 0)
                                {
                                    newRoom = r[0];
                                }
                                else
                                {
                                    newRoom = new Room();
                                    newRoom.RoomNumber = worksheet.Name;

                                    var floor = worksheet.Cell("B2").Value;
                                    //Чи існує поверх
                                    var isFloorEmpty = (from fl in _context.Floors
                                                        where fl.FloorNumber == Convert.ToInt32(floor)
                                                        select fl).FirstOrDefault();
                                    Floor newFloor;

                                    if (isFloorEmpty == null)
                                    {
                                        newFloor = new Floor();
                                        newFloor.FloorNumber = Convert.ToInt32(floor);
                                        if (worksheet.Cell("B3").Value is (object)"TRUE" or (object)"ИСТИНА" or (object)"ІСТИНА")
                                        {
                                            newFloor.IsKitchenOpen = true;
                                        }
                                        else
                                            newFloor.IsKitchenOpen = false;

                                        _context.Floors.Add(newFloor);
                                    }
                                    else
                                        newFloor = _context.Floors.Where(f => f.Id == isFloorEmpty.Id).FirstOrDefault();

                                    newRoom.FloorNumber = Convert.ToInt32(floor);
                                    newRoom.Floor = _context.Floors.Where(r => r.FloorNumber == Convert.ToInt32(floor)).FirstOrDefault();                                    
                                    newRoom.CountPlace = Convert.ToInt32(worksheet.Cell("C2").Value);
                                    _context.Rooms.Add(newRoom);
                                    newFloor.Rooms.Add(newRoom);
                                }

                                try
                                {
                                    if (worksheet.Cell("D2").Value.ToString() == "")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        using (StreamWriter str = new StreamWriter(@"D:\res.txt"))
                                        {
                                            for (int i = 2; worksheet.Cell("D" + i).Value.ToString() != ""; i++)
                                            {                                               
                                                var name = worksheet.Cell("D" + i).Value.ToString();
                                                var number = Convert.ToInt32(worksheet.Cell("E" + i).Value);

                                                var ifFurn = _context.Furnitures.Where(f => f.Name == name).Where(f => f.RoomId == newRoom.Id).ToList();                                            

                                                if (ifFurn != null)
                                                {
                                                    if (ifFurn.Count() != 0)
                                                    {
                                                        foreach (var item in ifFurn)
                                                        {
                                                            item.Number = number;
                                                            _context.SaveChanges();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Furniture newFurn = new Furniture();
                                                        newFurn.Name = name;
                                                        newFurn.Number = number;
                                                        newFurn.RoomId = newRoom.Id;                                                        
                                                        newFurn.Room = _context.Rooms.Where(r => r.Id == newRoom.Id).FirstOrDefault();
                                                        newRoom.Furnitures.Add(newFurn);

                                                        _context.Furnitures.Add(newFurn);
                                                    }
                                                }                                                
                                            }
                                        }
                                    }

                                    _context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }                            
                                
                            }
                        }
                        
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {

                var rooms = _context.Rooms.ToList();

                foreach (var r in rooms)
                {
                    var worksheet = workbook.Worksheets.Add(r.RoomNumber + r.CountPlace);

                    worksheet.Cell("A1").Value = "Номер";
                    worksheet.Cell("B1").Value = "Поверх";
                    worksheet.Cell("C1").Value = "Кількість місць";
                    worksheet.Cell("D1").Value = "Меблі";
                    worksheet.Cell("E1").Value = "Кількість мебелі";

                    worksheet.Cells("A1:E1").Style.Font.Bold = true;

                    worksheet.Column(1).Width = 10;
                    worksheet.Column(2).Width = 10;
                    worksheet.Column(3).Width = 30;
                    worksheet.Column(4).Width = 30;
                    worksheet.Column(5).Width = 20;

                    var worksheetRoomName = worksheet.Name[0].ToString() + worksheet.Name[1].ToString() + worksheet.Name[2].ToString();

                    for (int i = 0, j = 2; i < rooms.Count(); i++)
                    {
                        if (rooms[i].RoomNumber == worksheetRoomName)
                        {
                            worksheet.Cell("A2").Value = rooms[i].RoomNumber;
                            var floorNum = _context.Floors.Where(f => f.Id == rooms[i].FloorNumber).FirstOrDefault();

                            worksheet.Cell("B2").Value = floorNum.FloorNumber;

                            if (floorNum != null)
                            {
                                if (floorNum.IsKitchenOpen == true)
                                    worksheet.Cell("B3").Value = true;
                                else
                                    worksheet.Cell("B3").Value = false;
                            }

                            worksheet.Cell("C2").Value = rooms[i].CountPlace;

                            var furns = _context.Furnitures.Where(f => f.RoomId == rooms[i].Id).ToList();

                            foreach (var fur in furns)
                            {
                                worksheet.Cell("D" + j).Value = fur.Name;
                                worksheet.Cell("E" + j).Value = fur.Number;
                                j++;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Dorm_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        public ActionResult DExport()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var Rooms = _context.Rooms.ToList();

                    MainDocumentPart mainPart = package.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    var body = new Body();

                    foreach (var room in Rooms)
                    {
                        #region RUNs
                        Run runRoomNumber = new Run();
                        Run runRoomCount = new Run();
                        Run runRoomFloor = new Run();

                        Run runRoomMembers = new Run();
                        Run runFurnitures = new Run();

                        Run run = new Run();

                        #endregion RUNs

                        #region PARAGRAPHs
                        Paragraph roomNumber = new Paragraph();
                        Paragraph roomCount = new Paragraph();
                        Paragraph roomFloor = new Paragraph();

                        Paragraph roomMembers = new Paragraph();
                        Paragraph roomFurnitures = new Paragraph();

                        Paragraph paragraph = new Paragraph();
                        #endregion PARAGRAPHs

                        var inh = _context.Inhabitants.Where(a => a.RoomId == room.Id).ToList();
                        var fur = _context.Furnitures.Where(a => a.RoomId == room.Id).ToList();

                        RunProperties runHeaderProperties = runRoomNumber.AppendChild(new RunProperties(new Bold()));
                        RunProperties runProperties = runRoomNumber.AppendChild(new RunProperties(new Italic()));


                        runRoomNumber.AppendChild(new Text($"Номер кімнати: {room.RoomNumber}"));
                        roomNumber.Append(runRoomNumber);
                        body.Append(roomNumber);

                        runRoomCount.AppendChild(new Text($"Кількість місць: {room.CountPlace}"));
                        roomCount.Append(runRoomCount);
                        body.Append(roomCount);

                        runRoomFloor.AppendChild(new Text($"Поверх: {room.FloorNumber}"));
                        roomFloor.Append(runRoomFloor);
                        body.Append(roomFloor);

                        runRoomMembers.AppendChild(new Text("Мешканці:"));
                        roomMembers.Append(runRoomMembers);
                        body.Append(roomMembers);

                        if (inh.Count() > 0)
                        {
                            foreach (var inhab in inh)
                            {
                                Run runM = new Run();
                                Paragraph roomM = new Paragraph();
                                runM.AppendChild(new Text($"•   {inhab.Name}"));
                                roomM.Append(runM);
                                body.Append(roomM);
                            }
                        }
                        else
                        {
                            Run runM = new Run();
                            Paragraph roomM = new Paragraph();
                            runM.AppendChild(new Text("•   Мешканці відсутні."));
                            roomM.Append(runM);
                            body.Append(roomM);
                        }

                        runFurnitures.AppendChild(new Text("Меблі:"));
                        roomFurnitures.Append(runFurnitures);
                        body.Append(roomFurnitures);

                        if (fur.Count() > 0)
                        {
                            foreach (var furnit in fur)
                            {
                                Run runF = new Run();
                                Paragraph roomF = new Paragraph();
                                runF.AppendChild(new Text($"•   {furnit.Name}  |  {furnit.Number}.шт"));
                                roomF.Append(runF);
                                body.Append(roomF);
                            }
                        }
                        else
                        {
                            Run runF = new Run();
                            Paragraph roomF = new Paragraph();
                            runF.AppendChild(new Text("•   Меблі відсутні."));
                            roomF.Append(runF);
                            body.Append(roomF);
                        }

                        run.AppendChild(new Text("------------------------------------------------------------------------------------------------------------------------------------------"));
                        paragraph.Append(run);
                        body.Append(paragraph);
                    }

                    mainPart.Document.Append(body);
                    package.Close();
                }

                ms.Flush();
                return new FileContentResult(ms.ToArray(), "application/vnd.ms-word")
                {
                    //змініть назву файла відповідно до тематики Вашого проєкту
                    FileDownloadName = $"Dorm_{DateTime.UtcNow.ToShortDateString()}.docx"
                };
            }
        }
    }
}
