using Microsoft.AspNetCore.Mvc;
using PilarTestTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilarTestTask.Controllers
{
    [Route("[controller]")]
    public class MunicipalityController : Controller
    {
        private readonly DataBaceContext _context;

        public MunicipalityController(DataBaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.Municipality.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]Municipality item)
        {
            if (item != null)
            {
                try
                {
                    Municipality municipalityInfo = new Municipality
                    {
                        MandatoryInformationId = item.MandatoryInformationId,
                        NumberOfSchool = item.NumberOfSchool
                    };

                    var insertedItem = _context.Add<Municipality>(municipalityInfo);
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
        public JsonResult Update([FromBody]Municipality item)
        {
            if (item != null)
            {
                try
                {
                    var municipalityUpdate = _context.Municipality.Where(x => x.Id == item.Id).FirstOrDefault();

                    municipalityUpdate.MandatoryInformationId = item.MandatoryInformationId;
                    municipalityUpdate.NumberOfSchool = item.NumberOfSchool;

                    _context.SaveChanges();

                    return Json(municipalityUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] Municipality item)
        {
            if (item != null)
            {
                try
                {
                    _context.Municipality.Remove(item);
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
