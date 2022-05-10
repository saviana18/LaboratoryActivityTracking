using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccess.Contracts;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository repository;

        public StudentService(IGenericRepository repository)
        {
            this.repository = repository;
        }
        public void AddStudentModel(StudentModel student)
        {
            try
            {
                if (student != null)
                {
                    repository.Add<StudentEntity>(new StudentEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = student.Name,
                        Email = student.Email,
                        Group = student.Group,
                        Hobby = student.Hobby,
                        IsPassed = student.IsPassed
                    });

                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public List<StudentModel> GetAllStudents()
        {
            try
            {
                List<StudentModel> result = new List<StudentModel>();
                foreach (var x in repository.GetAll<StudentEntity>())
                {
                    result.Add(new StudentModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Group = x.Group,
                        Hobby = x.Hobby,
                        IsPassed = x.IsPassed
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void UpdateStudentModel(StudentModel student)
        {
            try
            {
                if (student != null)
                {
                    Guid id = student.Id;
                    var student1 = repository.GetById<StudentEntity>(id);
                    student1.Id = student.Id;
                    student1.Name = student.Name;
                    student1.Email = student.Email;
                    student1.Group = student.Group;
                    student1.Hobby = student.Hobby;
                    student1.IsPassed = student.IsPassed;
                    
                    repository.Update(student1);
                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteStudentModel(Guid id)
        {
            try
            {
                var student = repository.GetById<StudentEntity>(id);
                repository.Delete<StudentEntity>(student);
                repository.SaveChanges();
                
            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public StudentModel GetById(Guid id)
        {
            try
            {
                StudentModel result = new StudentModel();
                var x = repository.GetById<StudentEntity>(id);
                    result = (new StudentModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Group = x.Group,
                        Hobby = x.Hobby,
                        IsPassed = x.IsPassed
                    });
                
                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }

        }
    }
}
