using AutoMapper;
using DAL;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class repository
    {

        #region Constructor
        private readonly context context;
        private readonly MapperConfiguration mapperConfiguration;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly identityContext identityContext;
        private readonly UserManager<user> userManager;
        private readonly SignInManager<user> signInManager;
        private readonly RoleManager<role> roleManager;
        private int siteId = 0;
        private readonly int allSiteId = 1;
        private int userId = 0;

        public repository(
            context context,
            MapperConfiguration mapperConfiguration,
            IHttpContextAccessor httpContextAccessor,
            identityContext identityContext,
            UserManager<user> userManager,
            SignInManager<user> signInManager,
            RoleManager<role> roleManager
            )
        {
            this.identityContext = identityContext;
            this.context = context;
            this.mapperConfiguration = mapperConfiguration;
            this.httpContextAccessor = httpContextAccessor;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.siteId = this.GetSiteId();
            this.userId = this.GetUserId();
        }
        #endregion

        #region Common Functions
        public int GetSiteId()
        {

            string host = this.httpContextAccessor.HttpContext.Request.Host.ToString();

            if (host.Contains("localhost"))
                //host = "www.danainsurance.co";
                host = "www.bimebaz.com";

            return context.sites.FirstOrDefault(s => s.host.ToLower() == host.ToLower()).id;
        }
        public int GetUserId()
        {
            try
            {
                var uId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                return Int32.Parse(uId);
            }
            catch
            {
                return 0;
            }
        }
        public DateTime GetLastAccess()
        {
            var site = context.sites.FirstOrDefault(s => s.id == this.siteId);
            return site.lastAccess;
        }
        public void UpdateLastAccess()
        {
            var site = context.sites.FirstOrDefault(s => s.id == this.siteId);
            site.lastAccess = DateTime.Now;
            context.SaveChanges();

        }

        public void Labeling(baseClass entity)
        {
            entity.siteId = this.siteId;
            entity.updateUserId = this.userId;
            entity.updateDateTime = DateTime.Now;

        }
        public baseClass UpdateEntity(baseClass entity)
        {
            if (entity.siteId != 0 && entity.siteId != this.siteId)
                throw new Exception("شما مجوز تغییر این قسمت را ندارید.");

            this.Labeling(entity);
            this.context.Update(entity);
            this.context.SaveChanges();
            return entity;
        }
        public baseClass UpdateEntity(baseClass entity, IFormFile image)
        {
            this.UpdateEntity(entity);
            this.SaveFile1(entity, image);
            return entity;
        }
        public void DeleteEntity(baseClass entity)
        {
            entity.isDeleted = true;
            this.UpdateEntity(entity);
            //this.context.SaveChanges();
        }
        public baseClass AddEntity(baseClass entity)
        {
            this.Labeling(entity);
            this.context.Add(entity);
            this.context.SaveChanges();
            return entity;
        }
        public baseClass DuplicateEntity(baseClass entity, baseClass newEntity)
        {
            var values = context.Entry(entity).CurrentValues.Clone();
            values["id"] = 0;
            this.context.Add(newEntity);
            context.Entry(newEntity).CurrentValues.SetValues(values);
            this.Labeling(newEntity);
            this.context.SaveChanges();
            return entity;
        }
        public baseClass AddEntity(baseClass entity, IFormFile image)
        {
            this.AddEntity(entity);
            this.SaveFile1(entity, image);
            return entity;
        }
        private bool SaveFile1(baseClass entity, IFormFile file, string extention = ".jpg")
        {
            var t = entity.GetType();
            var type = t.FullName.Replace("Entities.", "");

            if (file == null || file.Length == 0)
                return false;

            var hame = GetSiteName();
            string directoryAddress = Directory.GetCurrentDirectory() + "\\wwwroot\\" + hame + "\\image\\" + type;
            bool exists = System.IO.Directory.Exists(directoryAddress);
            if (!exists)
                Directory.CreateDirectory(directoryAddress);

            var path = Path.Combine(directoryAddress, (entity as baseClass).id.ToString() + extention);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return true;
        }
        private bool SaveFile<T>(IFormFile file, T item, string extention = ".jpg")
        {
            var type = typeof(T);

            if (file == null || file.Length == 0)
                return false;

            var hame = GetSiteName();
            string directoryAddress = Directory.GetCurrentDirectory() + "\\wwwroot\\" + hame + "\\image\\" + type.Name;
            bool exists = System.IO.Directory.Exists(directoryAddress);
            if (!exists)
                Directory.CreateDirectory(directoryAddress);

            var path = Path.Combine(directoryAddress, (item as baseClass).id.ToString() + extention);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return true;
        }

        #endregion

        #region Reminder
        public IEnumerable<reminder> GetReminders()
        {
            var reminders = context.reminders.Where(r => (r.siteId == this.allSiteId || r.siteId == this.siteId) && !r.isDeleted).
                                     OrderBy(r => r.title).
                                     ToList();
            return reminders;

        }
        public IEnumerable<reminder> GetReminders(int remindDays)
        {
            PersianCalendar pc = new PersianCalendar();
            var dt = pc.AddDays(DateTime.Now, remindDays);
            int dayOfMonth = pc.GetDayOfMonth(dt);
            int month = pc.GetMonth(dt);
            var reminders = context.reminders.Where(r => r.day == dayOfMonth && r.month == month &&
                                                        (r.siteId == this.allSiteId || r.siteId == this.siteId) && !r.isDeleted).
                                              OrderBy(r => r.title).
                                              ToList();
            return reminders;
        }

        public reminder GetReminder(int? id)
        {
            var reminder = context.reminders.SingleOrDefault(r => r.id == id &&
                                                          (r.siteId == this.allSiteId || r.siteId == this.siteId) && !r.isDeleted);
            return reminder;
        }
        #endregion


        #region User
        public List<user> GetUsers_ThisSite()
        {
            var users = userManager.Users.Where(u =>
                            u.siteId == this.siteId && !u.isDeleted).ToList();
            foreach (var user in users)
            {
                user.role = this.GetRole(user);
            }
            return users;
        }

        public user GetUser(string userName)
        {
            user user = userManager.Users.FirstOrDefault(u =>
                              u.actualUserName == userName &&
                              (u.siteId == u.siteId) && !u.isDeleted);

            var roles = userManager.GetRolesAsync(user).Result;
            user.role = this.GetRole(user);
            return user;
        }

        public user GetUser(int? id)
        {
            user user = userManager.Users.FirstOrDefault(u => u.Id == id &&
                                (u.siteId == u.siteId || u.siteId == allSiteId) && !u.isDeleted);
            if (user == null)
                return null;
            var roles = userManager.GetRolesAsync(user).Result;
            user.role = this.GetRole(user);
            return user;
        }

        public user_express GetUser_Express(string userName)
        {
            user user = GetUser(userName);
            var res = mapperConfiguration.CreateMapper().Map<user_express>(user);
            res.clientIsValid = true;
            return res;
        }

        public user_express GetUser_Express(int? id)
        {
            user user = GetUser(id);
            var res = mapperConfiguration.CreateMapper().Map<user_express>(user);
            res.clientIsValid = true;
            return res;
        }

        public IdentityResult AddUser(user_express user)
        {
            var result = userManager.CreateAsync(new user
            {
                siteId = this.siteId,
                actualUserName = user.actualUserName,
                firstName = user.firstName,
                lastName = user.lastName,
                Email = user.email,
                nationalCode = user.nationalCode,
                PhoneNumber = user.phoneNumber,
                updateUserId = this.userId,
                updateDateTime = DateTime.Now
            }, user.password
         ).Result;

            return result;
        }

        public IdentityResult DeleteUser(int id)
        {
            var user = this.GetUser(id);

            user.isDeleted = true;
            user.updateUserId = this.userId;
            user.updateDateTime = DateTime.Now;
            var result = userManager.UpdateAsync(user).Result;
            return result;
        }

        public IdentityResult AssignRoleToUser(int userId, string role)
        {
            var user = this.GetUser(userId);
            var roles = userManager.GetRolesAsync(user).Result;
            var t = userManager.RemoveFromRolesAsync(user, roles).Result;
            var result = userManager.AddToRoleAsync(user, role).Result;
            return result;
        }
        #endregion

        #region Role
        public List<role> GetAllRoles_ThisSite()
        {
            var roles = roleManager.Roles.Where(r =>
                          r.siteId == this.siteId && !r.isDeleted).ToList();
            return roles;
        }

        public string GetRole(user user)
        {
            var roles = userManager.GetRolesAsync(user).Result;
            if (roles.Count() == 0)
                return "";
            else
                return roles[0];

        }

        public string GetRole(int userId)
        {
            var user = this.GetUser(userId);
            return this.GetRole(user);
        }

        public string GetRoleOfCurrentUser()
        {
            try
            {
                return this.GetRole(this.userId);
            }
            catch
            {
                return "";
            }
        }

        public IdentityResult AddRole(string roleName)
        {
            var result = roleManager.CreateAsync(new role
            {
                siteId = this.siteId,
                actualName = roleName,
                updateUserId = this.userId,
                updateDateTime = DateTime.Now
            }).Result;

            return result;
        }

        #endregion

        #region Login
        public SignInResult Login(string userName, string password, bool isPersistent = false)
        {
            var user = GetUser(userName);
            signInManager.SignOutAsync();
            if (user != null)
            {
                var result = signInManager.PasswordSignInAsync(user, password, isPersistent, false).Result;
                return result;
            }
            return SignInResult.Failed;
        }
        public void LogOut()
        {
            signInManager.SignOutAsync();
        }
        #endregion

        #region AdminMenu
        //جدول اکشن که اضافه شد باید این تابع به روز بشه
        public bool HavePermision(string roleName, string area, string controller, string action)
        {
            var count = this.context.role_adminMenu.Include(ra => ra.adminMenu).
                                                    Where(ra => ra.roleName == roleName && ra.adminMenu.area == area && ra.adminMenu.controller == controller &&
                                                                ra.adminMenu.showInMenu &&
                                                                (ra.siteId == this.allSiteId || ra.siteId == this.siteId) && !ra.isDeleted &&
                                                                (ra.adminMenu.siteId == this.allSiteId || ra.adminMenu.siteId == this.siteId) && !ra.adminMenu.isDeleted).
                                                    Count();
            if (count > 0)
                return true;
            return false;
        }
        //جدول اکشن که اضافه شد باید این تابع به روز بشه
        public List<adminMenu> GetAdminMenusOfRole(string role)
        {
            var adminMenus = this.context.role_adminMenu.Include(ra => ra.adminMenu).
                                                         Where(ra => ra.roleName == role && ra.adminMenu.showInMenu &&
                                                                   (ra.siteId == this.allSiteId || ra.siteId == this.siteId) && !ra.isDeleted &&
                                                                   (ra.adminMenu.siteId == this.allSiteId || ra.adminMenu.siteId == this.siteId) && !ra.adminMenu.isDeleted).
                                                         Select(ra => ra.adminMenu).
                                                         ToList();

            return adminMenus;
        }
        #endregion

        #region Order
        public int AddOrder(insurance_client insurance)
        {
            order order = new order
            {
                userId = this.userId,
                dateTime = DateTime.Now,
                //ناتمام
                orderStatusId = (int)orderStatuses.inCompleted,
                insuranceId = insurance.id,
                price = insurance.price
            };
            //this.Labeling(order);
            //this.context.Add(order);
            //this.context.SaveChanges();
            this.AddEntity(order);
            AppendOrderField(order, insurance);
            this.context.SaveChanges();
            return order.id;
        }
        public order GetOrder(int? id)
        {
            var order = this.context.orders.Where(o => o.id == id &&
                                                      (o.siteId == this.allSiteId || o.siteId == this.siteId) && !o.isDeleted).
                                            Include(o => o.insurance).
                                            Include(o => o.fields).
                                            Include(o => o.paymentType).
                                            Include(o => o.orderStatus).
                                            First();
            order.fields = order.fields.OrderBy(of => of.orderIndex).ToList();
            order.user = this.GetUser(order.userId);
            return order;
        }
        public int ProccessOrderImage(IFormFileCollection images)
        {
            int orderId = 0;
            foreach (var image in images)
            {
                var arr = image.Name.Split('@');
                var orderField = this.GetOrderField(Int32.Parse(arr[0]), arr[1]);
                orderId = orderField.orderId;
                this.SaveFile<orderField>(image, orderField);
            }
            return orderId;
        }
        public List<order> GetOrdersOfCurrentUser_ThisSite()
        {
            var orders = this.context.orders.Where(o => o.userId == this.userId &&
                                                      (o.siteId == this.siteId) && !o.isDeleted).
                                             Include(o => o.fields).
                                             Include(o => o.insurance).
                                             ToList();
            return orders;
        }
        public List<order> GetOrdersInStatus_ThisSite(int? statusId)
        {
            var orders = this.context.orders.Where(o => o.orderStatusId == statusId &&
                                          (o.siteId == this.siteId) && !o.isDeleted).
                                 Include(o => o.insurance).
                                 Include(o => o.orderStatus).
                                 OrderByDescending(o => o.dateTime).
                                 ToList();

            foreach (var order in orders)
            {
                order.user = this.GetUser(order.userId);
            }
            return orders;
        }
        public bool SetOrderBankReference(order order, string bankReference)
        {
            try
            {
                order.bankReference = bankReference;

                this.UpdateEntity(order);
                //this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void AddLogToOrder(order order, string log)
        {
            order.log += "#" + log;
            this.context.SaveChanges();
        }
        public void ChangeOrderStatus(order order, int statusId)
        {
            order.orderStatusId = statusId;
            this.context.SaveChanges();
        }
        #endregion

        #region OrderField
        private void AppendOrderField(order order, insurance_client insurance)
        {
            foreach (var step in insurance.steps)
            {
                foreach (var fieldSet in step.fieldSets)
                {
                    foreach (var field in fieldSet.fields)
                    {
                        if (field.type == "label" || field.type == "html" ||
                            field.type == "acceptCheckBox" || field.type == "price")
                            continue;
                        else if (field.type == "paymentType")
                        {
                            order.paymentTypeId = Convert.ToInt32(field.value);
                            continue;
                        }

                        orderField orderField = new orderField
                        {
                            name = field.name,
                            type = field.type,
                            title = field.title
                        };
                        this.Labeling(orderField as baseClass);

                        if (field.type == "comboBox")
                        {
                            int? id = Int32.Parse(field.value);
                            orderField.value = this.GetDataValue(id).title;
                        }
                        else
                        {
                            orderField.value = field.value;
                        }
                        orderField.orderIndex = step.orderIndex * 10000 + fieldSet.orderIndex * 100 + field.orderIndex;
                        order.fields.Add(orderField);
                    }
                }
            }
        }
        public orderField GetOrderField(int orderId, string fieldName)
        {
            var orderField = context.orderFields.FirstOrDefault(of => of.orderId == orderId &&
                                                              of.name == fieldName &&
                                                              (of.siteId == this.siteId) && !of.isDeleted);
            return orderField;
        }
        #endregion

        #region OrderStatus
        public IEnumerable<orderStatus> GetActiveOrderStatuses()
        {
            var orderStatuses = context.orderStatuses.Where(os => (os.siteId == this.allSiteId || os.siteId == this.siteId) && os.active && !os.isDeleted).
                                                                     OrderBy(os => os.orderIndex).
                                                                     ToList();
            return orderStatuses;
        }
        public IEnumerable<orderStatus> GetOrderStatuses()
        {
            var orderStatuses = context.orderStatuses.Where(os => (os.siteId == this.allSiteId || os.siteId == this.siteId) && !os.isDeleted).
                                     OrderBy(os => os.orderIndex).
                                     ToList();
            return orderStatuses;

        }
        public orderStatus GetOrderStatus(int? id)
        {
            var orderStatus = context.orderStatuses.SingleOrDefault(os => os.id == id &&
                                                                (os.siteId == this.allSiteId || os.siteId == this.siteId) && !os.isDeleted);
            return orderStatus;
        }
        //public orderStatus AddOrderStatus(orderStatus orderStatus)
        //{
        //    this.UpdateEntity(orderStatus as baseClass);
        //    this.context.Add(orderStatus);
        //    this.context.SaveChanges();
        //    return orderStatus;
        //}
        //public orderStatus UpdateOrderStatus(orderStatus orderStatus)
        //{
        //    this.UpdateEntity(orderStatus as baseClass);
        //    this.context.Update(orderStatus);
        //    this.context.SaveChanges();
        //    return orderStatus;
        //}
        //public void DeleteOrderStatus(orderStatus orderStatus)
        //{
        //    this.DeleteEntity(orderStatus as baseClass);
        //}
        #endregion

        #region Setting
        public string GetSiteName()
        {
            return this.context.sites.FirstOrDefault(s => (s.id == this.siteId)).name;
        }
        public string GetSetting(string key)
        {
            try
            {
                return this.context.settings.FirstOrDefault(s => s.key == key &&
                                                               (s.siteId == this.allSiteId || s.siteId == this.siteId) && s.active && !s.isDeleted).
                                            value;
            }
            catch
            {

                return "";
            }
        }

        public IEnumerable<setting> GetSettings()
        {
            var settings = context.settings.Where(s => (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted).
                                     OrderBy(s => s.orderIndex).
                                     ToList();
            return settings;

        }

        public setting GetSetting(int? id)
        {
            var setting = context.settings.SingleOrDefault(s => s.id == id &&
                                                                (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted);
            return setting;
        }

        #endregion

        #region Insurance
        public IEnumerable<insurance> GetActiveInsurances()
        {
            var insurances = context.insurances.Where(i => (i.siteId == this.allSiteId || i.siteId == this.siteId) && i.active && !i.isDeleted).
                                     OrderBy(insurance => insurance.orderIndex).
                                     ToList();
            return insurances;
        }
        public IEnumerable<insurance_client> GetActiveInsurances_Client()
        {
            var insurances = GetActiveInsurances();
            var res = mapperConfiguration.CreateMapper().Map<List<insurance_client>>(insurances);
            foreach (var insurance in res)
            {
                insurance.step_navigations = new List<step_navigation>();
                var steps = this.context.steps.Where(s => s.insuranceId == insurance.id &&
                                                    (s.siteId == this.allSiteId || s.siteId == this.siteId) && s.active && !s.isDeleted).
                                               OrderBy(s => s.number).ToList();
                foreach (var step in steps)
                {
                    insurance.step_navigations.Add(new step_navigation { title = step.title, number = step.number, image = step.image });
                }
            }
            return res;

        }
        public IEnumerable<insurance> GetInsurances()
        {
            var insurances = context.insurances.Where(i => (i.siteId == this.allSiteId || i.siteId == this.siteId) && !i.isDeleted).
                                     OrderBy(insurance => insurance.orderIndex).
                                     ToList();
            return insurances;

        }
        public insurance GetInsurance(int? id)
        {
            var insurance = context.insurances.SingleOrDefault(i => i.id == id &&
                                                                (i.siteId == this.allSiteId || i.siteId == this.siteId) && !i.isDeleted);
            return insurance;
        }
        //public insurance AddInsurance(insurance insurance, IFormFile image)
        //{
        //    this.UpdateEntity(insurance as baseClass);
        //    this.context.Add(insurance);
        //    this.context.SaveChanges();
        //    this.SaveFile<insurance>(image, insurance);
        //    return insurance;
        //}

        //public insurance UpdateInsurance(insurance insurance, IFormFile image)
        //{
        //    this.UpdateEntity(insurance as baseClass);
        //    this.context.Update(insurance);
        //    this.context.SaveChanges();
        //    this.SaveFile<insurance>(image, insurance);
        //    return insurance;
        //}
        //public void DeleteInsurance(insurance insurance)
        //{
        //    this.DeleteEntity(insurance as baseClass);
        //}
        #endregion

        #region Step
        public List<step> GetStepsOfInsurance(int? insuranceId)
        {
            var steps = context.steps.Where(s => s.insuranceId == insuranceId &&
                                (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted).
                                OrderBy(s => s.number).
                                ToList();
            return steps;
        }

        public step GetStep(int? id)
        {
            var step = context.steps.SingleOrDefault(s => s.id == id &&
                                (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted);
            return step;
        }

        //public step AddStep(step step)
        //{
        //    this.UpdateEntity(step as baseClass);
        //    this.context.Add(step);
        //    this.context.SaveChanges();
        //    return step;
        //}

        //public step UpdateStep(step step)
        //{
        //    this.UpdateEntity(step as baseClass);
        //    this.context.Update(step);
        //    this.context.SaveChanges();
        //    return step;
        //}

        //public void DeleteStep(step step)
        //{
        //    this.DeleteEntity(step);
        //}

        public step GetActiveStep(int insuranceId, int stepNumber)
        {
            try
            {
                var step = context.steps.Where(s => s.number == stepNumber &&
                                                s.insuranceId == insuranceId &&
                                                (s.siteId == this.allSiteId || s.siteId == this.siteId) && s.active && !s.isDeleted).
                                    Include(s => s.fieldSets).
                                        ThenInclude(fieldSet => fieldSet.fields).
                                    First();
                step.fieldSets = step.fieldSets.Where(fs => (fs.siteId == this.allSiteId || fs.siteId == this.siteId) && fs.active && !fs.isDeleted).
                                                OrderBy(fs => fs.orderIndex).
                                                ToList();
                foreach (var fieldSet in step.fieldSets)
                {
                    fieldSet.fields = fieldSet.fields.Where(f => (f.siteId == this.allSiteId || f.siteId == this.siteId) && f.active && !f.isDeleted).
                                                      OrderBy(f => f.orderIndex).
                                                      ToList();
                }
                return step;
            }
            catch
            {
                return null;
            }
        }

        public step_client GetStep_client(int insuranceId, int stepNumber)
        {
            var step = GetActiveStep(insuranceId, stepNumber);
            var res = mapperConfiguration.CreateMapper().Map<step_client>(step);
            return res;
        }
        #endregion

        #region FieldSet
        public List<fieldSet> GetFieldSetsOfStep(int? stepId)
        {
            var fieldSets = context.fieldSets.Where(fs => fs.stepId == stepId &&
                                (fs.siteId == this.allSiteId || fs.siteId == this.siteId) && !fs.isDeleted).
                                Include(fs => fs.step).
                                OrderBy(fs => fs.orderIndex).
                                ToList();
            return fieldSets;
        }

        public fieldSet GetFieldSet(int? id)
        {
            var fieldSet = context.fieldSets.FirstOrDefault(fs => fs.id == id &&
                                                    (fs.siteId == this.allSiteId || fs.siteId == this.siteId) && !fs.isDeleted);
            return fieldSet;
        }

        //public fieldSet AddFieldSet(fieldSet fieldSet)
        //{
        //    this.UpdateEntity(fieldSet as baseClass);
        //    this.context.Add(fieldSet);
        //    this.context.SaveChanges();
        //    return fieldSet;
        //}

        //public fieldSet UpdateFieldSet(fieldSet fieldSet)
        //{
        //    this.UpdateEntity(fieldSet as baseClass);
        //    this.context.Update(fieldSet);
        //    this.context.SaveChanges();
        //    return fieldSet;
        //}

        //public void DeleteFieldSet(fieldSet fieldSet)
        //{
        //    this.DeleteEntity(fieldSet as baseClass);
        //}
        #endregion

        #region Field
        public List<field> GetFieldsOfFieldSet(int? fieldSetId)
        {
            var fields = context.fields.Where(f => f.fieldSetId == fieldSetId &&
                                              (f.siteId == this.allSiteId || f.siteId == this.siteId) && !f.isDeleted).
                                    Include(f => f.fieldSet).
                                    OrderBy(f => f.orderIndex).
                                    ToList();
            return fields;
        }

        public List<field> GetFieldsOfInsurance(int? insuranceId)
        {
            var fields = context.fields.Where(f => f.fieldSet.step.insuranceId == insuranceId &&
                                                       (f.siteId == this.allSiteId || f.siteId == this.siteId) && !f.isDeleted).
                                        OrderBy(f => f.title).
                                        ToList();

            return fields;
        }

        public field GetField(int? id)
        {
            var field = context.fields.Include(f => f.fieldSet).
                                       Include(f => f.father).
                                       SingleOrDefault(f => f.id == id &&
                                                          (f.siteId == this.allSiteId || f.siteId == this.siteId) && !f.isDeleted);
            return field;
        }

        //public field AddField(field field, IFormFile image)
        //{
        //    this.UpdateEntity(field as baseClass);
        //    this.context.Add(field);
        //    this.context.SaveChanges();
        //    this.SaveFile<field>(image, field);
        //    return field;
        //}

        //public field UpdateField(field field, IFormFile image)
        //{
        //    this.UpdateEntity(field as baseClass);
        //    this.context.Update(field);
        //    this.context.SaveChanges();
        //    this.SaveFile<field>(image, field);
        //    return field;
        //}

        //public void DeleteField(field field)
        //{
        //    this.DeleteEntity(field as baseClass);
        //}
        #endregion

        #region DataType 
        public IEnumerable<dataType> GetDataTypes()
        {
            var dataTypes = context.dataTypes.Where(i => (i.siteId == this.allSiteId || i.siteId == this.siteId) && !i.isDeleted).
                                          OrderBy(dt => dt.orderIndex).
                                          ToList();
            return dataTypes;

        }

        public dataType GetDataType(int? id)
        {
            var dataType = context.dataTypes.Include(bc => bc.father).
                                    SingleOrDefault(dt => dt.id == id &&
                                                               (dt.siteId == this.allSiteId || dt.siteId == this.siteId) && !dt.isDeleted);
            return dataType;
        }

        //public dataType AddDataType(dataType dataType)
        //{
        //    this.UpdateEntity(dataType as baseClass);
        //    this.context.Add(dataType);
        //    this.context.SaveChanges();
        //    return dataType;
        //}

        //public dataType UpdateDataType(dataType dataType)
        //{
        //    this.UpdateEntity(dataType as baseClass);
        //    this.context.Update(dataType);
        //    this.context.SaveChanges();
        //    return dataType;
        //}

        //public void DeleteDataType(dataType dataType)
        //{
        //    this.DeleteEntity(dataType as baseClass);
        //}

        #endregion

        #region DataValue
        public List<dataValue> GetDataValuesOfDataType(int? dataTypeId)
        {
            var dataValues = context.dataValues.Where(dv => dv.dataTypeId == dataTypeId &&
                                (dv.siteId == this.allSiteId || dv.siteId == this.siteId) && !dv.isDeleted).
                                OrderBy(dv => dv.orderIndex).
                                ToList();
            return dataValues;
        }
        public List<dataValue> GetDataValuesOfFatherDataType(int? dataTypeId)
        {
            var dataType = this.GetDataType(dataTypeId);
            var dataValues = context.dataValues.Where(dv => dv.dataTypeId == dataType.fatherId &&
                                (dv.siteId == this.allSiteId || dv.siteId == this.siteId) && !dv.isDeleted).ToList();
            return dataValues;
        }
        public dataValue GetDataValue(int? id)
        {
            var dataValue = context.dataValues.SingleOrDefault(dv => dv.id == id &&
                                (dv.siteId == this.allSiteId || dv.siteId == this.siteId) && !dv.isDeleted);
            return dataValue;
        }



        public List<dataValue_client> GetAcitveDataValues_Client(int dataTypeId)
        {
            var dataValues = context.dataValues.Where(dv => dv.dataTypeId == dataTypeId &&
                                                (dv.siteId == this.allSiteId || dv.siteId == this.siteId) && dv.active && !dv.isDeleted).
                                       //اینها باید حذف شوند
                                       Include(dataValue => dataValue.categories).
                                       ThenInclude(dataValue_category => dataValue_category.category).
                                       ThenInclude(category => category.attributes).
                                    OrderBy(dv => dv.orderIndex).ToList();
            foreach (var dataValue in dataValues)
            {
                dataValue.categories = dataValue.categories.Where(dv => !dv.isDeleted).ToList();
                foreach (var category in dataValue.categories)
                {
                    category.category.attributes = category.category.attributes.Where(a => !a.isDeleted && a.active).ToList();
                }
            }
            //var dataValues = this.GetActiveDataTypeIncludeValues(dataTypeId).dataValues;
            var res = mapperConfiguration.CreateMapper().Map<List<dataValue_client>>(dataValues);
            return res;
        }

        public List<dataValue_client> GetActiveChildDataValues_Client(int dataTypeId, int fatherId)
        {
            var dataValues = context.dataValues.Where(dv => dv.dataTypeId == dataTypeId && dv.fatherId == fatherId &&
                                                                 (dv.siteId == this.allSiteId || dv.siteId == this.siteId) && dv.active && !dv.isDeleted).
                                                        //اینها باید حذف شوند انشاالله
                                                        Include(dataValue => dataValue.categories).
                                                        ThenInclude(dataValue_category => dataValue_category.category).
                                                        ThenInclude(category => category.attributes).
                                                      OrderBy(dv => dv.orderIndex).
                                                      ToList();
            foreach (var dataValue in dataValues)
            {
                dataValue.categories = dataValue.categories.Where(dv => !dv.isDeleted).ToList();
                foreach (var category in dataValue.categories)
                {
                    category.category.attributes = category.category.attributes.Where(a => !a.isDeleted && a.active).ToList();
                }
            }
            var res = mapperConfiguration.CreateMapper().Map<List<dataValue_client>>(dataValues);
            return res;
        }

        #endregion

        #region Term
        public IEnumerable<term> GetTerms()
        {
            var terms = context.terms.Where(t => (t.siteId == this.allSiteId || t.siteId == this.siteId) && !t.isDeleted).
                                  OrderBy(t => t.orderIndex).
                                  ToList();
            return terms;

        }
        public List<term> GetTermsIncludeCategory(int dataTypeId)
        {
            var terms = context.terms.Include(t => t.categories).
                                Where(t => t.dataTypeId == dataTypeId &&
                                     (t.siteId == this.allSiteId || t.siteId == this.siteId) && !t.isDeleted).
                                OrderBy(t => t.orderIndex).
                                ToList();
            foreach (var term in terms)
            {
                term.categories = term.categories.Where(c => c.active).ToList();
            }
            return terms;
        }
        public List<category> GetAcitveCategories(int? dataValueId)
        {
            var categories = this.context.dataValue_category.Include(dvc => dvc.category).
                                                         Where(dvc => dvc.dataValueId == dataValueId && !dvc.category.isDeleted && dvc.category.active &&
                                                              (dvc.siteId == this.allSiteId || dvc.siteId == this.siteId) && !dvc.isDeleted).
                                                         Select(dvc => dvc.category).
                                                         OrderBy(dvc => dvc.orderIndex).
                                                         ToList();
            return categories;
        }

        public void SetCategoriesForDataValue(int datavalueId, List<int> categories)
        {
            var currentCategories = this.context.dataValue_category.Where(dvc => dvc.dataValueId == datavalueId &&
                                                                             (dvc.siteId == this.allSiteId || dvc.siteId == this.siteId) && !dvc.isDeleted).
                                                                ToList();
            foreach (var cupt in currentCategories)
            {
                if (categories != null && categories.Contains(cupt.categoryId))
                    continue;
                this.DeleteEntity(cupt);
            }

            if (categories == null)
                return;

            foreach (var ctg in categories)
            {
                if (currentCategories.Select(cuc => cuc.categoryId).Contains(ctg))
                    continue;
                dataValue_category dvc = new dataValue_category { dataValueId = datavalueId, categoryId = ctg };
                this.AddEntity(dvc);
                //this.Labeling(dvc as baseClass);
                //this.context.Add(dvc);
                //this.context.SaveChanges();
            }
        }


        public term GetTerm(int? id)
        {
            var term = context.terms.SingleOrDefault(t => t.id == id &&
                                                      (t.siteId == this.allSiteId || t.siteId == this.siteId) && !t.isDeleted);
            return term;
        }

        //public term AddTerm(term term)
        //{
        //    this.UpdateEntity(term as baseClass);
        //    this.context.Add(term);
        //    this.context.SaveChanges();
        //    return term;
        //}

        //public term UpdateTerm(term term)
        //{
        //    this.UpdateEntity(term as baseClass);
        //    this.context.Update(term);
        //    this.context.SaveChanges();
        //    return term;
        //}

        //public void DeleteTerm(term term)
        //{
        //    this.DeleteEntity(term as baseClass);
        //}
        #endregion

        #region Category
        public List<category> GetCategoriesOfTerm(int? termId)
        {
            var categories = context.categories.Where(c => c.termId == termId &&
                                (c.siteId == this.allSiteId || c.siteId == this.siteId) && !c.isDeleted).ToList();
            return categories;
        }

        public category GetCategory(int? id)
        {
            var category = context.categories.SingleOrDefault(c => c.id == id &&
                                                              (c.siteId == this.allSiteId || c.siteId == this.siteId) && !c.isDeleted);
            return category;
        }

        //public category AddCategory(category category)
        //{
        //    this.UpdateEntity(category as baseClass);
        //    this.context.Add(category);
        //    this.context.SaveChanges();
        //    return category;
        //}

        //public category UpdateCategory(category category)
        //{
        //    this.UpdateEntity(category as baseClass);
        //    this.context.Update(category);
        //    this.context.SaveChanges();
        //    return category;
        //}

        //public void DeleteCategory(category category)
        //{
        //    this.DeleteEntity(category as baseClass);
        //}

        #endregion

        #region Attribute
        public List<attribute> GetAttributesOfCategory(int? categoryId)
        {
            var attributes = context.attributes.Where(a => a.categoryId == categoryId &&
                                                       (a.siteId == this.allSiteId || a.siteId == this.siteId) && !a.isDeleted).
                                            Include(a => a.category).
                                            ToList();
            return attributes;
        }

        public attribute GetAttribute(int? id)
        {
            var attribute = context.attributes.SingleOrDefault(a => a.id == id &&
                                                               (a.siteId == this.allSiteId || a.siteId == this.siteId) && !a.isDeleted);
            return attribute;
        }

        //public attribute AddAttribute(attribute attribute)
        //{
        //    this.UpdateEntity(attribute as baseClass);
        //    this.context.Add(attribute);
        //    this.context.SaveChanges();
        //    return attribute;
        //}

        //public attribute UpdateAttribute(attribute attribute)
        //{
        //    this.UpdateEntity(attribute as baseClass);
        //    this.context.Update(attribute);
        //    this.context.SaveChanges();
        //    return attribute;
        //}

        //public void DeleteAttribute(attribute attribute)
        //{
        //    this.DeleteEntity(attribute as baseClass);
        //}
        #endregion

        #region Box
        public List<box> GetBoxesOfBoxCategory(int? boxCategoryId)
        {
            var boxes = context.boxes.Where(b => b.boxCategoryId == boxCategoryId &&
                                             (b.siteId == this.allSiteId || b.siteId == this.siteId) && !b.isDeleted).
                                  //Include(b=>b.boxCategory).
                                  ToList();
            return boxes;
        }

        public box GetBox(int? id)
        {
            var box = context.boxes.SingleOrDefault(b => b.id == id &&
                                                    (b.siteId == this.allSiteId || b.siteId == this.siteId) && !b.isDeleted);
            return box;
        }

        //public box AddBox(box box, IFormFile image)
        //{
        //    this.UpdateEntity(box as baseClass);
        //    this.context.Add(box);
        //    this.context.SaveChanges();
        //    this.SaveFile<box>(image, box);
        //    return box;
        //}

        //public box UpdateBox(box box, IFormFile image)
        //{
        //    this.UpdateEntity(box as baseClass);
        //    this.context.Update(box);
        //    this.context.SaveChanges();
        //    this.SaveFile<box>(image, box);
        //    return box;
        //}

        //public void DeleteBox(box box)
        //{
        //    this.DeleteEntity(box as baseClass);
        //}

        public Task<List<box>> GetActiveBoxesOfCategory_Async(int categoryId)
        {
            return context.boxes.Where(b => b.boxCategory.id == categoryId &&
                                        (b.siteId == this.allSiteId || b.siteId == this.siteId) && b.active && !b.isDeleted).
                             OrderBy(b => b.orderIndex).
                             ToListAsync();
        }
        #endregion

        #region BoxCategory

        public IEnumerable<boxCategory> GetBoxCategories()
        {
            var boxCategories = context.boxCategories.Where(bc => (bc.siteId == this.allSiteId || bc.siteId == this.siteId) && !bc.isDeleted).
                                                  OrderBy(bc => bc.title).
                                                  ToList();
            return boxCategories;

        }

        public IEnumerable<boxCategory> GetBoxCategories_hierarchy()
        {
            var boxCategories = context.boxCategories.Where(bc => bc.father == null &&
                                    (bc.siteId == this.allSiteId || bc.siteId == this.siteId) && !bc.isDeleted).
                                                   Include(bc => bc.childs).
                                                    ThenInclude(bc => bc.childs).
                                                    ThenInclude(bc => bc.childs).
                                                  OrderBy(bc => bc.orderIndex).
                                                  ToList();

            foreach (var level1 in boxCategories)
            {
                if (level1.childs != null && level1.childs.Count > 0)
                {
                    level1.childs = level1.childs.Where(m => !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                    foreach (var level2 in level1.childs)
                    {
                        if (level2.childs != null && level2.childs.Count > 0)
                        {
                            level2.childs = level2.childs.Where(m => !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                            foreach (var level3 in level2.childs)
                            {
                                if (level3.childs != null && level3.childs.Count > 0)
                                {
                                    level3.childs = level3.childs.Where(m => !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                                }
                            }
                        }
                    }
                }
            }
            return boxCategories;
        }

        public boxCategory GetBoxCategory(int? id)
        {
            var boxCategory = context.boxCategories.Include(bc => bc.father).
                                     SingleOrDefault(bc => bc.id == id &&
                                                                (bc.siteId == this.allSiteId || bc.siteId == this.siteId) && !bc.isDeleted);
            return boxCategory;
        }

        //public boxCategory AddBoxCategory(boxCategory boxCategory, IFormFile image)
        //{
        //    this.UpdateEntity(boxCategory as baseClass);
        //    this.context.Add(boxCategory);
        //    this.context.SaveChanges();
        //    this.SaveFile<boxCategory>(image, boxCategory);
        //    return boxCategory;
        //}

        //public boxCategory UpdateBoxCategory(boxCategory boxCategory, IFormFile image)
        //{
        //    this.UpdateEntity(boxCategory as baseClass);
        //    this.context.Update(boxCategory);
        //    this.context.SaveChanges();
        //    this.SaveFile<boxCategory>(image, boxCategory);
        //    return boxCategory;
        //}

        //public void DeleteBoxCategory(boxCategory boxCategory)
        //{
        //    this.DeleteEntity(boxCategory as baseClass);
        //}

        public string GetBoxCategoryTitle(int id)
        {
            return context.boxCategories.SingleOrDefault(bc => bc.id == id &&
                                                          (bc.siteId == this.allSiteId || bc.siteId == this.siteId) && !bc.isDeleted).
                                     title;
        }

        public List<boxCategory> GetActiveBoxCategories_Async(int fatherId)
        {
            List<boxCategory> boxCategories =
                context.boxCategories.Where(bc => bc.father.id == fatherId &&
                                              (bc.siteId == this.allSiteId || bc.siteId == this.siteId) && bc.active && !bc.isDeleted).
                                  Include(bc => bc.boxes).
                                  OrderBy(bc => bc.orderIndex).ToList();

            foreach (var boxCategory in boxCategories)
                boxCategory.boxes = boxCategory.boxes.Where(b => b.active && !b.isDeleted).OrderBy(b => b.orderIndex).ToList();
            return boxCategories;
        }
        #endregion

        #region Menu
        public IEnumerable<menu> GetMenus()
        {
            var menus = context.menus.Where(m => (m.siteId == this.allSiteId || m.siteId == this.siteId) && !m.isDeleted).
                                     OrderBy(m => m.title).
                                     ToList();
            return menus;

        }

        public IEnumerable<menu> GetMenus_hierarchy()
        {
            var menus = context.menus.Where(m => m.father == null &&
                                                (m.siteId == this.allSiteId || m.siteId == this.siteId) && !m.isDeleted).
                                                 Include(m => m.childs).
                                                 ThenInclude(m => m.childs).
                                                 ThenInclude(m => m.childs).
                                     OrderBy(m => m.orderIndex).
                                     ToList();
            foreach (var level1 in menus)
            {
                if (level1.childs != null && level1.childs.Count > 0)
                {
                    level1.childs = level1.childs.Where(m => !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                    foreach (var level2 in level1.childs)
                    {
                        if (level2.childs != null && level2.childs.Count > 0)
                        {
                            level2.childs = level2.childs.Where(m => !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                            foreach (var level3 in level2.childs)
                            {
                                if (level3.childs != null && level3.childs.Count > 0)
                                {
                                    level3.childs = level3.childs.Where(m => !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                                }
                            }
                        }
                    }
                }
            }

            return menus;

        }

        public menu GetMenu(int? id)
        {
            var menu = context.menus.Include(m => m.father).
                                     SingleOrDefault(m => m.id == id &&
                                                          (m.siteId == this.allSiteId || m.siteId == this.siteId) && !m.isDeleted);
            return menu;
        }

        //public menu AddMenu(menu menu, IFormFile image)
        //{
        //    this.UpdateEntity(menu as baseClass);
        //    this.context.Add(menu);
        //    this.context.SaveChanges();
        //    this.SaveFile<menu>(image, menu);
        //    return menu;
        //}

        //public menu UpdateMenu(menu menu, IFormFile image)
        //{
        //    this.UpdateEntity(menu as baseClass);
        //    this.context.Update(menu);
        //    this.context.SaveChanges();
        //    this.SaveFile<menu>(image, menu);
        //    return menu;
        //}

        //public void DeleteMenu(menu menu)
        //{
        //    this.DeleteEntity(menu as baseClass);
        //}

        public List<menu> GeActiveChildMenus_Async(int fatherId)
        {
            var menus = context.menus.Where(m => m.father.id == fatherId &&
                                        (m.siteId == this.allSiteId || m.siteId == this.siteId) && m.active && !m.isDeleted).
                             Include(m => m.childs).
                             ThenInclude(m => m.childs).
                             OrderBy(m => m.orderIndex).ToList();

            foreach (var level1 in menus)
            {
                if (level1.childs != null && level1.childs.Count > 0)
                {
                    level1.childs = level1.childs.Where(m => m.active && !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                    foreach (var level2 in level1.childs)
                    {
                        if (level2.childs != null && level2.childs.Count > 0)
                        {
                            level2.childs = level2.childs.Where(m => m.active && !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                            foreach (var level3 in level2.childs)
                            {
                                if (level3.childs != null && level3.childs.Count > 0)
                                {
                                    level3.childs = level3.childs.Where(m => m.active && !m.isDeleted).OrderBy(m => m.orderIndex).ToList();
                                }
                            }
                        }
                    }
                }
            }
            return menus;
        }
        #endregion

        #region PostCategory
        public IEnumerable<postCategory> GetPostCategories()
        {
            var postCategories = context.postCategories.Where(pc => (pc.siteId == this.allSiteId || pc.siteId == this.siteId) && !pc.isDeleted).
                                     OrderBy(p => p.orderIndex).
                                     ToList();
            return postCategories;

        }

        public postCategory GetPostCategory(int? id)
        {
            var postCategory = context.postCategories.SingleOrDefault(pc => pc.id == id &&
                                                                (pc.siteId == this.allSiteId || pc.siteId == this.siteId) && !pc.isDeleted);
            return postCategory;
        }

        //public postCategory AddPostCategory(postCategory postCategory)
        //{
        //    this.UpdateEntity(postCategory as baseClass);
        //    this.context.Add(postCategory);
        //    this.context.SaveChanges();
        //    return postCategory;
        //}

        //public postCategory UpdatePostCategory(postCategory postCategory)
        //{
        //    this.UpdateEntity(postCategory as baseClass);
        //    this.context.Update(postCategory);
        //    this.context.SaveChanges();
        //    return postCategory;
        //}

        //public void DeletePostCategory(postCategory postCategory)
        //{
        //    this.DeleteEntity(postCategory as baseClass);
        //}
        #endregion

        #region Post
        public IEnumerable<post> GetPosts()
        {
            var posts = context.posts.Where(p => (p.siteId == this.allSiteId || p.siteId == this.siteId) && !p.isDeleted).
                                     OrderBy(p => p.orderIndex).
                                     ToList();
            return posts;

        }

        public post GetPost(int? id)
        {
            var post = context.posts.SingleOrDefault(p => p.id == id &&
                                                                (p.siteId == this.allSiteId || p.siteId == this.siteId) && !p.isDeleted);
            return post;
        }
        #endregion

        #region PaymentType
        public List<paymentType> GetPaymentTypes()
        {
            var paymentTypes = this.context.paymentTypes.Where(pt => (pt.siteId == this.allSiteId || pt.siteId == this.siteId) && !pt.isDeleted).
                                                         OrderBy(pt => pt.orderIndex).ToList();
            return paymentTypes;
        }

        public List<paymentType> GetAcitvePaymentTypes(int? uId)
        {
            var paymentTypes = this.context.user_paymentType.Include(upt => upt.paymentType).
                                                         Where(upt => upt.userId == uId && !upt.paymentType.showForAll && !upt.paymentType.isDeleted && upt.paymentType.active &&
                                                              (upt.siteId == this.allSiteId || upt.siteId == this.siteId) && !upt.isDeleted).
                                                         Select(upt => upt.paymentType);
            var showForAll = this.context.paymentTypes.Where(pt => pt.showForAll && pt.active &&
                                                              (pt.siteId == this.allSiteId || pt.siteId == this.siteId) && !pt.isDeleted);
            return paymentTypes.Union(showForAll).
                                OrderBy(pt => pt.orderIndex).
                                ToList();
        }

        public List<paymentType_client> GetAcitvePaymentTypesOfCurrentUser_Client()
        {
            var paymentTypes = GetAcitvePaymentTypes(this.userId);
            var res = mapperConfiguration.CreateMapper().Map<List<paymentType_client>>(paymentTypes);
            return res;
        }

        public void SetPaymentTypesForUser(int uId, int[] paymentTypes)
        {
            var currentPaymentTypes = this.context.user_paymentType.Where(upt => upt.userId == uId &&
                                                                             (upt.siteId == this.allSiteId || upt.siteId == this.siteId) && !upt.isDeleted).
                                                                ToList();
            foreach (var cupt in currentPaymentTypes)
            {
                if (paymentTypes != null && paymentTypes.Contains(cupt.paymentTypeId))
                    continue;
                this.DeleteEntity(cupt as baseClass);
            }

            if (paymentTypes == null)
                return;
            foreach (var pt in paymentTypes)
            {
                if (currentPaymentTypes.Select(cupt => cupt.paymentTypeId).Contains(pt))
                    continue;
                user_paymentType upt = new user_paymentType { userId = uId, paymentTypeId = pt };
                this.AddEntity(upt);
            }
        }

        public paymentType GetPaymentType(int? id)
        {
            var paymentType = context.paymentTypes.SingleOrDefault(pt => pt.id == id &&
                                                                (pt.siteId == this.allSiteId || pt.siteId == this.siteId) && !pt.isDeleted);
            return paymentType;
        }

        #endregion

        #region Term
        public IEnumerable<hook> GetHooks()
        {
            var hooks = context.hooks.Where(h => (h.siteId == this.allSiteId || h.siteId == this.siteId) && !h.isDeleted).
                                  OrderBy(h => h.orderIndex).
                                  ToList();
            return hooks;

        }
        public List<sms> GetActiveSmses(string hookName)
        {
            var smses = this.context.smses.Where(s => s.hook.name == hookName &&
                                                              (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted && s.active).
                                                         OrderBy(s => s.orderIndex).
                                                         ToList();
            return smses;
        }
        public List<email> GetActiveEmails(string hookName)
        {
            var emails = this.context.emails.Where(e => e.hook.name == hookName &&
                                                              (e.siteId == this.allSiteId || e.siteId == this.siteId) && !e.isDeleted && e.active).
                                                         OrderBy(e => e.orderIndex).
                                                         ToList();
            return emails;
        }

        public hook GetHook(int? id)
        {
            var hook = context.hooks.SingleOrDefault(h => h.id == id &&
                                                      (h.siteId == this.allSiteId || h.siteId == this.siteId) && !h.isDeleted);
            return hook;
        }

        #endregion

        #region Sms
        public List<sms> GetSmsesOfHook(int? hookId)
        {
            var smses = context.smses.Where(s => s.hookId == hookId &&
                                (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted).ToList();
            return smses;
        }

        public sms GetSms(int? id)
        {
            var sms = context.smses.SingleOrDefault(s => s.id == id &&
                                                              (s.siteId == this.allSiteId || s.siteId == this.siteId) && !s.isDeleted);
            return sms;
        }

        #endregion

        #region Email
        public List<email> GetEmailsOfHook(int? hookId)
        {
            var emails = context.emails.Where(e => e.hookId == hookId &&
                                (e.siteId == this.allSiteId || e.siteId == this.siteId) && !e.isDeleted).ToList();
            return emails;
        }

        public email GetEmail(int? id)
        {
            var email = context.emails.SingleOrDefault(e => e.id == id &&
                                                              (e.siteId == this.allSiteId || e.siteId == this.siteId) && !e.isDeleted);
            return email;
        }

        #endregion
    }
}
