using Microsoft.AspNetCore.Mvc;
using PilarTestTask.Model;
using System;
using System.Linq;

namespace PilarTestTask.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly DataBaceContext _context;

        public UserController(DataBaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.User.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("GetUser")]
        public JsonResult GetUserById([FromBody]int id)
        {
            try
            {
                return Json(_context.User.Where(c => c.Id == id).ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Identification")]
        public JsonResult UserIdentification([FromBody]Identification user)
        {
            if(user != null)
            {            
                try
                {
                    return Json(_context.User.Where(c => (c.UserName == user.Name && c.Password == user.Password)).Select(c => new {c.IsAdmin, c.Id}).ToList());
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]User item)
        {
            if (item != null)
            {
                try
                {
                    User MandatoryInfo = new User
                    {
                        Name = item.Name,
                        Mobile = item.Mobile,
                        Male = item.Male,
                        DepatmentId = item.DepatmentId,
                        UserName = item.UserName,
                        Password = item.Password,
                        IsAdmin = item.IsAdmin
                    };

                    var insertedItem = _context.Add<User>(MandatoryInfo);
                    _context.SaveChanges();
                    return Json(insertedItem.Entity);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpPut, Route("Update")]
        public JsonResult Update([FromBody]User item)
        {
            if (item != null)
            {
                try
                {
                    var userForUpdate = _context.User.Where(x => x.Id == item.Id).FirstOrDefault();
                    userForUpdate.Name = item.Name;
                    userForUpdate.Mobile = item.Mobile;
                    userForUpdate.Male = item.Male;
                    userForUpdate.DepatmentId = item.DepatmentId;
                    userForUpdate.UserName = item.UserName;
                    userForUpdate.Password = item.Password;
                    userForUpdate.IsAdmin = item.IsAdmin;

                    _context.SaveChanges();

                    return Json(userForUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] User item)
        {
            if (item != null)
            {
                try
                {
                    _context.User.Remove(item);
                    _context.SaveChanges();

                    return Json(item);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }
    }
}
