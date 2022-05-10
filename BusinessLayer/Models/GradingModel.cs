using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class GradingModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid AssignmentId { get; set; }
        public float Grade { get; set; }
        public string Observations { get; set; }
    }
}
