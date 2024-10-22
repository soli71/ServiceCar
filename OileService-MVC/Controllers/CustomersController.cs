using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OilChangeApp.Data;
using OileService_MVC.Services;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OilChangeApp.Controllers;

public class CreateCustomerViewModel
{
    [Required(ErrorMessage = "لطفاً نام مشتری را وارد کنید.")]
    public string Name { get; set; }

    [RegularExpression(@"^09\d{9}$", ErrorMessage = "لطفاً یک شماره موبایل معتبر وارد کنید.")]
    public string PhoneNumber { get; set; }

    public SelectList Cars { get; set; }
    public int? SelectedCarId { get; set; }
}

public class EditCustomerViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "لطفاً نام مشتری را وارد کنید.")]
    public string Name { get; set; }

    [RegularExpression(@"^09\d{9}$", ErrorMessage = "لطفاً یک شماره موبایل معتبر وارد کنید.")]
    public string PhoneNumber { get; set; }

    public SelectList Cars { get; set; }
    public int? SelectedCarId { get; set; }
}

public class CustomersController : Controller
{
    private readonly OilChangeDbContext _context;
    private readonly ISmsService _smsService;

    public CustomersController(OilChangeDbContext context, ISmsService smsService)
    {
        _context = context;
        _smsService = smsService;
    }

    // GET: Customers
    public IActionResult Index(string searchString)
    {
        ViewData["CurrentFilter"] = searchString;

        var customers = _context.Customers.Include(c => c.Car).AsQueryable();

        if (!String.IsNullOrEmpty(searchString))
        {
            customers = customers.Where(c => c.Name.Contains(searchString) || c.PhoneNumber.Contains(searchString));
        }

        return View(customers.ToList());
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        var carSelectList = GetCarSelectList();

        return View(new CreateCustomerViewModel
        {
            Cars = carSelectList
        });
    }

    private SelectList GetCarSelectList()
    {
        var cars = _context.Cars.ToList();
        var carSelectList = new SelectList(cars, "Id", "Name");
        return carSelectList;
    }

    // POST: Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateCustomerViewModel customer)
    {
        if (ModelState.IsValid)
        {
            var newCustomer = new Customer { CarId = customer.SelectedCarId, Name = customer.Name, PhoneNumber = customer.PhoneNumber };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = newCustomer.Id });
        }
        var carSelectList = GetCarSelectList();
        customer.Cars = carSelectList;
        return View(customer);
    }

    public IActionResult Search(string searchString)
    {
        var customers = from c in _context.Customers
                        select c;

        if (!String.IsNullOrEmpty(searchString))
        {
            customers = customers.Where(s => s.Name.Contains(searchString) || s.PhoneNumber.Contains(searchString));
        }

        return View(nameof(Index), customers.ToList());
    }

    public IActionResult Details(int id)
    {
        var viewModel = GetCustomerDetails(id);

        return View(viewModel);
    }

    private CustomerDetailsViewModel GetCustomerDetails(int id)
    {
        var customer = _context.Customers
    .Include(c => c.Car)
    .Include(c => c.CustomerServices)
    .FirstOrDefault(c => c.Id == id);

        var serviceList = _context.Services.OrderBy(c=>c.Order).Select(s => new ServiceViewModel
        {
            Id = s.Id,
            Name = s.ServiceName,
        }).ToList();

        var customerService = _context.CustomerServices.Where(c => c.CustomerId == id)
            .Include(cs => cs.Oil)
            .Include(cs => cs.CustomerServiceDetails)
                .ThenInclude(cs => cs.Services).Select(cs => new CustomerServiceHistoryViewModel
                {
                    ServiceDate = cs.ServiceDate,
                    OilName = cs.Oil.Name,
                    Kilometers = cs.Kilometers,
                    NextServiceKilometers = cs.NextServiceKilometers,
                    ServicesName = string.Join(", ", cs.CustomerServiceDetails.Select(cs => cs.Services.ServiceName))

                }).OrderByDescending(c => c.ServiceDate).ToList();

        var oils = _context.Oils.ToList();
        var oilSelectList = new SelectList(oils, "Id", "Name");

        var viewModel = new CustomerDetailsViewModel
        {
            CarName = customer.Car?.Name,
            Id = customer.Id,
            Name = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            CustomerServiceHistory = customerService,
            ServiceList = serviceList,
            Oils = oilSelectList
        };
        return viewModel;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddService(CustomerDetailsViewModel model)
    {
        if (ModelState.IsValid)
        {
            string sanitizedKilometers = Regex.Replace(model.Kilometers, @"\D", "");
            string sanitizedNextServiceKilometers = Regex.Replace(model.NextServiceKilometers, @"\D", "");

            if (!long.TryParse(sanitizedKilometers, out long kilometers))
            {
                ModelState.AddModelError("Kilometers", "لطفاً یک مقدار معتبر برای کیلومتر سرویس وارد کنید.");
            }

            if (!long.TryParse(sanitizedNextServiceKilometers, out long nextServiceKilometers))
            {
                ModelState.AddModelError("NextServiceKilometers", "لطفاً یک مقدار معتبر برای کیلومتر بعدی سرویس وارد کنید.");
            }
            var customerService = new CustomerService
            {
                CustomerId = model.Id,
                ServiceDate = DateTime.Now,
                OilId = model.SelectedOilId,
                Kilometers = Int64.Parse(sanitizedKilometers),
                NextServiceKilometers = Int64.Parse(sanitizedNextServiceKilometers)
            };

            var serviceList = new List<CustomerServiceDetail>();
            if (model.SelectedServiceIds != null && model.SelectedServiceIds.Any())
            {
                foreach (var serviceId in model.SelectedServiceIds)
                {
                    var service = new CustomerServiceDetail
                    {
                        ServiceId = serviceId,
                        CustomerService = customerService
                    };
                    serviceList.Add(service);
                }
            }

            customerService.CustomerServiceDetails = serviceList;
            _context.CustomerServices.Add(customerService);

            _context.SaveChanges();

            var customer = _context.Customers.Find(model.Id);
            var services = _context.Services.Where(s => model.SelectedServiceIds.Contains(s.Id)).Select(s => s.ServiceName).ToList();

            var oil = _context.Oils.Find(model.SelectedOilId);

            var concatServices = $" {string.Join(",", services)}";
            var message = $"مشتری گرامی {customer.Name ?? ""}\r\n سرویس {concatServices} با موفقیت انجام شد .\r\n روغن {oil?.Name ?? ""}\r\n کیلومتر فعلی : {model.Kilometers} \r\n کیلومتر بعدی : {model.NextServiceKilometers} \r\n {ToPersianDate(DateTime.Now)} \r\n";

            await _smsService.SendAsync(customer.PhoneNumber, message);

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        var viewModel = GetCustomerDetails(model.Id);

        return View("Details", viewModel);
    }

    public string ToPersianDate(DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();
        int year = pc.GetYear(date);
        int month = pc.GetMonth(date);
        int day = pc.GetDayOfMonth(date);

        return string.Format("{0}/{1}/{2}", year, month.ToString("00"), day.ToString("00"));
    }

    // GET: Customers/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return NotFound();
        }

        var carSelectList = GetCarSelectList();
        return View(new EditCustomerViewModel
        {
            Id = customer.Id,
            SelectedCarId = customer.CarId,
            Name = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            Cars = carSelectList
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name,PhoneNumber,SelectedCarId")] EditCustomerViewModel customer)
    {
        if (id != customer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var customerToUpdate = _context.Customers.Find(id);
                if (customerToUpdate == null)
                {
                    return NotFound();
                }
                customerToUpdate.Name = customer.Name;
                customerToUpdate.PhoneNumber = customer.PhoneNumber;
                customerToUpdate.CarId = customer.SelectedCarId;
                customerToUpdate.Name = customer.Name;
                _context.Update(customerToUpdate);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        return View(customer);
    }

    private bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }

    // GET: Customers/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = _context.Customers
            .FirstOrDefault(m => m.Id == id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var customer = _context.Customers.Include(c => c.CustomerServices).ThenInclude(c => c.CustomerServiceDetails).FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            _context.CustomerServiceDetail.RemoveRange(_context.CustomerServiceDetail.Where(cs => cs.CustomerService.CustomerId == id));
            _context.CustomerServices.RemoveRange(_context.CustomerServices.Where(cs => cs.CustomerId == id));
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}