using FlightBookingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingApp.Controllers;

public class CustomerController : Controller
{
    private readonly PostgresContext _db;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(PostgresContext db,ILogger<CustomerController> logger)
    {
        _db = db;
        _logger = logger;
    }
    // GET
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Register(Customer newCust)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(newCust);
            }
            Customer? existingCustomer = _db.Customers.FirstOrDefault(c => c.Email.ToLower().Equals(newCust.Email.ToLower()));
            if (existingCustomer != null)
            {
                ModelState.AddModelError("Email","An account with this email already exists.");
                return View(newCust);
            }

            _db.Customers.Add(newCust);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Account created successfully.";
            return RedirectToAction("Login");
        }
        catch (Exception e)
        {
            
            ViewBag.ErrorMessage="An error occurred while creating the account. Please try again.";
            return View(newCust);
        }
        
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Customer cust)
    {
        try
        {
            Customer? existingCust= _db.Customers.FirstOrDefault(c=> c.Email.ToLower().Equals(cust.Email.ToLower()) && c.Password.Equals(cust.Password));
            if (existingCust == null)
            {
                ViewBag.ErrorMessage="Account does not exist, Please Register!!";
                return View(cust);
            }
            //* if login is successfull then
            HttpContext.Session.SetString("Id",existingCust.Customerid.ToString());
            HttpContext.Session.SetString("Name",existingCust.Name);
            HttpContext.Session.SetString("Email",existingCust.Email);
            
            
            //change this
            return RedirectToAction("Index","BookingDetail");

        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage="An error occurred while creating the account. Please try again.";
            return View(cust);
        }
        
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}