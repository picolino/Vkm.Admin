﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VKM.Admin.Models;
using VKM.Admin.Models.Database;
using VKM.Admin.Services;
using VKM.Admin.Services.Interfaces;

namespace VKM.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly Config config;
        private readonly IDatabaseProvider databaseProvider;
        
        public HomeController(IOptions<Config> config)
        {
            this.config = config.Value;
            databaseProvider = new SqLiteDatabaseProvider(this.config.DatabaseConnectionString);
        }
        
        public IActionResult Index()
        {
            var teams = databaseProvider.LoadTeamsAndStudents();
            
            return View(teams);
        }

        #region Student

        public IActionResult Student(int id)
        {
            var student = databaseProvider.LoadStudentById(id);
            var studentHistory = databaseProvider.LoadHistoryByStudentId(id);

            var json = new {Student = student, History = studentHistory};

            return Json(json);
        }
        
        public IActionResult UpdateStudent(Student student)
        {
            databaseProvider.UpdateStudent(student);

            return Ok();
        }
        
        public IActionResult RemoveStudent(int id)
        {
            try
            {
                databaseProvider.RemoveStudentById(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }

        #endregion

        #region Team

        public IActionResult Team(int id)
        {
            var team = databaseProvider.LoadTeam(id);

            return Json(team);
        }

        public IActionResult Teams()
        {
            var teams = databaseProvider.LoadTeams();
            
            return Json(teams);
        }
        
        public IActionResult UpdateTeam(Team team)
        {
            databaseProvider.UpdateTeam(team);

            return Ok();
        }

        public IActionResult RemoveTeam(int id)
        {
            databaseProvider.RemoveTeamById(id);

            return Ok();
        }

        #endregion
        
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}