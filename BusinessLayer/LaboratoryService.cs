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
    public class LaboratoryService : ILaboratoryService
    {
        private readonly IGenericRepository repository;

        public LaboratoryService(IGenericRepository repository)
        {
            this.repository = repository;
        }
        public void AddLaboratoryModel(LaboratoryModel laboratory)
        {
            try
            {
                if (laboratory != null)
                {
                    repository.Add<LaboratoryEntity>(new LaboratoryEntity
                    {
                        Id = Guid.NewGuid(),
                        Number = laboratory.Number,
                        Date = laboratory.Date,
                        Title = laboratory.Title,
                        Objectives = laboratory.Objectives,
                        Description = laboratory.Description
                    });

                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public List<LaboratoryModel> GetAllLaboratories()
        {
            try
            {
                List<LaboratoryModel> result = new List<LaboratoryModel>();
                foreach (var x in repository.GetAll<LaboratoryEntity>())
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
            catch (Exception e)
            {
                return null;
            }
        }

        public void UpdateLaboratoryModel(LaboratoryModel laboratory)
        {
            try
            {
                if (laboratory != null)
                {
                    Guid id = laboratory.Id;
                    var laboratory1 = repository.GetById<LaboratoryEntity>(id);
                    laboratory1.Id = laboratory.Id;
                    laboratory1.Number = laboratory.Number;
                    laboratory1.Date = laboratory.Date;
                    laboratory1.Title = laboratory.Title;
                    laboratory1.Objectives = laboratory.Objectives;
                    laboratory1.Description = laboratory.Description;

                    repository.Update(laboratory1);
                    repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteLaboratoryModel(Guid id)
        {
            try
            {
                var laboratory = repository.GetById<LaboratoryEntity>(id);
                repository.Delete<LaboratoryEntity>(laboratory);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public LaboratoryModel GetById(Guid id)
        {
            try
            {
                LaboratoryModel result = new LaboratoryModel();
                var x = repository.GetById<LaboratoryEntity>(id);
                result = (new LaboratoryModel
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
            catch (NullReferenceException e)
            {
                return null;
            }

        }
    }
}
