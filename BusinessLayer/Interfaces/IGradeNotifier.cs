using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGradeNotifier
    {
        void Attach(IGradeObserver observer);
        void Detach(IGradeObserver observer);
        void Notify(GradingModel gradingModel);
    }
}
