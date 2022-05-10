using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class GradingEntity
    {
        public Guid Id { get; set; }
        public StudentEntity Student { get; set; }
        public AssignmentEntity Assignment { get; set; }
        public float Grade { get; set; }
        public string Observations { get; set; }
    }
}
