using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryController : ControllerBase
    {
        private readonly ILaboratoryService laboratoryService;

        public LaboratoryController(ILaboratoryService laboratoryService)
        {
            this.laboratoryService = laboratoryService;
        }


        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        public IEnumerable<LaboratoryModel> Get()
        {
            var result = new List<LaboratoryModel>();
            foreach (var x in laboratoryService.GetAllLaboratories())
            {
                result.Add(new LaboratoryModel
                {
                    Id = x.Id,
                    Number = x.Number,
                    Date = x.Date,
                    Title = x.Title,
                    Objectives = x.Objectives,
                    Description = x.Description
                });
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        //[Auth]
        public ActionResult Post(LaboratoryModel laboratory)
        {
            try
            {
                laboratoryService.AddLaboratoryModel(new LaboratoryModel
                {
                    Id = laboratory.Id,
                    Number = laboratory.Number,
                    Date = laboratory.Date,
                    Title = laboratory.Title,
                    Objectives = laboratory.Objectives,
                    Description = laboratory.Description
                });

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Pfuuu");
            }
        }

        [HttpGet("GetById")]
        [Authorize(Roles = "Teacher")]
        public IEnumerable<LaboratoryModel> GetById(Guid id)
        {
            var result = new List<LaboratoryModel>();
            var x = laboratoryService.GetById(id);
            result.Add(new LaboratoryModel
            {
                Id = x.Id,
                Number = x.Number,
                Date = x.Date,
                Title = x.Title,
                Objectives = x.Objectives,
                Description = x.Description
            });
            return result;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Teacher")]
        public void Put(LaboratoryModel laboratory)
        {
            laboratoryService.UpdateLaboratoryModel(laboratory);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Teacher")]
        public void Delete(Guid id)
        {
            laboratoryService.DeleteLaboratoryModel(id);
        }
    }
}
