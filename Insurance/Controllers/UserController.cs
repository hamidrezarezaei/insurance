using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

using Entities;
using DAL;
using Insurance.Services;

namespace Insurance.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly repository repository;
        private readonly HookManager hookManager;

        public UserController(repository repository, HookManager hookManager)
        {
            this.repository = repository;
            this.hookManager = hookManager;
        }


        [HttpGet("[action]")]
        public user_express GetUser()
        {
            user_express newUser = new user_express();
            return newUser;
        }
        [HttpPost("Login")]
        public user_express Login([FromBody]user_express tempUser)
        {
            try
            {
                var result = repository.Login(tempUser.actualUserName, tempUser.password);
                if (result.Succeeded)
                {
                    return repository.GetUser_Express(tempUser.actualUserName);
                }
                throw new Exception();
            }
            catch
            {
                user_express tmp = new user_express();
                tmp.clientMessage = "نام کاربری و یا کلمه عبور اشتباه است.";
                return tmp;
            }
        }

        [HttpPost("Register")]
        public user_express Register([FromBody]user_express newUser)
        {
            try
            {
                var result = repository.AddUser(newUser);
                if (result.Succeeded)
                {
                    this.hookManager.HookFired("register", this.repository.GetUser(newUser.actualUserName));
                    var res = repository.Login(newUser.actualUserName, newUser.password);
                    if (res.Succeeded)
                    {
                        return repository.GetUser_Express(newUser.actualUserName);
                    }
                }
                throw new Exception();

            }
            catch
            {
                user_express tmp = new user_express();
                tmp.clientMessage = "خطایی در ثبت کاربر بوجود آمده است.";
                return tmp;
            }

        }

    }
}