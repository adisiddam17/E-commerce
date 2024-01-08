using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NEWMVCWebApp.Models;

using HRApp.service;
using HRApp.Entities;

namespace NEWMVCWebApp.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeesService _svc;

    public EmployeesController(IEmployeesService svc)
    {
        _svc=svc;
    }
    public IActionResult Index()
    {
        List<Employee> emps=_svc.getAll();
        // ViewData["employees"]=emps;
        ViewBag.employees=emps;
        return View();
    }
   
    public IActionResult Details(int id)
    {
        Employee e=_svc.getById(id);
        Console.WriteLine("In controller Details "+e);
        TempData["employee"]=e;
       
        return View();
    }


    public IActionResult Insert()
    {
        Employee e=_svc.getById(12);
        return View();
    }

    [HttpPost]
    public IActionResult Insert(string id,string name,string basicSal,string dailyAllo,string tax,string nowd)
    {
        Employee e=new Employee(int.Parse(id),name,double.Parse(basicSal),double.Parse(dailyAllo),double.Parse(tax),int.Parse(nowd));
        // Console.WriteLine(e);
        string msg=_svc.insert(e);
        ViewData["mesg"]=msg;
        // return View();
        return RedirectToAction("Index");
    }

    public IActionResult Update(int id)
    {
        Employee e=_svc.getById(id);
        ViewData["employee"]=e;      
        return View();
    }

    [HttpPost]
    public IActionResult Update(string id,string name,string basicSal,string dailyAllo,string tax,string nowd)
    {
        Employee e=new Employee(int.Parse(id),name,double.Parse(basicSal),double.Parse(dailyAllo),double.Parse(tax),int.Parse(nowd));
        string mesg=_svc.update(e);
        ViewData["mesg"]=mesg;

        return RedirectToAction("Index");
        //  return RedirectToAction("AnotherAction", "AnotherController");
    }

    public IActionResult Delete(int id)
    {
        string mesg=_svc.delete(id);
        ViewData["mesg"]=mesg;
        // return View();
        return RedirectToAction("Index");
    }

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
