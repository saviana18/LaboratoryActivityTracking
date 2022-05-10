using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class AssignmentEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public LaboratoryEntity Laboratory { get; set; }

        public object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
