using Dapper;
using DapperInMVC.Models;
using DapperInMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DapperInMVC.Controllers
{
    public class HomeController : Controller
    {   
        private readonly IDapper _dapper;
        public HomeController(IDapper dapper)
        {
            _dapper = dapper;
        }
        public IActionResult Create()
        {
            return View();
        }
        public  IActionResult Index()
        {
           var Employee =  Task.FromResult(_dapper.GetAll<Employee>($"Select * from [StudentDTO] ", null, commandType: CommandType.Text).ToList());
            return View(Employee.Result);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult Create_Post(Employee data)
        {
            if (ModelState.IsValid)
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Name", data.Name, DbType.String);
                dbparams.Add("Age", data.Agr, DbType.Int32);
                dbparams.Add("City", data.City, DbType.String);
                var result = Task.FromResult(_dapper.Insert<Employee>("[dbo].[AddNewStudentDTO]"
                    , dbparams,
                    commandType: CommandType.StoredProcedure));
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [HttpGet]
        [ActionName("Details")]
        public IActionResult Details(int id)
        {
            var result =  Task.FromResult(_dapper.Get<Employee>($"Select * from [StudentDTO] where Id = {id}", null, commandType: CommandType.Text));
            return View(result.Result);
           
        }

        }
}
