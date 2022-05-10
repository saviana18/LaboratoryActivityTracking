using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccess.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;


        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }


        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IEnumerable<StudentModel> Get()
        {
            var result = new List<StudentModel>();
            foreach (var t in studentService.GetAllStudents())
            {
                result.Add(new StudentModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email,
                    Group = t.Group,
                    Hobby = t.Hobby,
                    IsPassed = t.IsPassed
                });
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        //[Auth]
        public ActionResult Post(StudentModel student)
        {
            try
            {
                studentService.AddStudentModel(new StudentModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Group = student.Group,
                    Hobby = student.Hobby,
                    IsPassed = student.IsPassed
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
        public IEnumerable<StudentModel> GetById(Guid id)
        {
            var result = new List<StudentModel>();
            var t = studentService.GetById(id);
            result.Add(new StudentModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Group = t.Group,
                Hobby = t.Hobby,
                IsPassed = t.IsPassed
            });
            return result;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Teacher")]
        public void Put(StudentModel student)
        {
            studentService.UpdateStudentModel(student);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Teacher")]
        public void Delete(Guid id)
        {
            studentService.DeleteStudentModel(id);
        }
    }
}
