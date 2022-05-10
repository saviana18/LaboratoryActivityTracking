using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            this.assignmentService = assignmentService;
        }


        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IEnumerable<AssignmentModel> Get()
        {
            var result = new List<AssignmentModel>();
            foreach (var x in assignmentService.GetAllAssignments())
            {
                result.Add(new AssignmentModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    LaboratoryId = x.LaboratoryId
                });
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        //[Auth]
        public ActionResult Post(AssignmentModel assignment)
        {
            try
            {
                assignmentService.AddAssignmentModel(new AssignmentModel
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    Deadline = assignment.Deadline,
                    Description = assignment.Description,
                    LaboratoryId = assignment.LaboratoryId
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
        public IEnumerable<AssignmentModel> GetById(Guid id)
        {
            var result = new List<AssignmentModel>();
            var x = assignmentService.GetById(id);
            result.Add(new AssignmentModel
            {
                Id = x.Id,
                Name = x.Name,
                Deadline = x.Deadline,
                Description = x.Description,
                LaboratoryId = x.LaboratoryId
            });
            return result;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Teacher")]
        public void Put(AssignmentModel assignment)
        {
            assignmentService.UpdateAssignmentModel(assignment);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Teacher")]
        public void Delete(Guid id)
        {
            assignmentService.DeleteAssignmentModel(id);
        }

        [HttpGet("GetByLab")]
        [Authorize(Roles = "Student")]
        public IEnumerable<AssignmentModel> GetByLaboratory(Guid id)
        {
            var result = new List<AssignmentModel>();
            foreach (var x in assignmentService.GetAllAssignmentsByLaboratory(id))
            {
                result.Add(new AssignmentModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    LaboratoryId = x.LaboratoryId
                });
            }
            return result;
        }
    }
}
