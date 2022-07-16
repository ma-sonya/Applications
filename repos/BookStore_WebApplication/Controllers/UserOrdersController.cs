#nullable disable
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore_WebApplication.Models;
using BookStore_WebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BookStore_WebApplication.Controllers
{
    public class UserOrdersController : Controller
    {
        private readonly StoreDBContext _context;
        private readonly IdentityContext _identityContext;
        private readonly UserManager<User> _userManager;

        public UserOrdersController(StoreDBContext context, IdentityContext identityContext, UserManager<User> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var stores = (from s in _context.BookStores
                          select s).ToList();

            ViewBag.Stores = stores;

            var id = _userManager.GetUserId(User);

            var ourUser = (from u in _identityContext.Users
                           where u.Id == id
                           select u).First();

            var ourClient = (from c in _context.Clients
                             where c.Id == ourUser.client_id
                             select c).ToList();

            if (ourClient.Count() == 0)
            {
                var storeDBContext1 = _context.Orders.Where(o => o.IdClientNavigation == null).Include(o => o.IdDeliveryNavigation).Include(o => o.IdEmployeeNavigation);
                return View(await storeDBContext1.ToListAsync());
            }

            var storeDBContext = _context.Orders.Where(o => o.IdClientNavigation == ourClient.First()).Include(o => o.IdDeliveryNavigation).Include(o => o.IdEmployeeNavigation);

            return View(await storeDBContext.ToListAsync());
        }

        public IActionResult Success()
        {
            return View();
        }

        List<Book_BookOrderDto> GetBooksByStore(int store_id)
        {
            List<Book_BookOrderDto> Books = new List<Book_BookOrderDto>();
            var bookList = _context.Books.ToList();

            if (store_id == 0) //для нової книги
            {
                foreach (var item in bookList)
                {
                    Book_BookOrderDto bbodto = new Book_BookOrderDto(item.Id, item.Name, item.Cost, item.Type, item.IdStore, 0, false);
                    Books.Add(bbodto);
                }
            }
            else //для існуючої книги
            {
                var id = _userManager.GetUserId(User);

                var ourUser = (from u in _identityContext.Users
                               where u.Id == id
                               select u.client_id).First();

                var ourOrder = (from bo in _context.BookOrders
                                join b in _context.Books on bo.IdBook equals b.Id
                                join o in _context.Orders on bo.IdOrder equals o.Id
                                where b.IdStore == store_id && o.IdClient == ourUser
                                select bo.IdOrder).First();

                foreach (var item in bookList)
                {
                    if (item.IdStore == store_id)
                    {
                        var ourNumber = (from bo in _context.BookOrders
                                         where bo.IdOrder == ourOrder && bo.IdBook == item.Id
                                         select bo.Number).ToList();

                        int num = ((ourNumber.Count() == 0) == true) ? 0 : ourNumber.First();

                        if (num != 0)
                        {
                            Book_BookOrderDto bbodto = new Book_BookOrderDto(item.Id, item.Name, item.Cost, item.Type, item.IdStore, num, true);
                            Books.Add(bbodto);
                        }
                        else
                        {
                            Book_BookOrderDto bbodto = new Book_BookOrderDto(item.Id, item.Name, item.Cost, item.Type, item.IdStore, 0, false);
                            Books.Add(bbodto);
                        }
                    }
                }
            }

            var SortedBookList = Books.OrderBy(o => o.IdStore).ToList();

            return SortedBookList;
        }


        List<Book_BookOrderDto> GetBooks_BookOrderDto(int id)
        {
            List<Book_BookOrderDto> Books = new List<Book_BookOrderDto>();

            var bookList = _context.Books.ToList();

            if (id == 0)
            {
                foreach (var item in bookList)
                {
                    Book_BookOrderDto bbodto = new Book_BookOrderDto(item.Id, item.Name, item.Cost, item.Type, item.IdStore, 0, false);
                    Books.Add(bbodto);
                }
            }
            else
            {
                var ourBookId = (from bo in _context.BookOrders
                               where bo.IdOrder == id
                               select bo.IdBook).ToList();

                if (ourBookId.Count() == 0)
                {
                    return null;
                }

                var ourBook = _context.Books.Where(b => b.Id == ourBookId.First()).First();

                var ourStore = (from bs in _context.BookStores
                                where ourBook.IdStore == bs.Id
                                select bs).First();

                bookList = _context.Books.Where(b => b.IdStore == ourStore.Id).ToList();

                var Order = _context.Orders.Where(b => b.Id == id).Include(b => b.BookOrders).FirstOrDefault();
                var bookListForOrder = Order.BookOrders;

                foreach (var item in bookList)
                {
                    var ourNumber = (from bo in _context.BookOrders
                                     where bo.IdOrder == id && bo.IdBook == item.Id
                                     select bo.Number).ToList();

                    int num = ((ourNumber.Count() == 0) == true) ? 0 : ourNumber.First();

                    if (num != 0)
                    {
                        Book_BookOrderDto bbodto = new Book_BookOrderDto(item.Id, item.Name, item.Cost, item.Type, item.IdStore, num, true);
                        Books.Add(bbodto);
                    }
                    else
                    {
                        Book_BookOrderDto bbodto = new Book_BookOrderDto(item.Id, item.Name, item.Cost, item.Type, item.IdStore, 0, false);
                        Books.Add(bbodto);
                    }
                }
            }

            var SortedBookList = Books.OrderBy(o => o.IdStore).ToList();

            return SortedBookList;
        }

        public IActionResult Create()
        {
            ViewData["Books"] = new SelectList(_context.Books, "Id", "Name");

            var stores = (from s in _context.BookStores
                          select s).ToList();

            ViewBag.Stores = stores;

            ViewBag.AllBooks = GetBooksByStore(0);          

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = (from c in _context.Clients
                                  where c.FullName == model.ClientName &&
                                        c.Address == model.ClientAddress &&
                                        c.PhoneNumber == model.ClientPhoneNumber
                                  select c.Id).ToList();

                    var id = _userManager.GetUserId(User);

                    var ourUser = (from u in _identityContext.Users
                                   where u.Id == id
                                   select u).First();

                    if (client.Count() == 0)
                    {
                        Client newClient = new Client { FullName = model.ClientName, Address = model.ClientAddress, PhoneNumber = model.ClientPhoneNumber };
                        int raw = _context.Database.ExecuteSqlRaw("INSERT INTO Client(FullName, PhoneNumber, Address) VALUES({0}, {1}, {2})", newClient.FullName, newClient.PhoneNumber, newClient.Address);                        

                        var ourClient = (from c in _context.Clients
                                         where c.FullName == model.ClientName && c.Address == model.ClientAddress && c.PhoneNumber == model.ClientPhoneNumber
                                         select c).First();

                        client.Add(ourClient.Id);                        

                        ourUser.client_id = ourClient.Id;
                        _identityContext.Users.Update(ourUser);
                        _identityContext.SaveChanges();
                    }

                    List<Book> bookList = _context.Books.Include(a => a.BookOrders).ToList(); //усі книги                    

                    foreach (var tmpbook in bookList)
                    {
                        string bookId = tmpbook.Id.ToString();
                        var t = Request.Form[bookId.ToString()];                                            

                        if (t.Count() > 0) //За оновленими даниим, книгу обрано
                        {
                            var OurNumber = Convert.ToInt32(Request.Form["number_" + tmpbook.Id.ToString()]);

                            var ourEmp = (from e in _context.Employees
                                          where e.IdStore == tmpbook.IdStore
                                          select e.Id).ToList();

                            var isOrder = (from o in _context.Orders
                                           where o.IdClient == ourUser.client_id && o.IdEmployee == ourEmp.First()
                                           select o).ToList();

                            if (isOrder.Count() > 0)
                            {
                                var ourOrder = _context.Orders.Where(a => a.Id == isOrder.First().Id).ToList();

                                var b_order = (from bo in _context.BookOrders
                                               join o in _context.Orders on bo.IdOrder equals o.Id
                                               where bo.IdBook == tmpbook.Id && o.IdClient == ourUser.client_id
                                               select bo).ToList();

                                if (b_order.Count() > 0)
                                {
                                    if (OurNumber != 0)
                                    {
                                        int raw = _context.Database.ExecuteSqlRaw("UPDATE BookOrder SET Number = {2} WHERE IdOrder = {0} AND IdBook = {1}", ourOrder.First().Id, tmpbook.Id, OurNumber);
                                    }
                                    else
                                    {
                                        int raw = _context.Database.ExecuteSqlRaw("DELETE FROM BookOrder WHERE IdOrder = {0} AND IdBook = {1}", ourOrder.First().Id, tmpbook.Id);
                                    }
                                }
                                else
                                {
                                    int raw = _context.Database.ExecuteSqlRaw("INSERT INTO BookOrder(IdOrder, IdBook, Number) VALUES({0}, {1}, {2})", ourOrder.First().Id, tmpbook.Id, OurNumber);
                                }
                            }
                            else
                            {
                                if (OurNumber != 0)
                                {
                                    var ourDelivery = _context.Deliveries.FirstOrDefault();

                                    var ourEmloyee = _context.Employees.Where(a => a.IdStore == tmpbook.IdStore).FirstOrDefault();

                                    var ourDate = DateTime.UtcNow;

                                    Order newOrder = new Order { IdClient = ourUser.client_id, IdDelivery = ourDelivery.Id, IdEmployee = ourEmloyee.Id, OrderDate = ourDate };

                                    int raw = _context.Database.ExecuteSqlRaw("INSERT INTO \"Order\"(IdClient, IdDelivery, IdEmployee, OrderDate) VALUES({0}, {1}, {2}, {3})", newOrder.IdClient, newOrder.IdDelivery, newOrder.IdEmployee, newOrder.OrderDate);

                                    var ourOrder = _context.Orders.Where(a => a.IdClient == ourUser.client_id && a.IdEmployee == ourEmloyee.Id && a.IdDelivery == ourDelivery.Id && a.OrderDate == newOrder.OrderDate).ToList();

                                    int raw1 = _context.Database.ExecuteSqlRaw("INSERT INTO BookOrder(IdOrder, IdBook, Number) VALUES({0}, {1}, {2})", ourOrder.First().Id, tmpbook.Id, OurNumber);
                                }
                            }
                        }
                        else
                        {
                            var isOrder = (from o in _context.Orders
                                           where o.IdClient == ourUser.client_id
                                           select o).ToList();

                            if (isOrder.Count() != 0)
                            {
                                var ourOrder = (from o in _context.Orders
                                                where o.Id == isOrder.First().Id
                                                select o).ToList();

                                var OurNumber = Convert.ToInt32(Request.Form["number_" + tmpbook.Id.ToString()]);

                                int raw = _context.Database.ExecuteSqlRaw("DELETE FROM BookOrder WHERE IdOrder = {0} AND IdBook = {1} AND Number = {2}", ourOrder.First().Id, tmpbook.Id, OurNumber);

                            }                                       
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction("Success");
            }

            var stores = (from s in _context.BookStores
                          select s).ToList();

            ViewBag.Stores = stores;

            ViewBag.AllBooks = GetBooksByStore(0);

            return View();
        }

        public async Task<IActionResult> Add(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            ViewBag.Books = GetBooks_BookOrderDto(id);

            return View(order);
        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddConfirmed(Order ourOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = _userManager.GetUserId(User);

                    var ourUser = (from u in _identityContext.Users
                                   where u.Id == id
                                   select u).First();

                    List<Book> bookList = _context.Books.Include(a => a.BookOrders).ToList(); //усі 

                    foreach (var item in bookList)
                    {
                        string bookId = item.Id.ToString();
                        var t = Request.Form[bookId.ToString()];

                        if (t.Count > 0) //true - за оновленими даними автор є автором книги
                        {
                            Book bk = (_context.Books.Where(b => b.Id == item.Id).Include(b => b.BookOrders).FirstOrDefault());
                            bk.Name = item.Name;
                            bk.Cost = item.Cost;
                            bk.Type = item.Type;
                            bk.IdStore = item.IdStore;
                            List<BookOrder> OrdersBookList = bk.BookOrders.ToList(); // ... книги до змін

                            var OurNumber = Convert.ToInt32(Request.Form["number_" + item.Id.ToString()]);

                            var b_order = (from bo in _context.BookOrders
                                           from o in _context.Orders
                                           where bo.IdOrder == ourOrder.Id && bo.IdBook == item.Id && o.IdClient == ourUser.client_id
                                           select bo).ToList();

                            if (b_order.Count() > 0)
                            {
                                if (OurNumber != 0)
                                {
                                    int raw = _context.Database.ExecuteSqlRaw("UPDATE BookOrder SET Number = {2} WHERE IdOrder = {0} AND IdBook = {1}", ourOrder.Id, item.Id, OurNumber);
                                }
                                else
                                {
                                    int raw = _context.Database.ExecuteSqlRaw("DELETE FROM BookOrder WHERE IdOrder = {0} AND IdBook = {1}", ourOrder.Id, item.Id);
                                }
                            }
                            else
                            {
                                int raw = _context.Database.ExecuteSqlRaw("INSERT INTO BookOrder(IdOrder, IdBook, Number) VALUES({0}, {1}, {2})", ourOrder.Id, item.Id, OurNumber);
                            }

                        }
                        else //true - за оновленими даними автор не є автором книги
                        {
                            Book bk = (_context.Books.Where(b => b.Id == item.Id).Include(b => b.BookOrders).FirstOrDefault());
                            bk.Name = item.Name;
                            bk.Cost = item.Cost;
                            bk.Type = item.Type;
                            bk.IdStore = item.IdStore;
                            List<BookOrder> OrdersBookList = bk.BookOrders.ToList(); // ... книги до змін

                            var OurNumber = Convert.ToInt32(Request.Form["number_" + item.Id.ToString()]);

                            if (OrdersBookList.Where(a => a.IdBook == Int32.Parse(bookId)).Count() > 0) //за попереднім списком був автором книги - потрібно видалити
                            {
                                int raw = _context.Database.ExecuteSqlRaw("DELETE FROM BookOrder WHERE IdOrder = {0} AND IdBook = {1} AND Number = {2}", ourOrder.Id, item.Id, OurNumber);
                            }
                        }
                    }

                    if (GetBooks_BookOrderDto(ourOrder.Id) == null)
                    {
                        int raw = _context.Database.ExecuteSqlRaw("DELETE FROM \"Order\" WHERE Id = {0}", ourOrder.Id);
                    }
                    //_context.Update(ourOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction("Success");
            }

            //ViewBag.Books = GetBooks_BookOrderDto(ourOrder.Id);

            return View(ourOrder);
        }
    }
}
