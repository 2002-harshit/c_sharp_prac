using Microsoft.AspNetCore.Mvc;
using bank_proj_db_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_proj_db_mvc.Controllers;

public class AccountController : Controller
{
    private readonly PostgresContext _db;

    public AccountController(PostgresContext db)
    {
        _db = db;
    }
    
    // GET, but defualt every action is get
    public IActionResult Index()
    {
        return View(_db.Sbaccounts);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Sbaccount newAccount)
    {
        try
        {
            newAccount.Currentbalance = 0;
            _db.Sbaccounts.Add(newAccount);
            _db.SaveChanges();

            Console.WriteLine("Account created successfully.");
            TempData["SuccessMessage"] = "Account created successfully.";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            ViewBag.ErrorMessage="An error occurred while creating the account. Please try again.";
            return View(newAccount);
        }
    }

    public IActionResult Details(int id)
    {
        try
        {
            Sbaccount acc = _db.Sbaccounts.Find(id);

            if (acc == null)
            {
                TempData["ErrorMessage"] = "Account does not exist";
                return RedirectToAction("Index");
            }
            else
            {
                return View(acc);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            TempData["ErrorMessage"]="An error occurred while fetching the account details. Please try again.";
            return RedirectToAction("Index");
        }
    }

    public IActionResult Edit(int id)
    {
        try
        {
            Sbaccount acc = _db.Sbaccounts.Find(id);

            if (acc == null)
            {
                TempData["ErrorMessage"] = "Account does not exist";
                return RedirectToAction("Index");
            }
            else
            {
                return View(acc);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            TempData["ErrorMessage"]="An error occurred while fetching the account details. Please try again.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Edit(Sbaccount upAcc)
    {
        //* also for fetching the balance, you need to have key, so in the form either have a readonly or hidden filed for it , ir add it to the url, ot have it as the temp data in the get Edit
        // <form asp-action="Edit" asp-route-id="@Model.Accountnumber">
        //     <!-- form fields go here -->
        // </form>
        //*fetch the original, balance or set it is a hidden field in the form itslef, otherwise it will get a default value acc to its datatype
        try
        {
            //Way 1
            // Sbaccount existingAcc = _db.Sbaccounts.Find(upAcc.Accountnumber);
            // existingAcc.Customername = upAcc.Customername;
            // existingAcc.Customeraddress = upAcc.Customeraddress;
            //  _db.SaveChanges();
            
            //Way 2
            Sbaccount existingAcc =
                _db.Sbaccounts.AsNoTracking().First(acc => acc.Accountnumber == upAcc.Accountnumber);
            upAcc.Currentbalance = existingAcc.Currentbalance;
            _db.Sbaccounts.Update(upAcc);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Account updated successfully.";
            return RedirectToAction("Index");
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            TempData["ErrorMessage"]="An error occurred while updating the account details. Please try again.";
            return RedirectToAction("Index");
        }
    }
    
    public IActionResult Delete(int id)
    {
        try
        {
            Sbaccount acc = _db.Sbaccounts.Find(id);

            if (acc == null)
            {
                TempData["ErrorMessage"] = "Account does not exist";
                return RedirectToAction("Index");
            }
            else
            {
                return View(acc);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            TempData["ErrorMessage"]="An error occurred while fetching the account details. Please try again.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            Sbaccount acc = _db.Sbaccounts.Find(id);
    
            if (acc == null)
            {
                TempData["ErrorMessage"] = "Account does not exist";
                return RedirectToAction("Index");
            }
            else
            {
                
                _db.Sbaccounts.Remove(acc);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Account deleted successfully.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            TempData["ErrorMessage"]="An error occurred while fetching the account details. Please try again.";
            return RedirectToAction("Index");
        }
    }

    
}