using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccess.Contracts;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class GradingService : IGradingService
    {
        private readonly IGenericRepository repository;
        private readonly IConfiguration configuration;
        public List<IGradeObserver> Observers = new List<IGradeObserver>();

        public GradingService(IGenericRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }
        public void AddGradingModel(GradingModel grading)
        {
            try
            {
                if (grading != null)
                {
                    repository.Add<GradingEntity>(new GradingEntity
                    {
                        Id = Guid.NewGuid(),
                        Student = repository.GetAll<StudentEntity>().Where(x => x.Id == grading.StudentId).First(),
                        Assignment = repository.GetAll<AssignmentEntity>().Where(x => x.Id == grading.AssignmentId).First(),
                        Grade = grading.Grade,
                        Observations = grading.Observations
                    }); ;

                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public List<GradingModel> GetAllGradings()
        {
            try
            {
                List<GradingModel> result = new List<GradingModel>();
                foreach (var x in repository.GetAll<GradingEntity>().Include(x => x.Student).Include(x => x.Assignment).ToList())
                {
                    result.Add(new GradingModel
                    {
                        Id = x.Id,
                        StudentId = x.Student.Id,
                        AssignmentId = x.Assignment.Id,
                        Grade = x.Grade,
                        Observations = x.Observations
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void UpdateGradingModel(GradingModel grading)
        {
            try
            {
                if (grading != null)
                {
                    var assignment = repository.GetAll<AssignmentEntity>().Include(x => x.Laboratory).Where(x => x.Id == grading.AssignmentId).First();
                    var stud = repository.GetAll<StudentEntity>().Where(x => x.Id == grading.StudentId).First();
                    Guid idGrading = grading.Id;
                    var grading1 = repository.GetAll<GradingEntity>().Include(x => x.Student).Include(x => x.Assignment).FirstOrDefault(x => x.Id == idGrading);
                    grading1.Id = idGrading;
                    grading1.Student = stud;
                    grading1.Assignment = assignment;
                    grading1.Grade = grading.Grade;
                    grading1.Observations = grading.Observations;

                    repository.Update(grading1);
                    repository.SaveChanges();
                    Notify(grading);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void UpdateGradingModelForStudent(GradingModel grading)
        {
            try
            {
                if (grading != null)
                {
                    Guid idGrading = grading.Id;
                    var grading1 = repository.GetAll<GradingEntity>().Include(x => x.Student).Include(x => x.Assignment).FirstOrDefault(x => x.Id == idGrading);
                    grading1.Observations = grading.Observations;

                    repository.Update(grading1);
                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteGradingModel(Guid id)
        {
            try
            {
                var grading = repository.GetById<GradingEntity>(id);
                repository.Delete<GradingEntity>(grading);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public GradingModel GetById(Guid id)
        {
            try
            {
                GradingModel result = new GradingModel();
                var x = repository.GetAll<GradingEntity>().Include(x => x.Student).Include(x => x.Assignment).FirstOrDefault(x => x.Id == id);
                result = (new GradingModel
                {
                    Id = x.Id,
                    StudentId = x.Student.Id,
                    AssignmentId = x.Assignment.Id,
                    Grade = x.Grade,
                    Observations = x.Observations
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }

        }

        public string GetGradingWeight(Guid idStudent, Guid idLaboratory)
        {
            float weight = 0;
            int count = 0;
            bool passed = true;
            bool submitted = true;
            List<GradingEntity> result = new List<GradingEntity>();
            foreach (var x in repository.GetAll<GradingEntity>().Include(x => x.Student).Include(x => x.Assignment).Include(x => x.Assignment.Laboratory).ToList())
            {
                if (x.Student.Id == idStudent && x.Assignment.Laboratory.Id == idLaboratory)
                {
                    result.Add(new GradingEntity
                    {
                        Id = x.Id,
                        Student = x.Student,
                        Assignment = x.Assignment,
                        Grade = x.Grade,
                        Observations = x.Observations
                    });
                }
                if (x.Observations != "string")
                {
                    submitted = false;
                }
            }
            
            var gradePerAssignment = configuration.GetSection("WeightedGrade:GradeForAssignment").Value;
            var averageValue = configuration.GetSection("WeightedGrade:AverageGrade").Value;
            foreach (var x in result)
            { 
                passed = false;
                if(x.Grade > int.Parse(gradePerAssignment))
                {
                    passed = true;
                }
                count++;
                weight += x.Grade;
            }

            weight /= count;

            if (weight > int.Parse(averageValue) && passed == true && submitted == true)
            {
                repository.Update<StudentEntity>(repository.GetById<StudentEntity>(idStudent));
                repository.SaveChanges();
                return "Passed with " + weight;
            }
            else
            {
                repository.Update<StudentEntity>(repository.GetById<StudentEntity>(idStudent));
                repository.SaveChanges();
                return "Failed with " + weight;
            } 
        }

        public void Attach(IGradeObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IGradeObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify(GradingModel gradingModel)
        {
            foreach (var observer in Observers)
            {
                observer.Update(gradingModel);
            }
        }
    }
}
