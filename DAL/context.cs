using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class context : DbContext
    {
        public context(DbContextOptions<context> options) : base(options)
        {

        }
        public context()
        {

        }
        public DbSet<site> sites { get; set; }
        public DbSet<insurance> insurances{ get; set; }
        public DbSet<step> steps{ get; set; }
        public DbSet<fieldSet> fieldSets { get; set; }
        public DbSet<field> fields { get; set; }
        public DbSet<dataType> dataTypes { get; set; }
        public DbSet<dataValue> dataValues { get; set; }
        public DbSet<dataValue_category> dataValue_category { get; set; }
        
        public DbSet<term> terms { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<attribute> attributes { get; set; }
        public DbSet<setting>settings{ get; set; }
        //public DbSet<user> users { get; set; }
        public DbSet<menu> menus { get; set; }
        public DbSet<boxCategory> boxCategories { get; set; }
        public DbSet<box> boxes { get; set; }
        public DbSet<postCategory> postCategories { get; set; }
        public DbSet<post> posts { get; set; }
        public DbSet<post_postCategory> post_postCategory { get; set; }
        public DbSet<price> price { get; set; }
        public DbSet<order> orders{ get; set; }
        public DbSet<orderField> orderFields{ get; set; }
        public DbSet<paymentType> paymentTypes{ get; set; }
        public DbSet<orderStatus> orderStatuses{ get; set; }
        public DbSet<user_paymentType> user_paymentType { get; set; }
        public DbSet<adminMenu> adminMenus { get; set; }
        public DbSet<role_adminMenu> role_adminMenu { get; set; }
        public DbSet<hook> hooks{ get; set; }
        public DbSet<sms> smses{ get; set; }
        public DbSet<email> emails { get; set; }
        public DbSet<reminder> reminders { get; set; }
    }
}
