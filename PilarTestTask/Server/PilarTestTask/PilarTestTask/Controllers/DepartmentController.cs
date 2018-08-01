using Microsoft.AspNetCore.Mvc;
using PilarTestTask.Model;
using System;
using System.Linq;

namespace PilarTestTask.Controllers
{
    [Route("[controller]")]
    public class DepatmentController : Controller
    {
        private readonly DataBaceContext _context;

        public DepatmentController(DataBaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.Depatment.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("GetDepatment")]
        public JsonResult GetDepatmentById([FromBody]int id)
        {
            try
            {
                return Json(_context.Depatment.Where(c => c.UserId == id).ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]Depatment item)
        {
            if (item != null)
            {
                try
                {
                    Depatment MandatoryInfo = new Depatment
                    {
                        Name = item.Name,
                        UserId = item.UserId,
                        Address = item.Address,
                    };

                    var insertedItem = _context.Add<Depatment>(MandatoryInfo);
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
        public JsonResult Update([FromBody]Depatment item)
        {
            if (item != null)
            {
                try
                {
                    var depatmentForUpdate = _context.Depatment.Where(x => x.Id == item.Id).FirstOrDefault();
                    depatmentForUpdate.Name = item.Name;
                    depatmentForUpdate.Address = item.Address;
                    depatmentForUpdate.UserId = item.UserId;

                    _context.SaveChanges();

                    return Json(depatmentForUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] Depatment item)
        {
            if (item != null)
            {
                try
                {
                    _context.Depatment.Remove(item);
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
