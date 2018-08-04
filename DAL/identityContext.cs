using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class identityContext : IdentityDbContext<user,role,int>
    {
        public identityContext(DbContextOptions options) : base(options)
        {

        }
    }
}
