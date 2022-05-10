using BusinessLayer;
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
    public class GradingController : ControllerBase
    {
        private readonly IGradingService gradingService;

        public GradingController(IGradingService gradingService)
        {
            this.gradingService = gradingService;
        }


        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IEnumerable<GradingModel> Get()
        {
            var result = new List<GradingModel>();
            foreach (var x in gradingService.GetAllGradings())
            {
                result.Add(new GradingModel
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    AssignmentId = x.AssignmentId,
                    Grade = x.Grade,
                    Observations = x.Observations
                });
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        //[Auth]
        public ActionResult Post(GradingModel grading)
        {
            try
            {
                gradingService.AddGradingModel(new GradingModel
                {
                    Id = grading.Id,
                    StudentId = grading.StudentId,
                    AssignmentId = grading.AssignmentId,
                    Grade = grading.Grade,
                    Observations = grading.Observations
                });

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }
        }

        [HttpGet("GetById")]
        [Authorize(Roles = "Teacher")]
        public IEnumerable<GradingModel> GetById(Guid id)
        {
            var result = new List<GradingModel>();
            var x = gradingService.GetById(id);
            result.Add(new GradingModel
            {
                Id = x.Id,
                StudentId = x.StudentId,
                AssignmentId = x.AssignmentId,
                Grade = x.Grade,
                Observations = x.Observations
            });
            return result;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Teacher")]
        public void Put(GradingModel grading)
        {
            gradingService.UpdateGradingModel(grading);
        }

        [HttpPut("Update/Student")]
        [Authorize(Roles = "Student")]
        public void PutForStudent(GradingModel grading)
        {
            gradingService.UpdateGradingModelForStudent(grading);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Teacher")]
        public void Delete(Guid id)
        {
            gradingService.DeleteGradingModel(id);
        }

        [HttpGet("GetGradingWeight")]
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult GetGradingWeight(Guid idStudent, Guid idLaboratory)
        {
            var grading = gradingService.GetGradingWeight(idStudent, idLaboratory);
            return Ok(grading);
        }

        [HttpPost("Observer")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Observer(GradingModel grading)
        {
            Console.WriteLine("Attaching Observers...");

            var emailObserver = new StudentEmailObserver();

            gradingService.Attach(emailObserver);

            Console.WriteLine("Updating grade...");

            gradingService.UpdateGradingModel(grading);

            return Ok(grading);
        }
    }
}
