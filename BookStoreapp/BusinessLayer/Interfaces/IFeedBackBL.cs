using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedBackBL
    {
        bool AddFeedback(AddFeedbackModel addFeedbackModel);
        List<AllfeedbackModel> getFeedback(int bookId);
    }
}

