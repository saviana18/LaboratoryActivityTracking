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

namespace BusinessLayer
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IGenericRepository repository;

        public AssignmentService(IGenericRepository repository)
        {
            this.repository = repository;
        }
        public void AddAssignmentModel(AssignmentModel assignment)
        {
            try
            {
                if (assignment != null)
                {
                    repository.Add<AssignmentEntity>(new AssignmentEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = assignment.Name,
                        Deadline = assignment.Deadline,
                        Description = assignment.Description,
                        Laboratory = repository.GetAll<LaboratoryEntity>().Where(x => x.Id == assignment.LaboratoryId).First()
                    });

                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public List<AssignmentModel> GetAllAssignments()
        {
            try
            {
                List<AssignmentModel> result = new List<AssignmentModel>();
                foreach (var x in repository.GetAll<AssignmentEntity>().Include(x => x.Laboratory).ToList())
                {
                    result.Add(new AssignmentModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Deadline = x.Deadline,
                        Description = x.Description,
                        LaboratoryId = x.Laboratory.Id
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void UpdateAssignmentModel(AssignmentModel assignment)
        {
            try
            {
                if (assignment != null)
                {
                    var lab = repository.GetAll<LaboratoryEntity>().Where(x => x.Id == assignment.LaboratoryId).First();
                    Guid id = assignment.Id;
                    var assignment1 = repository.GetById<AssignmentEntity>(id);
                    assignment1.Id = assignment.Id;
                    assignment1.Name = assignment.Name;
                    assignment1.Deadline = assignment.Deadline;
                    assignment1.Description = assignment.Description;
                    assignment1.Laboratory = lab;

                    repository.Update(assignment1);
                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteAssignmentModel(Guid id)
        {
            try
            {
                var assignment = repository.GetById<AssignmentEntity>(id);
                repository.Delete<AssignmentEntity>(assignment);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public AssignmentModel GetById(Guid id)
        {
            try
            {
                AssignmentModel result = new AssignmentModel();
                var x = repository.GetAll<AssignmentEntity>().Include(x => x.Laboratory).FirstOrDefault(x => x.Id == id);
                result = (new AssignmentModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Deadline = x.Deadline,
                    Description= x.Description,
                    LaboratoryId = x.Laboratory.Id
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }

        }

        public List<AssignmentModel> GetAllAssignmentsByLaboratory(Guid id)
        {
            List<AssignmentModel> result = new List<AssignmentModel>();
            foreach (var assignment in repository.GetAll<AssignmentEntity>().Include(x => x.Laboratory).ToList())
            {
                if(assignment.Laboratory.Id == id)
                {
                    result.Add(new AssignmentModel
                    {
                        Id = assignment.Id,
                        Name = assignment.Name,
                        Deadline = assignment.Deadline,
                        Description = assignment.Description,
                        LaboratoryId = assignment.Laboratory.Id
                    });
                }
            }
            return result;
        }
    }
}
