using Microsoft.AspNetCore.Mvc;
using PilarTestTask.Model;
using System;
using System.Linq;

namespace PilarTestTask.Controllers
{
    [Route("[controller]")]
    public class MandatoryInformationController : Controller
    {
        private readonly DataBaceContext _context;

        public MandatoryInformationController(DataBaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.MandatoryInformation.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]MandatoryInformation item)
        {
            if (item != null)
            {
                try
                {
                    MandatoryInformation MandatoryInfo = new MandatoryInformation
                    {
                        Name = item.Name,
                        Addres = item.Addres,
                        Email = item.Email,
                        Phone = item.Phone,
                        Comments = item.Comments
                    };

                    var insertedItem = _context.Add<MandatoryInformation>(MandatoryInfo);
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
        public JsonResult Update([FromBody]MandatoryInformation item)
        {
            if (item != null)
            {
                try
                {
                    var mandatoruInfoForUpdate = _context.MandatoryInformation.Where(x => x.Id == item.Id).FirstOrDefault();
                    mandatoruInfoForUpdate.Name = item.Name;
                    mandatoruInfoForUpdate.Addres = item.Addres;
                    mandatoruInfoForUpdate.Email = item.Email;
                    mandatoruInfoForUpdate.Phone = item.Phone;
                    mandatoruInfoForUpdate.Comments = item.Comments;

                    _context.SaveChanges();

                    return Json(mandatoruInfoForUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] MandatoryInformation item)
        {
            if (item != null)
            {
                try
                {
                    _context.MandatoryInformation.Remove(item);
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
