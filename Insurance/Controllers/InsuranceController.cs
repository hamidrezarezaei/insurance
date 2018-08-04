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
    public class InsuranceController : Controller
    {
        //context ctx;
        repository repository;
        private readonly HookManager hookManager;

        public InsuranceController(repository repository,HookManager hookManager)
        {
            //this.ctx = ctx;
            this.repository = repository;
            this.hookManager = hookManager;
        }
        [HttpGet("[action]")]
        public IEnumerable<insurance_client> GetInsurances()
        {
            return repository.GetActiveInsurances_Client();
        }

        [HttpGet("[action]")]
        public step_client GetStep(int insuranceId, int stepNumber)
        {
            return repository.GetStep_client(insuranceId, stepNumber);
        }

        [HttpGet("[action]")]
        public List<dataValue_client> GetDataValues(int dataTypeId)
        {
            return repository.GetAcitveDataValues_Client(dataTypeId);
        }

        [HttpGet("[action]")]
        public List<dataValue_client> GetChildDataValues(int dataTypeId, int fatherId)
        {
            return repository.GetActiveChildDataValues_Client(dataTypeId, fatherId);
        }

        [HttpPost("[action]")]
        public int CalcPrice([FromBody]step step)
        {
            //var json = JsonConvert.SerializeObject(step);
            //var price = ctx.price.FromSql("calc1 @step={0}", json).FirstOrDefault();
            //return price.value;
            return 100;
        }
    }
}
