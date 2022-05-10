using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAssignmentService
    {
        public List<AssignmentModel> GetAllAssignments();
        public AssignmentModel GetById(Guid id);
        public void AddAssignmentModel(AssignmentModel assignment);
        public void UpdateAssignmentModel(AssignmentModel assignment);
        public void DeleteAssignmentModel(Guid id);
        public List<AssignmentModel> GetAllAssignmentsByLaboratory(Guid id);
    }
}
