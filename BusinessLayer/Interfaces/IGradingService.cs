using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGradingService : IGradeNotifier
    {
        public List<GradingModel> GetAllGradings();
        public GradingModel GetById(Guid id);
        public void AddGradingModel(GradingModel grading);
        public void UpdateGradingModel(GradingModel grading);
        public void DeleteGradingModel(Guid id);
        public string GetGradingWeight(Guid idStudent, Guid idLaboratory);
        public void UpdateGradingModelForStudent(GradingModel grading);
    }
}
