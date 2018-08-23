using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Insurance.Controllers
{
    [Route("api/[controller]")]
    public class ReminderController : Controller
    {
        //context ctx;
        repository repository;
        private readonly HookManager hookManager;

        public ReminderController(repository repository,HookManager hookManager)
        {
            //this.ctx = ctx;
            this.repository = repository;
            this.hookManager = hookManager;
        }
        
        [HttpPost("[action]")]
        public bool AddReminder([FromBody]reminder reminder)
        {
            try
            {
                this.repository.AddEntity(reminder);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
