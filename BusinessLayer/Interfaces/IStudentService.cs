using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IStudentService
    {
        public List<StudentModel> GetAllStudents();
        public StudentModel GetById(Guid id);
        public void AddStudentModel(StudentModel student);
        public void UpdateStudentModel(StudentModel student);
        public void DeleteStudentModel(Guid id);
    }
}
