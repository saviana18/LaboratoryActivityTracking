using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class LaboratoryEntity
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Objectives { get; set; }
        public string Description { get; set; }

    }
}
