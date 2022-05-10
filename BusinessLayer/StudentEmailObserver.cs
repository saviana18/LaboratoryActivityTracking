using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StudentEmailObserver : IGradeObserver
    {
        public void Update(GradingModel gradingModel)
        {
            Console.WriteLine("The grade '{0}' for the assignment '{1} was added. An email was sent to the student.",
                gradingModel.Grade.ToString(), gradingModel.AssignmentId.ToString());
        }
    }
}
