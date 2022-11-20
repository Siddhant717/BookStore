using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedBackRL
    {
        bool AddFeedback(AddFeedbackModel addFeedbackModel);
        List<AllfeedbackModel> getFeedback(int bookId);
    }
}
