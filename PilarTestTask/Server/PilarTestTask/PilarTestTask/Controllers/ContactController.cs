using Microsoft.AspNetCore.Mvc;
using PilarTestTask.Model;
using System;
using System.Linq;

namespace PilarTestTask.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly DataBaceContext _context;

        public ContactController(DataBaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.Contact.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("GetContact")]
        public JsonResult GetContactById([FromBody]int id)
        {
            try
            {
                return Json(_context.Contact.Where(c => c.UserId == id).ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]Contact item)
        {
            if (item != null)
            {
                try
                {
                    Contact MandatoryInfo = new Contact
                    {
                        Name = item.Name,
                        Role = item.Role,
                        Phone = item.Phone,
                        Mail = item.Mail
                    };

                    var insertedItem = _context.Add<Contact>(MandatoryInfo);
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
        public JsonResult Update([FromBody]Contact item)
        {
            if (item != null)
            {
                try
                {
                    var contactInfoForUpdate = _context.Contact.Where(x => x.Id == item.Id).FirstOrDefault();
                    contactInfoForUpdate.Name = item.Name;
                    contactInfoForUpdate.Role = item.Role;
                    contactInfoForUpdate.Phone = item.Phone;
                    contactInfoForUpdate.Mail = item.Mail;

                    _context.SaveChanges();

                    return Json(contactInfoForUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] Contact item)
        {
            if (item != null)
            {
                try
                {
                    _context.Contact.Remove(item);
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
