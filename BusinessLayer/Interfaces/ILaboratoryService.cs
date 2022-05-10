using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILaboratoryService
    {
        public List<LaboratoryModel> GetAllLaboratories();
        public LaboratoryModel GetById(Guid id);
        public void AddLaboratoryModel(LaboratoryModel laboratory);
        public void UpdateLaboratoryModel(LaboratoryModel laboratory);
        public void DeleteLaboratoryModel(Guid id);
    }
}
