using Microsoft.AspNetCore.Mvc;
using PilarTestTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilarTestTask.Controllers
{
    [Route("[controller]")]
    public class BusinessController : Controller
    {
        private readonly DataBaceContext _context;

        public BusinessController(DataBaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.Business.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]Business item)
        {
            if (item != null)
            {
                try
                {
                    Business BusinessInfo = new Business
                    {
                        MandatoryInformationId = item.MandatoryInformationId
                    };

                    var insertedItem = _context.Add<Business>(BusinessInfo);
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
        public JsonResult Update([FromBody]Business item)
        {
            if (item != null)
            {
                try
                {
                    var businesUpdate = _context.Business.Where(x => x.Id == item.Id).FirstOrDefault();

                    businesUpdate.MandatoryInformationId = item.MandatoryInformationId;
                    _context.SaveChanges();

                    return Json(businesUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] Business item)
        {
            if (item != null)
            {
                try
                {
                    _context.Business.Remove(item);
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
